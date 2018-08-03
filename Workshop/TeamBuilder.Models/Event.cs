namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Event
    {
        public Event()
        {
            this.ParticipatingEventTeams = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public  ICollection<TeamEvent> ParticipatingEventTeams { get; set; }
    }
}
