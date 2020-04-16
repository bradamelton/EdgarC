using System;

namespace EdgarC
{
    public class FormLink
    {
        public object CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string FormType { get; set; }
        public DateTime Date { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }

        public override string ToString()
        {
            return $"Company Id: {CompanyId}, Company Name: {CompanyName}, Form Type: {FormType}, Date: {Date}, Path: {Path}, FullPath: {FullPath}";
        }
    }
}