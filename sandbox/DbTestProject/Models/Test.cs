using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageLayer.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool IsPrivate { get; set; }

        [MinLength(1), MaxLength(50)]
        public string KeyWord { get; set; }

         public virtual List<TestSummary> TestSummaries { get; set; }
    }
}
