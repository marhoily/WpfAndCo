using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample
{
    public class City
    {
        private ICollection<Person> _messages;

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Person> People
        {
            get { return _messages ?? (_messages = new HashSet<Person>()); } 
        }
    }
}