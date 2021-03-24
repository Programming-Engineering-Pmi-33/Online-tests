using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageLayer.Models
{
    public class TestItem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public int Order { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Question { get; set; }

        public virtual List<TestItemOption> TestItemOptions { get; set; }
    }
}
