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
			return $"{_configurationAdapter.EdgarWebUrl}{_configurationAdapter.IndexDirectory}/{year}/QTR{quarter}/{_configurationAdapter.IndexFile}";
		}

		private string ArchiveQuarterUrl(int year, int quarter)
		{
			return $"{IndexUrl(year,quarter)}{_configurationAdapter.IndexFile}";
		}

		//https://www.sec.gov/Archives/edgar/data/1000275/0001140361-20-008680.txt
		// it will have the path relative to archives/

		public async Task<List<FormLink>> GetFormList(int year, int quarter)
		{
			var result = new List<FormLink>();

			try
			{
				var wc = new WebClient();
				var url = new Uri(IndexUrl(year, quarter));

				using(var stream = wc.OpenRead(url))
				{
					using(var sr = new StreamReader(stream))
					{

						var content = sr.ReadToEnd();

						Console.WriteLine($"content: {content}");

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

	}

	public class FormLink
	{
		public Int64 CompanyId { get; set; }
		public string CompanyName { get; set; }
		public string FormType { get; set; }
		public DateTime Date { get; set; }
		public string Path { get; set; }

	}

}