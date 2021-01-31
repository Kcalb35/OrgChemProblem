using System.Threading.Tasks;
using OrgChemProblem.Controllers;

namespace OrgChemProblem.Models
{
    public interface IAuthenticate
    {
        Task<(bool, string)> IsAuthenticated(LoginReq req);
    }
}