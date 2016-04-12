using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample
{
    public class Conversation
    {
        private ICollection<TextMessage> _messages;

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, MaxLength(50)]
        public string Sender { get; set; }
        [Required, MaxLength(50)]
        public string Receiver { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<TextMessage> Messages
        {
            get { return _messages ?? (_messages = new HashSet<TextMessage>()); } 
        }
    }
}