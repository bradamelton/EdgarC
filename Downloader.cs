using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreFtp;
using CoreFtp.Enum;

namespace EdgarC
{
    public class Downloader
    {
        private readonly ConfigurationAdapter _configurationAdapter;

        public Downloader(ConfigurationAdapter configurationAdapter)
        {
            _configurationAdapter = configurationAdapter;
        }

        private string IndexUrl(int year, int quarter)
        {
            return $"{_configurationAdapter.EdgarWebUrl}{_configurationAdapter.IndexDirectory}/{year}/QTR{quarter}/";
        }

        //https://www.sec.gov/Archives/edgar/data/1000275/0001140361-20-008680.txt
        // it will have the path relative to archives/

        public async Task<List<FormLink>> GetFormList(int year, int quarter)
        {
            var result = new List<FormLink>();

            try
            {
                var wc = new WebClient();
                var url = new Uri($"{IndexUrl(year, quarter)}master.idx");

                using (var stream = wc.OpenRead(url))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        var content = sr.ReadToEnd();

                        result = ParseMasterIdxFile(content);

                        //foreach (var r in results) { Console.WriteLine($"result: {r}"); }



                    }
                }

                // we should grab the index.json from the year/quarter folder.

                // then pull all the master.*.idx s in there.

                //https://www.sec.gov/Archives/edgar/full-index/2020/QTR2/index.json

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetFormList: {ex.ToString()}");
            }

            return result;
        }

        private static List<FormLink> ParseMasterIdxFile(string content)
        {
            var results = new List<FormLink>();

            foreach (var line in content.Split('\n'))
            {
                var parts = line.Split('|');

                try
                {
                    results.Add(new FormLink()
                    {
                        CompanyId = Int64.Parse(parts[0]),
                        CompanyName = parts[1],
                        FormType = parts[2],
                        Date = DateTime.Parse(parts[3]),
                        Path = parts[4]
                    });

                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Parse error for line {line}: {ex}");
					//Console.ReadKey();
                }
            }

            return results;
        }
    }

    public class FormLink
    {
        public Int64 CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string FormType { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"Company Id: {CompanyId}, Company Name: {CompanyName}, Form Type: {FormType}, Date: {Date}, Path: {Path}";
        }
    }
}