using Backend_.Net.Data;
using Backend_.Net.DTO;
using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Service
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> AddCommentAsync(CommentDTO dto)
        {
            var comment = new Comment
            {
                UserID = dto.UserId,
                Message = dto.Message,
                DateTime = DateTime.UtcNow
            };

            var designComment = new DesignComment
            {
                CommentId = comment.Id,
                Comment = comment,
                DesignId = dto.DesignId
            };

            _context.Comments.Add(comment);
            _context.DesignComments.Add(designComment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _context.Comments
           .Include(c => c.DesignComment)
           .FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null) return false;

            // Remove from junction table
            _context.DesignComments.RemoveRange(comment.DesignComment);
            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetCommentsByDesignIdAsync(Guid designId)
        {
            return await _context.DesignComments
           .Where(dc => dc.DesignId == designId)
           .Select(dc => dc.Comment)
           .Include(c => c.User)
           .ToListAsync();
        }

        public async Task<Comment?> UpdateCommentAsync(Guid commentId, string newMessage)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null) return null;

            comment.Message = newMessage;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
