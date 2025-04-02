using System.ComponentModel.DataAnnotations;

namespace ElderlyCareApp.Models
{
    public class UploadDataModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Link is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string Link { get; set; }
    }
}