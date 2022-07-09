using Data.ContractRepo;
using Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        #region [- ctor -]
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        } 
        #endregion

        #region [- async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken) -]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        } 
        #endregion

        #region [- async Task<ActionResult<User>> Get(int id, CancellationToken cancellationToken) -]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();
            return user;
        } 
        #endregion

        #region [- async Task Create(User user) -]
        [HttpPost]
        public async Task Create(User user)
        {
            await userRepository.AddAsync(user, CancellationToken.None);
        } 
        #endregion

        #region [-  async Task<IActionResult> Update(int id, User user) -]
        [HttpPut]
        public async Task<IActionResult> Update(int id, User user)
        {
            var updatedUser = await userRepository.GetByIdAsync(CancellationToken.None, id);
            updatedUser.FullName = user.FullName;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Gender = user.Gender;
            updatedUser.Age = user.Age;
            updatedUser.IsActive = user.IsActive;
            updatedUser.LastLoginDate = user.LastLoginDate;
            await userRepository.UpdateAsync(updatedUser, CancellationToken.None);
            return Ok();
        } 
        #endregion

        #region [- async Task<ActionResult> Delete(int id) -]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userRepository.GetByIdAsync(CancellationToken.None, id);
            await userRepository.DeleteAsync(user, CancellationToken.None);
            return Ok();

        } 
        #endregion
    }
}

