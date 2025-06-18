using Backend_.Net.DTO;
using Backend_.Net.Entities;

namespace Backend_.Net.Service
{
    public interface ICommentService
    {
        Task<Comment> AddCommentAsync(CommentDTO dto);
        Task<List<Comment>> GetCommentsByDesignIdAsync(Guid designId);
        Task<Comment?> UpdateCommentAsync(Guid commentId, string newMessage);
        Task<bool> DeleteCommentAsync(Guid commentId);
    }
}
