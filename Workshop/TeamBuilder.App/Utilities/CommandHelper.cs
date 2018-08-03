using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public static class CommandHelper
    {

        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(x => x.Name == teamName);
            }
        }
        public static bool IsUserExisting(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.Where(x => x.IsDeleted == false).Any(x => x.Username == username);
            }
        }
       public static bool IsInviteExisting(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Invitations
                    .Any(x => x.Team.Name == teamName && x.InvitedUserId == user.Id && x.IsActive);
            }
        }
        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return true;
            }
        }
        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {

                return context.Events.SingleOrDefault(x => x.Name == eventName).Creator == user;
            }
        }
        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams
                    .Single(x => x.Name == teamName)
                    .UserTeams.Any(ut => ut.User.Username == username);
            }
        }
        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(x => x.Name == eventName);
            }
        }
    }
}
