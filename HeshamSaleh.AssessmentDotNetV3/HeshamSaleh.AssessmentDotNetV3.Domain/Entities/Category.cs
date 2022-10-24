using HeshamSaleh.AssessmentDotNetV3.Domain.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace HeshamSaleh.AssessmentDotNetV3.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
