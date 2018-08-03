namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;


    public class Team
    {
        public Team()
        {
            this.UserTeams = new HashSet<UserTeam>();
            this.TeamEvents = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [Range(3,3)]
        public string Acronym { get; set; }

        public int CretorId { get; set; }
        public User Creator { get; set; }

        public ICollection<TeamEvent> TeamEvents { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Invitation> Invitations { get; set; }


    }
}
