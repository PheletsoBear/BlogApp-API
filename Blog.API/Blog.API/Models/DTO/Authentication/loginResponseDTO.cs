namespace Blog.API.Models.DTO.Authentication
{
    public class loginResponseDTO
    {
        public string  Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
