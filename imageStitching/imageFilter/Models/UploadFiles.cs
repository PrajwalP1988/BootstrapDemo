using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imageFilter.Models
{
    public partial class UploadFiles
    {
    }

    public class UploadsViewModel
    {
        public long ID { get; set; }
        public List<myFile> Uploads { get; set; }
        public UploadsViewModel()
        {
            this.Uploads = new List<myFile>();
        }
    }
    public class myFile
    {
        public string FilePath { get; set; }
        public long FileID { get; set; }
        public string FileName { get; set; }
        public Guid GuidID { get; set; }
    }
}