using System;
using System.Collections.Generic;

namespace Scheduler.WebAPI.Library.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int HostUserId { get; set; }

        public User HostUser { get; set; }
    }
}
