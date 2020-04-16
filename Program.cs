using System;
using System.Linq;
using System.Collections.Generic;

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

            var downloader = new Downloader(new ConfigurationAdapter(), new FileProvider());

            var results = downloader.GetFormList(2020, 2).Result;

            //results.Select(r => r.CompanyName).Distinct().OrderBy(c => c).ToList().ForEach(c => Console.WriteLine($"{c}"));

            // save companies and forms?

            results.ForEach(r =>
            {
                var c = new Company();
                c.Load(new Dictionary<string, object>() { new })
            });




            //foreach (var r in results) { Console.WriteLine($"result: {r}"); }


            // build and or update comprehensive list of available files and records.

            // need to compare which ones we have and which ones not.

            // maybe compare filecounts in the directories...
            /// that can be the checksum for now.



        }
    }
}