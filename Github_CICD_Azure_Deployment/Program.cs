
using Github_CICD_Azure_Deployment.Context;
using Github_CICD_Azure_Deployment.Models;
using Microsoft.EntityFrameworkCore;

namespace Github_CICD_Azure_Deployment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(opt => {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/", () => "Hello World");

            app.MapGet("/api/todos", async (AppDbContext context) => Results.Ok(await context.Todos.ToListAsync()));
            app.MapPost("/api/todos", async (string name, AppDbContext context) => {

                var todo = new Todo() {
                    Name = name,
                };
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Results.Ok(todo);
            });

            app.Run();
        }
    }
}
