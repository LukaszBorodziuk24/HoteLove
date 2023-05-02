namespace HoteLove.Models
{
    public class ApplicationUser
    {
        public ApplicationUser(string id, string email)
        {
            Id = id;
            Email = email;
        }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
