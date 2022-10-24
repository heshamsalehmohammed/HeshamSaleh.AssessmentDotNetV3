using HeshamSaleh.AssessmentDotNetV3.Domain.Core.Models;
using System;

namespace HeshamSaleh.AssessmentDotNetV3.Domain.Models
{
    public class ProductModel : BaseModel
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public int Quantity { get; set; }

        public string ImgURL { get; set; }

        public Guid CategoryId { get; set; }
    }
}
