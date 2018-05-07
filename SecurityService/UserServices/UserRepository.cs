using System.Collections.Generic;
using System.Linq;
using System;

namespace SecurityService.UserServices
{
    public class UserRepository : IUserRepository
    {
        private readonly List<TaskUser> _users = new List<TaskUser>
        {
            new TaskUser{
                SubjectId = "123",
                UserName = "postmanadmin",
                Password = "postmanadmin",
                Email = "postmanadmin@email.ch"
            },
             new TaskUser{
                SubjectId = "456",
                UserName = "postmanuser",
                Password = "postmanuser",
                Email = "postmanuser@email.ch"
            }
        };

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }

        public TaskUser FindBySubjectId(string subjectId)
        {
            return _users.FirstOrDefault(x => x.SubjectId == subjectId);
        }

        public TaskUser FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
