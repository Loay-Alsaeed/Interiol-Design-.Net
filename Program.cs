
using System.Text;
using Backend_.Net.Data;
using Backend_.Net.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend_.Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            Console.WriteLine("Connection string: " + builder.Configuration.GetConnectionString("DefaultConnection"));


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                    options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = builder.Configuration["AppSettings:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                            ValidateIssuerSigningKey = true
                        };

                    }
                );



            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IColorService, ColorService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IConsiderationService, ConsiderationService>();
            builder.Services.AddScoped<IDescriptionService, DescriptionService>();
            builder.Services.AddScoped<IMaterialService, MaterialService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<ILayoutImageService, LayoutImageService>();
            builder.Services.AddScoped<IKeyFeatureService, KeyFeatureService>();
            builder.Services.AddScoped<IStyleService, StyleService>();
            builder.Services.AddScoped<IDesignService, DesignService>();
            builder.Services.AddScoped < IDesignConceptService, DesignConceptService>();







            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://interial-design-frontend-react.vercel.app/")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("AllowFrontend");
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();
            app.MapGet("/", () => "Welcome to the API");


            app.Run();
        }
    }
}
