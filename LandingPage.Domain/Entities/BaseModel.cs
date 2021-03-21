using System;
using System.Collections.Generic;
using System.Text;

namespace LandingPage.Domain.Entities
{
    public class BaseModel
    {

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? CreateUserId { get; set; }
        public Guid? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid? DeletedUserId { get; set; }
    }
}
