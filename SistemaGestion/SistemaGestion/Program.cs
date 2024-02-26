using Microsoft.EntityFrameworkCore;
using SistemaGestionBussiness;
using SistemaGestionData;

namespace SistemaGestion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddScoped<ProductoBussiness>();
            //builder.Services.AddScoped<UsuarioBussiness>();
            //builder.Services.AddScoped<VentaBussiness>();
            //builder.Services.AddScoped<ProductoVendidoBussiness>();

            builder.Services.AddDbContext<CoderContext>(options =>
            {
                options.UseSqlServer("Server=.; Database=coderhouseTEST; Trusted_Connection=True;");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
