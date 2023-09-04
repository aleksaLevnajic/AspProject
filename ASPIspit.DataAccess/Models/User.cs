using System;
using System.Collections.Generic;

#nullable disable

namespace ASPIspit.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            UserUseCases = new HashSet<UserUseCase>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserUseCase> UserUseCases { get; set; }
    }
}
