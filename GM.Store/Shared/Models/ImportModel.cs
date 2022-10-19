using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Store.Shared.Models
{
    public class UploadedFile
    {
        public int ApplicationId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string? LocalId { get; set; }
        public int BaseCategoryId { get; set; }
    }
    public class ProductImportModel
    {
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string ParentCategory { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }
        public string SellingPrice { get; set; }
        public string Stock { get; set; }
        public string Gst { get; set; }
        public string LPR { get; set; }
        public string Cess { get; set; }
        public string Hsn { get; set; }
    }
}
