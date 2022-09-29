namespace PYP_SignalRPrictice.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? ConnectionId { get; set; }   
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
