using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Sample
{
    [DebuggerDisplay("{Text} | {Sender} -> {Receiver}")]
    public class TextMessage
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Number { get; set; }

        [Required, MaxLength(100)]
        public string Sender { get; set; }
        [Required, MaxLength(100)]
        public string Receiver { get; set; }

        [Required, MaxLength(10*1024)]
        public string Text { get; set; }

        public DateTime Created { get; set; }
        public DateTime Saved { get; set; }
        public DateTime? Read { get; set; }

        public long ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
    }
}