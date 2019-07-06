using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PDX.BLL.Model
{
    public class Document: PDX.Domain.Document.Document
    {
        [NotMapped]
        public virtual IFormFile File { get; set; }

        [NotMapped]
        public string TempFolderName { get; set; }
        [NotMapped]
        public string TempFileName { get; set; }

    }
}