using System;

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

            downloader.GetFormList(2020, 2).Wait();

        }
    }
}