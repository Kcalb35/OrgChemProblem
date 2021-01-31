using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrgChemProblem.Models
{
    public interface IProblemRepository
    {
        Task<Problem> GetProblemByIdAsync(int id);
        IEnumerable<Problem> GetAllProblems();
        Task<Problem> UpdateProblemAsync(Problem updatedProblem);
        Task DeleteProblemByIdAsync(int id);
        Task<Problem> CreateProblemAsync(Problem problem);
        Task<int> GetCount();

    }
}