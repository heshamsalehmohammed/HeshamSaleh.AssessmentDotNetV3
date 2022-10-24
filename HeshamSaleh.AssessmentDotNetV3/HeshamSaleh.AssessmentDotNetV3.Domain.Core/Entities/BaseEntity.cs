using Flunt.Notifications;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeshamSaleh.AssessmentDotNetV3.Domain.Core.Entities
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        [Key]
        public Guid Id { get; set; }

        protected BaseEntity() => Id = Guid.NewGuid();
    }
}
