using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EdgarC
{
    public class Downloader
    {
        private readonly ConfigurationAdapter _configurationAdapter;
        private readonly IFileProvider _fileProvider;

        public Downloader(ConfigurationAdapter configurationAdapter, IFileProvider fileProvider)
        {
            _configurationAdapter = configurationAdapter;
            _fileProvider = fileProvider;
        }

        private string IndexUrl(int year, int quarter)
        {
            return $"{_configurationAdapter.EdgarWebUrl}{_configurationAdapter.IndexDirectory}/{year}/QTR{quarter}/";
        }

        public async Task<List<FormLink>> GetFormList(int year, int quarter)
        {
            var result = new List<FormLink>();

            var content = _fileProvider.GetTextFile($"{IndexUrl(year, quarter)}master.idx");
            result = ParseMasterIdxFile(content);

            result.ForEach(r => r.FullPath = $"{_configurationAdapter.EdgarWebUrl}/{r.Path}");

            return result;
        }

        private static List<FormLink> ParseMasterIdxFile(string content)
        {
            var results = new List<FormLink>();

            foreach (var line in content.Split('\n'))
            {
                var parts = line.Split('|');

                if (parts.Length == 5)
                {
                    try
                    {
                        results.Add(new FormLink()
                        {
                            CompanyId = parts[0],
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
            }

            return results;
        }
    }
}