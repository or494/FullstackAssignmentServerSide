using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class QueuePlace
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EnqueuedAt { get; set; }
        public DateTime EnteredAt { get; set; }
        public bool IsCurrent { get; set; }
    }
}
