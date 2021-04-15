using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTests.Models
{
    public class TestItemOption
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int TestItemId { get; set; }

        public virtual TestItem TestItem { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
