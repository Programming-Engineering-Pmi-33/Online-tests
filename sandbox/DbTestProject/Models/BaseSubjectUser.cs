using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorageLayer.Models
{
    public class BaseSubjectUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual BaseUser User { get; set; }

        [Required]
        public int SubjectId { get; set; }
        
        public Subject Subject { get; set; }
    }
}
