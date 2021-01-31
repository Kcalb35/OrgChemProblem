using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrgChemProblem.Models;
using OrgChemProblem.Utils;

namespace OrgChemProblem.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    public class ProblemApi : ControllerBase
    {
        private readonly IProblemRepository _Repository;

        public ProblemApi(IProblemRepository repo)
        {
            _Repository = repo;
        }

        [HttpGet]
        [Route("problem/{id:int}")]
        public async Task<IActionResult> GetProblemById([FromRoute] int id)
        {
            var problem = await _Repository.GetProblemByIdAsync(id);
            return Ok(problem);
        }

        [HttpPost]
        [Route("problem")]
        public async Task<IActionResult> CreateProblem([FromForm] ProblemUpload upload)
        {
            var problem = new Problem
            {
                AnswerDescription = upload.AnsDescription,
                ProblemDescription = upload.ProblemDescription
            };

            // savefile
            problem.AnswerPicture = ImageHandle.SaveImg(upload.AnsImg);
            problem.ProblemPicture = ImageHandle.SaveImg(upload.ProblemImg);
            problem.Tags = upload.Tags.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            problem = await _Repository.CreateProblemAsync(problem);
            return Ok(problem);
        }

        [HttpDelete]
        [Route("problem/{id:int}")]
        public async Task<IActionResult> DeleteProblemById([FromRoute] int id)
        {
            await _Repository.DeleteProblemByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        [Route("problem")]
        public async Task<IActionResult> UpdateProblem([FromForm] ProblemUpload upload)
        {
            if (upload.Id == null) return BadRequest();
            var problem = await _Repository.GetProblemByIdAsync(upload.Id.Value);
            problem.AnswerDescription = upload.AnsDescription;
            problem.ProblemDescription = upload.ProblemDescription;
            problem.AnswerPicture = ImageHandle.SaveImg(upload.AnsImg);
            problem.ProblemPicture = ImageHandle.SaveImg(upload.ProblemImg);
            problem.Tags = upload.Tags.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            problem = await _Repository.UpdateProblemAsync(problem);
            return Ok(problem);
        }

        [HttpPost]
        [Route("search")]
        public  IActionResult SearchProblems([FromForm] ProblemSearch search)
        {
            var li = _Repository.GetAllProblems();
            var tags = search.Tags.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrEmpty(search.ProblemDescription))
            {
                // 先筛有题目描述的题
                li = li.Where(p => !string.IsNullOrEmpty(p.ProblemDescription));
                li = li.OrderByDescending(p =>
                {
                    double tagW = SimilarityTool.TagSim(p.Tags, tags);
                    double desW = SimilarityTool.LDistanceSim(search.ProblemDescription, p.ProblemDescription);
                    return SimilarityTool.CalculateWeight(tagW, desW);
                });
                return Ok(li);
            }
            else
            {
                // 没有题目描述
                li = li.OrderByDescending(p=> SimilarityTool.TagSim(p.Tags,tags));
                return Ok(li);
            }
        }
    }
}