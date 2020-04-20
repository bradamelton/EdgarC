using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using log4net;
using log4net.Core;
using log4net.Config;

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
            var logRepository = LogManager.GetRepository("EdgarC");
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            ILogger _log = logRepository.GetLogger("console");
            ILogger _ui = logRepository.GetLogger("ui");


            var downloader = new Downloader(new ConfigurationAdapter(), new FileProvider(), _log);

            var em = new EdgarCManager(downloader, _log, _ui);




            em.DownloadQuarterCurrent();

            // build and or update comprehensive list of available files and records.

            // need to compare which ones we have and which ones not.

            // maybe compare filecounts in the directories...
            /// that can be the checksum for now.

        }
    }
}