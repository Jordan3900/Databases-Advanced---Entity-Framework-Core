namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TeamBuilder.Models.Enums;

    public class User
    {
        public User()
        {
            this.CreatedEvents = new HashSet<Event>();
            this.UserTeams = new HashSet<UserTeam>();
           // this.CreatedUserTeams = new HashSet<UserTeam>();
            this.ReceivedInvitations = new HashSet<Invitation>();
                
        }

        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Range(6, 30)]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Team> CreatorTeams { get; set; }

        public ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
