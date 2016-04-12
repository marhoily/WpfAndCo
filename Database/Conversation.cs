using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Dpb.MessageDb
{
    [PublicAPI]
    public class Conversation
    {
        private ICollection<Message> _messages;

        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, MaxLength(50)]
        public string Sender { get; set; }
        [Required, MaxLength(50)]
        public string Receiver { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Message> Messages
        {
            get { return _messages ?? (_messages = new HashSet<Message>()); } 
        }
    }
}