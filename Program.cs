using System;
using System.Collections.Generic;
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

            var downloader = new Downloader(new ConfigurationAdapter(), new FileProvider());

            var results = downloader.GetFormList(2020, 2).Result;

            //results.Select(r => r.CompanyName).Distinct().OrderBy(c => c).ToList().ForEach(c => Console.WriteLine($"{c}"));

            var companies = new List<Company>();

            foreach (var r in results.Select(r => r.SECNumber).Distinct())
            {
                var c = new Company();
                if (!c.Load(new Dictionary<string, object>() { { "SECNumber", r } }))
                {
                    c.Save();
                    Console.Write("+");
                }
                else
                {
                    Console.Write(".");
                }

                companies.Add(c);
            }

            // save companies and forms?
            Console.WriteLine("Companies complete.");

            results.ForEach(r =>
            {
                Console.WriteLine($"result: {r}");
                // save each record.

                var c = companies.First(c1 => c1.SECNumber == r.SECNumber);

                var f = new Form()
                {
                    SECDocumentNumber = r.SECDocumentNumber,
                    CompanyId = c.Id,
                    FormType = r.FormType,
                    FullPath = r.FullPath
                };

                if (!f.Load(new Dictionary<string, object>() { { "SECDocumentNumber", r } }))
                {
                    f.Save();
                    Console.Write("+");
                }
                else
                {
                    Console.Write(".");
                }

            });

            //foreach (var r in results) { Console.WriteLine($"result: {r}"); }

            // build and or update comprehensive list of available files and records.

            // need to compare which ones we have and which ones not.

            // maybe compare filecounts in the directories...
            /// that can be the checksum for now.

        }
    }
}