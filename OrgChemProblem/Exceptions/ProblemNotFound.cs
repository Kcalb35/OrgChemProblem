using System;

namespace OrgChemProblem.Exceptions
{
    public class ProblemNotFound : Exception

    {
        public ProblemNotFound(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}