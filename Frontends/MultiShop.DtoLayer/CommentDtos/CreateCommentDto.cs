using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CommentDtos
{
    public class CreateCommentDto
    {
        public string ProductId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Detail { get; set; }
        public string? ImageUrl { get; set; }
        public int Raiting { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
