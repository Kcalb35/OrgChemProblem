using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrgChemProblem.Exceptions;
using OrgChemProblem.Models;
using OrgChemProblem.Utils;

namespace OrgChemProblem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/problem")]
    public class ProblemApi : ControllerBase
    {
        private readonly ILogger<ProblemApi> _Logger;
        private readonly IProblemRepository _Repository;

        public ProblemApi(IProblemRepository repo, ILogger<ProblemApi> logger)
        {
            _Repository = repo;
            _Logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProblemById([FromRoute] int id)
        {
            try
            {
                var problem = await _Repository.GetProblemByIdAsync(id);
                return Ok(problem);
            }
            catch (ProblemNotFound e)
            {
                _Logger.LogError("Problem {e.Id} not found", e.Id);
                return NotFound(new {id = e.Id});
            }
        }

        [HttpPost]
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
            _Logger.LogInformation("Created problem {problem.Id}", problem.Id);
            return Ok(problem);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProblemById([FromRoute] int id)
        {
            try
            {
                await _Repository.DeleteProblemByIdAsync(id);
                _Logger.LogInformation("Deleted problem {id}", id);
                return Ok();
            }
            catch (ProblemNotFound e)
            {
                _Logger.LogError("Problem {e.Id} not found", e.Id);
                return NotFound(new {id = e.Id});
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProblem([FromForm] ProblemUpload upload)
        {
            if (upload.Id == null) return BadRequest();
            try
            {
                var problem = await _Repository.GetProblemByIdAsync(upload.Id.Value);
                problem.AnswerDescription = upload.AnsDescription;
                problem.ProblemDescription = upload.ProblemDescription;
                problem.AnswerPicture = ImageHandle.SaveImg(upload.AnsImg);
                problem.ProblemPicture = ImageHandle.SaveImg(upload.ProblemImg);
                problem.Tags = upload.Tags.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                problem = await _Repository.UpdateProblemAsync(problem);
                _Logger.LogInformation("Updated problem {problem.Id}", problem.Id);
                return Ok(problem);
            }
            catch (ProblemNotFound e)
            {
                _Logger.LogError("Problem {e.Id} not found", e.Id);
                return NotFound(new {id = e.Id});
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("search")]
        public IActionResult SearchProblems([FromForm] ProblemSearch search)
        {
            var li = _Repository.GetAllProblems();
            var tags = search.Tags.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrEmpty(search.ProblemDescription))
            {
                // 先筛有题目描述的题
                li = li.Where(p => !string.IsNullOrEmpty(p.ProblemDescription));
                li = li.OrderByDescending(p =>
                {
                    var tagW = SimilarityTool.TagSim(p.Tags, tags);
                    var desW = SimilarityTool.LDistanceSim(search.ProblemDescription, p.ProblemDescription);
                    return SimilarityTool.CalculateWeight(tagW, desW);
                });
            }
            else
            {
                // 没有题目描述
                li = li.OrderByDescending(p => SimilarityTool.TagSim(p.Tags, tags));
            }

            _Logger.LogInformation("Successfully search!");
            return Ok(li);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> Count()
        {
            int count = await _Repository.GetCount();
            return Ok(new {count = count});
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllProblems()
        {
            return Ok(_Repository.GetAllProblems());
        }
    }
}