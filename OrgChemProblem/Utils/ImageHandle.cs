using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OrgChemProblem.Utils
{
    public static class ImageHandle
    {
        public static string SaveImg(IFormFile file)
        {
            string filename = null;
            if (file != null)
            {
                filename = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                var path = Path.Combine("Images", filename);
                file.CopyTo(new FileStream(path, FileMode.OpenOrCreate));
            }

            return filename;
        }
    }
}