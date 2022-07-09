using Common.Utilities;
using Data.ContractRepo;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task <User> GetUserByFullNameandpassword(string userName, string password,CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256(password);
            return await Table.Where(p => p.FullName == userName && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
