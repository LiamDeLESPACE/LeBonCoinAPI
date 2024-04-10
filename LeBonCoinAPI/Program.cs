using LeBonCoinAPI.DataManager;
using LeBonCoinAPI.Models.Auth;
using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LeBonCoinAPI
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

            builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PhPpgAdmin")));
            builder.Services.AddScoped<IRepository<Admin>, AdminManager>();
            builder.Services.AddScoped<IRepository<Adresse>, AdresseManager>();
            builder.Services.AddScoped<IRepository<Annonce>, AnnonceManager>();
            builder.Services.AddScoped<IRepository<CarteBancaire>, CarteBancaireManager>();
            builder.Services.AddScoped<IRepositoryCommentaire<Commentaire>, CommentaireManager>();
            builder.Services.AddScoped<IRepositoryDepartement<Departement>, DepartementManager>();
            builder.Services.AddScoped<IRepository<Entreprise>, EntrepriseManager>();
            builder.Services.AddScoped<IRepository<Equipement>, EquipementManager>();
            builder.Services.AddScoped<IRepositoryFavoris<Favoris>, FavorisManager>();
            builder.Services.AddScoped<IRepository<Particulier>, ParticulierManager>();
            builder.Services.AddScoped<IRepositoryPhoto<Photo>, PhotoManager>();
            builder.Services.AddScoped<IRepositoryPossedeEquipement<PossedeEquipement>, PossedeEquipementManager>();
            builder.Services.AddScoped<IRepository<Profil>, ProfilManager>();
            builder.Services.AddScoped<IRepositoryReglement<Reglement>, ReglementManager>();
            builder.Services.AddScoped<IRepository<Reservation>, ReservationManager>();
            builder.Services.AddScoped<IRepository<SecteurActivite>, SecteurActiviteManager>();
            builder.Services.AddScoped<IRepositorySignale<Signale>, SignaleManager>();
            builder.Services.AddScoped<IRepository<TypeEquipement>, TypeEquipementManager>();
            builder.Services.AddScoped<IRepository<TypeLogement>, TypeLogementManager>();
            builder.Services.AddScoped<IRepositoryVille<Ville>, VilleManager>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.RequireHttpsMetadata = false;
                     options.SaveToken = true;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = builder.Configuration["Jwt:Issuer"],
                         ValidAudience = builder.Configuration["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                         ClockSkew = TimeSpan.Zero
                     };
                 });

            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.particulier, Policies.ParticulierPolicy());
                config.AddPolicy(Policies.entreprise, Policies.EntreprisePolicy());
                config.AddPolicy(Policies.particulier+","+ Policies.admin, Policies.HumanPolicy());
                config.AddPolicy(Policies.entreprise + "," + Policies.admin, Policies.DirectorPolicy());
                config.AddPolicy(Policies.entreprise + "," + Policies.particulier + "," + Policies.admin, Policies.AllPolicy());
            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.MapControllers();

            app.Run();
        }
    }
}