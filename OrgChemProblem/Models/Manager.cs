using System.ComponentModel.DataAnnotations;

namespace OrgChemProblem.Models
{
    public class Manager
    {
        public int Id { get; set; }

        [Required] public string UserName { get; set; }

        [Required] public string HashPassword { get; set; }
    }
}