using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrgChemProblem.Exceptions;

namespace OrgChemProblem.Models
{
    public class ProblemsRepository : IProblemRepository
    {
        private readonly ProblemDbcontext _Context;

        public ProblemsRepository(ProblemDbcontext dbcontext)
        {
            _Context = dbcontext;
        }

        public async Task<Problem> GetProblemByIdAsync(int id)
        {
            var problem = await _Context.Problems.FirstOrDefaultAsync(p => p.Id == id);
            if (problem == null) throw new ProblemNotFound(id);
            return problem;
        }


        public IEnumerable<Problem> GetAllProblems()
        {
            return _Context.Problems.AsEnumerable();
        }

        public async Task<Problem> UpdateProblemAsync(Problem updatedProblem)
        {
            var problem = await GetProblemByIdAsync(updatedProblem.Id);
            problem.ProblemDescription = updatedProblem.ProblemDescription;
            problem.ProblemPicture = updatedProblem.ProblemPicture;
            problem.AnswerDescription = updatedProblem.AnswerDescription;
            problem.AnswerPicture = updatedProblem.AnswerPicture;

            _Context.Problems.Update(problem);
            await _Context.SaveChangesAsync();
            return problem;
        }

        public async Task DeleteProblemByIdAsync(int id)
        {
            var problem = await GetProblemByIdAsync(id);
            _Context.Problems.Remove(problem);
            await _Context.SaveChangesAsync();
        }

        public async Task<Problem> CreateProblemAsync(Problem problem)
        {
            _Context.Problems.Add(problem);
            await _Context.SaveChangesAsync();
            return problem;
        }

        public async Task<int> GetCount()
        {
            return await _Context.Problems.CountAsync();
        }

    }
}