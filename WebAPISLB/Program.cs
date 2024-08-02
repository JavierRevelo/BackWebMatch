using Microsoft.EntityFrameworkCore;
using WebAPISLB.Context;

namespace WebAPISLB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectioString = builder.Configuration.GetConnectionString("DBString");
            // Registrar servicio para la conexión 
            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectioString));

            // Agregar soporte para CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()  // Permitir solicitudes desde cualquier origen
                               .AllowAnyMethod()  // Permitir cualquier método (GET, POST, etc.)
                               .AllowAnyHeader(); // Permitir cualquier encabezado
                    });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Aplicar la política CORS
            app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
