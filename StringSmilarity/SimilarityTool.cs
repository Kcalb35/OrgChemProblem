using System;

namespace StringSmilarity
{
    public static class SimilarityTool
    {
        public static double LDistance(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1)) return 0;
            if (string.IsNullOrEmpty(str2)) return 0;
            int[,] m=new int[str1.Length+1,str2.Length+1];
            for (int i = 0; i < str2.Length+1; i++)
            {
                m[0, i] = i;
            }

            for (int i = 0; i < str1.Length+1; i++)
            {
                m[i, 0] = i;
            }

            for (int i = 1; i < str1.Length+1; i++)
            {
                for (int j = 1; j < str2.Length+1; j++)
                {
                    int tmp = str1[i-1] == str2[j-1] ? 0 : 1;
                    m[i, j] = Min(m[i - 1, j] + 1, m[i, j - 1] + 1, m[i - 1, j - 1] + tmp);
                } 
            }

            return 1 - 1.0* m[str1.Length , str2.Length ]/Math.Max(str1.Length,str2.Length);
        }


        

        private static int Min(int a, int b, int c)
        {
            int min2 = Math.Min(a, b);
            return Math.Min(min2, c);
        }
    }
}