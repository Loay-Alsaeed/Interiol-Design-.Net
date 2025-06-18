using Backend_.Net.DTO;
using Backend_.Net.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO dto)
        {
            var comment = await _commentService.AddCommentAsync(dto);
            return Ok(comment);
        }

        [HttpGet("design/{designId}")]
        public async Task<IActionResult> GetComments(Guid designId)
        {
            var comments = await _commentService.GetCommentsByDesignIdAsync(designId);
            return Ok(comments);
        }
        
        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(Guid commentId, [FromBody] string newMessage)
        {
            var success = await _commentService.UpdateCommentAsync(commentId, newMessage);
            return success is null ? NotFound() : Ok();
        }
        
        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var success = await _commentService.DeleteCommentAsync(commentId);
            return success ? Ok() : NotFound();
        }

    }
}
