using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_course_web.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public int LikeCount { get; set; }

        // Define the foreign key for self-referencing relationship
        [ForeignKey("ParentComment")]
        public int? ParentCommentId { get; set; }

        // Define navigation property for self-referencing relationship
        public virtual Comment ParentComment { get; set; }

        // Define navigation property for child comments
        public virtual ICollection<Comment> ChildComments { get; set; }
    }
}
