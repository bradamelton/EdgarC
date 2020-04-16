using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EdgarC
{

    public interface IFileProvider
    {
        string GetTextFile(string path);
    }

    public class FileProvider : IFileProvider
    {
        public string GetTextFile(string path)
        {
            string content = null;

            try
            {
                var wc = new WebClient();
                var url = new Uri(path);

                using (var stream = wc.OpenRead(url))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetFormList: {ex.ToString()}");
            }

            return content;
        }
    }
}