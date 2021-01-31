using System;

namespace OrgChemProblem.Exceptions
{
    public class IllegalPageException : Exception
    {
        public IllegalPageException(int page, int count)
        {
            Page = page;
            Count = count;
        }

        public int Page { get; set; }
        public int Count { get; set; }
    }
}