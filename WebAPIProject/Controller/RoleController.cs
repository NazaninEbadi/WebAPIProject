using Data.ContractRepo;
using Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        #region [- ctor -]
        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        #endregion

        #region [- async Task<ActionResult<List<Role>>> Get(CancellationToken cancellationToken) -]
        [HttpGet]
        public async Task<ActionResult<List<Role>>> Get(CancellationToken cancellationToken)
        {
            var roles = await roleRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(roles);
        }
        #endregion

        #region [- async Task<ActionResult<Role>> Get(int id, CancellationToken cancellationToken) -]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Role>> Get(int id, CancellationToken cancellationToken)
        {
            var role = await roleRepository.GetByIdAsync(cancellationToken, id);
            if (role == null)
                return NotFound();
            return role;
        }
        #endregion

        #region [- async Task Create(Role role) -]
        [HttpPost]
        public async Task Create(Role role)
        {
            await roleRepository.AddAsync(role, CancellationToken.None);
        } 
        #endregion

        #region [-  async Task<IActionResult> Update(int id, Role role) -]
        [HttpPut]
        public async Task<IActionResult> Update(int id, Role role)
        {
            var updatedRole = await roleRepository.GetByIdAsync(CancellationToken.None, id);
            updatedRole.Name = role.Name;
            updatedRole.Description = role.Description;

            await roleRepository.UpdateAsync(updatedRole, CancellationToken.None);
            return Ok();
        } 
        #endregion

        #region [- async Task<ActionResult> Delete(int id) -]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var role = await roleRepository.GetByIdAsync(CancellationToken.None, id);
            await roleRepository.DeleteAsync(role, CancellationToken.None);
            return Ok();

        } 
        #endregion
    }
}

