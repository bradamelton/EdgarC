using System;

namespace EdgarC
{
	public class ConfigurationAdapter
	{
		public string EdgarWebUrl => "https://www.sec.gov/Archives";

		public string IndexDirectory => "/edgar/full-index";
		public string IndexFile => "index.json";
	}
}