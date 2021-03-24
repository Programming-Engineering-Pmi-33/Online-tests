using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageLayer.Models
{
    public class TestSummary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OwnerUserId { get; set; }

        [Required]
        public int TestId { get; set; }

        [Required, Range(0, 100)]
        public int Points { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual Test Test { get; set; }

        [Required]
        public int OwnerUserId { get; set; }

        public virtual User OwnerUser { get; set; }
    }
}
