using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.API.Controllers.Post;

[Authorize]
[Route("api/v1/post")]
[ApiController]
public class PostController : ControllerBase
{
    private IPostService postService;

    public PostController(IPostService postService)
    {
        this.postService = postService;
    }

    [HttpGet("rec")]
    public async Task<IActionResult> GetRecommendedAsync(CancellationToken ct)
    {
        try
        {
            var token = new AuthentificationToken() { Value = Request.Headers.Authorization };
            var post = await postService.GetRecommendedAsync(token, ct);
            var response = post.Adapt<PostGetRecommendedResponseContract>();
            response.Tags = post.Tags.Select(x => x.Adapt<PostTagsContract>());
            return Ok(ResponseContract<PostGetRecommendedResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<PostGetRecommendedResponseContract>.Create(ex));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id, CancellationToken ct)
    {
        try
        {
            var token = new AuthentificationToken() { Value = Request.Headers.Authorization };
            var post = await postService.GetById(token, id, ct);
            var response = post.Adapt<PostGetByIdResponseContract>();
            response.Tags = post.Tags.Select(x => x.Adapt<PostTagsContract>());
            return Ok(ResponseContract<PostGetByIdResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<PostGetByIdResponseContract>.Create(ex));
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] PostCreateRequestContract request, CancellationToken ct)
    {
        try
        {
            var token = new AuthentificationToken() { Value = Request.Headers.Authorization };
            var post = await postService.CreateAsync(token, request.SportId, request.CourseId, request.Content, ct);
            var response = post.Adapt<PostCreateResponseContract>();
            response.Tags = post.Tags.Select(x => x.Adapt<PostTagsContract>());
            return Ok(ResponseContract<PostCreateResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<PostCreateResponseContract>.Create(ex));
        }
    }
}