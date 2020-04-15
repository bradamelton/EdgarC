using System;
using System.Linq;

namespace EdgarC
{
    class Program
    {
        static void Main(string[] args)
        {

            // want to get all companies numbers
            // need to link these to another db.

            // do'nt need to do that immediately. It's not as applicable to what I'm trying to do here.

            // load a range of indexes. (Just daily master.)

            var downloader = new Downloader(new ConfigurationAdapter());

            var results = downloader.GetFormList(2020, 2).Result;

            results.Select(r => r.CompanyName).Distinct().OrderBy(c => c).ToList().ForEach(c => Console.WriteLine($"{c}"));

            //foreach (var r in results) { Console.WriteLine($"result: {r}"); }
        }
    }
}