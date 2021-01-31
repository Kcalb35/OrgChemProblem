using System;
using System.Collections.Generic;
using System.Linq;

namespace OrgChemProblem.Utils
{
    public class SimilarityTool
    {
        public static double LDistanceSim(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1)) return 0;
            if (string.IsNullOrEmpty(str2)) return 0;
            var m = new int[str1.Length + 1, str2.Length + 1];
            for (var i = 0; i < str2.Length + 1; i++) m[0, i] = i;

            for (var i = 0; i < str1.Length + 1; i++) m[i, 0] = i;

            for (var i = 1; i < str1.Length + 1; i++)
            for (var j = 1; j < str2.Length + 1; j++)
            {
                var tmp = str1[i - 1] == str2[j - 1] ? 0 : 1;
                m[i, j] = Min(m[i - 1, j] + 1, m[i, j - 1] + 1, m[i - 1, j - 1] + tmp);
            }

            return 1 - 1.0 * m[str1.Length, str2.Length] / Math.Max(str1.Length, str2.Length);
        }


        private static int Min(int a, int b, int c)
        {
            var min2 = Math.Min(a, b);
            return Math.Min(min2, c);
        }

        public static double TagSim(IEnumerable<string> target, IEnumerable<string> query)
        {
            var count = query.Count();
            var sameCount = query.Count(tag => target.Contains(tag));
            return 1.0 * sameCount / count;
        }

        public static double CalculateWeight(double tagWeight, double descriptionWeight)
        {
            return tagWeight * 0.4 + descriptionWeight * 0.6;
        }
    }
}