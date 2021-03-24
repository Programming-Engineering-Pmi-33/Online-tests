using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageLayer.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Name { get; set; }

        public virtual List<Test> Tests { get; set; }
    }
}
