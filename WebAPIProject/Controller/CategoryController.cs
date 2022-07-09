using Data.ContractRepo;
using Entities.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        #region [- ctor -]
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        #endregion

        #region [-  async Task<ActionResult<List<Category>>> Get(CancellationToken cancellationToken) -]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get(CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(categories);
        }
        #endregion

        #region [- async Task<ActionResult<Category>> Get(int id, CancellationToken cancellationToken) -]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(cancellationToken, id);
            if (category == null)
                return NotFound();
            return category;
        }
        #endregion

        #region [- async Task Create(Category category) -]
        [HttpPost]
        public async Task Create(Category category)
        {
            await categoryRepository.AddAsync(category, CancellationToken.None);
        }
        #endregion

        #region [-  async Task<IActionResult> Update(int id, Category category) -]
        [HttpPut]
        public async Task<IActionResult> Update(int id, Category category)
        {
            var updatedCategory = await categoryRepository.GetByIdAsync(CancellationToken.None, id);
            updatedCategory.Name = category.Name;
            updatedCategory.ParentCategoryId = category.ParentCategoryId;
            updatedCategory.CreatedById = category.CreatedById;
            await categoryRepository.UpdateAsync(updatedCategory, CancellationToken.None);
            return Ok();
        } 
        #endregion

        #region [- async Task<ActionResult> Delete(int id) -]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetByIdAsync(CancellationToken.None, id);
            await categoryRepository.DeleteAsync(category, CancellationToken.None);
            return Ok();

        } 
        #endregion
    }
}

