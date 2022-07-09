using Data.ContractRepo;
using Data.Repository;
using Entities.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
       
        private readonly IPostRepository postRepository;

        #region [- ctor -]
        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        #endregion

        #region [- async Task<ActionResult<List<Post>>> Get(CancellationToken cancellationToken) -]
        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get(CancellationToken cancellationToken)
        {
            var posts = await postRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(posts);
        }
        #endregion

        #region [- async Task<ActionResult<Post>> Get(int id, CancellationToken cancellationToken) -]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> Get(int id, CancellationToken cancellationToken)
        {
            var post = await postRepository.GetByIdAsync(cancellationToken, id);
            if (post == null)
                return NotFound();
            return post;
        }
        #endregion

        #region [- async Task Create(Post post) -]
        [HttpPost]
        public async Task Create(Post post)
        {
            await postRepository.AddAsync(post, CancellationToken.None);
        }
        #endregion

        #region [- async Task<IActionResult> Update(int id, Post post) -]
        [HttpPut]
        public async Task<IActionResult> Update(int id, Post post)
        {
            var updatedPost = await postRepository.GetByIdAsync(CancellationToken.None, id);
            updatedPost.Title = post.Title;
            updatedPost.Description = post.Description;
            updatedPost.CreatedBy = post.CreatedBy;
            updatedPost.CategoryId = post.CategoryId;
            await postRepository.UpdateAsync(updatedPost, CancellationToken.None);
            return Ok();
        }
        #endregion

        #region [- async Task<ActionResult> Delete(int id) -]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var post = await postRepository.GetByIdAsync(CancellationToken.None, id);
            await postRepository.DeleteAsync(post, CancellationToken.None);
            return Ok();

        } 
        #endregion
    }
}

