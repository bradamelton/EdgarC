using System;
using DBu;

namespace EdgarC
{
    [DatabaseName("EdgarC")]
    public class Form : DBuClass
    {
        public string SECDocumentNumber { get; set; }
        public object CompanyId { get; set; }
        public string Content { get; set; }
        public string FormType { get; set; }
        public string FullPath { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}