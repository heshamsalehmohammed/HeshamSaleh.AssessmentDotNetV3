using HeshamSaleh.AssessmentDotNetV3.Domain.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeshamSaleh.AssessmentDotNetV3.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string ImgURL { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
