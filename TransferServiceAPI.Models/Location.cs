using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TransferServiceAPI.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int LocationCategoryId { get; set; }

        [ForeignKey("LocationCategoryId")]
        [ValidateNever]
        public LocationCategory? LocationCategory { get; set; }
    }
}