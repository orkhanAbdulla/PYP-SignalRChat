using Microsoft.EntityFrameworkCore;
using PYP_SignalRPrictice.Models;

namespace PYP_SignalRPrictice.DAL
{
    public class ChatContext:DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options):base(options){}
        public DbSet<User> Users { get; set; } = null!;

    }
}
