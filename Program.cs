using FilmesAPI.Data;
using FilmesAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<FilmeServices, FilmeServices>();
            builder.Services.AddScoped<CinemaServices, CinemaServices>();
            builder.Services.AddScoped<EnderecoServices, EnderecoServices>();
            builder.Services.AddScoped<GerenteServices, GerenteServices>();
            builder.Services.AddScoped<SessaoServices, SessaoServices>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectString = builder.Configuration["ConnectionStrings:UsuarioConnection"];
            builder.Services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(connectString));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
