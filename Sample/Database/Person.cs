using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Sample
{
    public class Person
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public long CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
    }
}