using System;

namespace EdgarC
{
    public class FormLink
    {
        public Int64 SECNumber { get; set; }
        public string CompanyName { get; set; }
        public string FormType { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public string SECDocumentNumber
        {
            get
            {
                var docNum = "";

                var s = Path.LastIndexOf("/") + 1;
                var e = Path.LastIndexOf(".");

                if (s >= 0 && e > s)
                {
                    docNum = Path.Substring(s, e - s);
                }

                return docNum;
            }
        }

        public override string ToString()
        {
            return $"Company Id: {SECNumber}, Company Name: {CompanyName}, Form Type: {FormType}, Date: {Date}, Path: {Path}, FullPath: {FullPath}";
        }

        public Company GetCompany()
        {
            return new Company()
            {
                SECNumber = SECNumber,
                Name = CompanyName
            };
        }
    }
}