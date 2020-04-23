using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Core;

namespace EdgarC
{
    public class EdgarCManager
    {

        private readonly IDownloader _downloader;
        private readonly ILog _log;
        private readonly ILog _ui;

        public EdgarCManager(IDownloader downloader, ILog log, ILog ui)
        {
            _downloader = downloader;
            _log = log;
            _ui = ui;
        }

        public void DownloadQuarterCurrent()
        {
            var month = DateTime.Now.Month;
            var quarter = month >= 1 && month <= 3 ? 1 : month >= 4 && month <= 6 ? 2 : month >= 7 && month <= 9 ? 3 : 4;
            this.DownloadQuarter(DateTime.Now.Year, quarter);
        }

        public void DownloadQuarter(int year, int quarter)
        {

            var results = _downloader.GetFormList(2020, 2).Result;

            var companies = new List<Company>();

            foreach (var r in results.Select(r => r.SECNumber).Distinct())
            {
                var c = new Company();
                if (!c.Load(new Dictionary<string, object>() { { "SECNumber", r } }))
                {
                    c.Save();
                    _ui.Info("+");
                }
                else
                {
                    _ui.Info(".");
                }

                companies.Add(c);
            }

            // save companies and forms?
            _ui.Info("Companies complete.");

            results.ForEach(r =>
            {
                _ui.Info($"result: {r}");
                // save each record.

                var c = companies.First(c1 => c1.SECNumber == r.SECNumber);

                var f = new Form()
                {
                    SECDocumentNumber = r.SECDocumentNumber,
                    CompanyId = c.Id,
                    FormType = r.FormType,
                    FullPath = r.FullPath
                };

                if (!f.Load(new Dictionary<string, object>() { { "SECDocumentNumber", r.SECDocumentNumber } }))
                {
                    f.Save();
                    _ui.Info("+");
                }
                else
                {
                    _ui.Info(".");
                }

            });

        }

        public List<Company> CompanySearch(string search)
        {
            return Company.GetQueryable<Company>().Where(c => c.Name.Contains(search)).ToList();
        }
    }
}
