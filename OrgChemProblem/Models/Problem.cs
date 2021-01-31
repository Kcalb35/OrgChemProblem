using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrgChemProblem.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string ProblemDescription { get; set; }
        public string ProblemPicture { get; set; }
        public string AnswerDescription { get; set; }
        public string AnswerPicture { get; set; }

        [Required] public List<string> Tags { get; set; }
    }
}