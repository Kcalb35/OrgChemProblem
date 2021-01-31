#nullable enable
using Microsoft.AspNetCore.Http;

namespace OrgChemProblem.Controllers
{
    public record ProblemUpload(
        int? Id, string? ProblemDescription, IFormFile? ProblemImg, string? AnsDescription, IFormFile? AnsImg,
        string Tags);

    public record ProblemSearch(string? ProblemDescription, string Tags);
}