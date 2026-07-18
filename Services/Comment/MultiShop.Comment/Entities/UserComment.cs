namespace MultiShop.Comment.Entities
{
    public class UserComment
    {
        public int UserCommentId { get; set; }
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
