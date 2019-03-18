using System.ComponentModel.DataAnnotations;
using System.Web;

namespace imageFilter.Models
{
    public class FileModel
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Select files to Stitch :")]
        public HttpPostedFileBase[] files { get; set; }

    }
}
