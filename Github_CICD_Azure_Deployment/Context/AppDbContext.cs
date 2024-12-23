using Github_CICD_Azure_Deployment.Models;
using Microsoft.EntityFrameworkCore;

namespace Github_CICD_Azure_Deployment.Context {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
