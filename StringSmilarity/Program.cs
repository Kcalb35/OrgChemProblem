using System;

namespace StringSmilarity
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = SimilarityTool.LDistance("11ssss", "12234512321334251");
            Console.WriteLine(e);
        }
    }
}