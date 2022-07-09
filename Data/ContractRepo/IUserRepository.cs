using Data.Repository;
using Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContractRepo
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserByFullNameandpassword(string fullName, string password,
            CancellationToken cancellationToken);
    }
}
