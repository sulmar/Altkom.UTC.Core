using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altkom.UTC.Core.DbServices
{
    public class DbUsersService : DbEntitiesService<User>, IUsersService
    {
        public DbUsersService(UTCContext context) : base(context)
        {
        }

        public User Authenticate(string login, string password)
        {
            //return context.Users
            //    .SingleOrDefault(u => u.Login == login && u.Password == password);

            return context.Users
                .Where(u => u.Login == login)
                .Where(u => u.Password == password)
                .SingleOrDefault();
        }
    }
}
