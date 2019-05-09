using Altkom.UTC.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UTC.Core.IServices
{
    public interface IUsersService : IEntiesService<User>
    {
        User Authenticate(string login, string password);

    }
}
