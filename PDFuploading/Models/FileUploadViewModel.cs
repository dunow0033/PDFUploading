using System.ComponentModel.DataAnnotations;

namespace PDFuploading.Models
{
    public class FileUploadViewModel
    {
        [Required]
        [Display(Name ="Upload File")]
        public IFormFile File { get; set; }
    }
}
