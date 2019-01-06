using System;
using System.Collections.Generic;

namespace Scheduler.WebAPI.Library.Models
{
    public partial class User
    {
        public User()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Event> Event { get; set; }
    }
}
