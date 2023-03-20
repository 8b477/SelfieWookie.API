using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using SelfieWookie.Core.Infrastructure.Configurations;

namespace SelfieWookie.API.UI.ExtensionMethods
{
    /// <summary>
    /// A propos de (CORS et JWT)
    /// </summary>
    public static class SecurityMethods // ! Ne pas oublier de faire un builder.Services.AddCustomSecurity(); dans le program.cs ainsi que un app.UseCors(SecurityMethods.DEFAULT_POLICY);
    {

        #region constante pour démo
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public const string DEFAULT_POLICY2 = "DEFAULT_POLICY2";
        public const string DEFAULT_POLICY3 = "DEFAULT_POLICY3"; 
        #endregion

        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddCustomCors(configuration);
            services.AddCustomAuthentication(configuration);
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Récupération de ma clef secrete qui se trouve dans le secrets.json
            SecurityOption securityOption = new SecurityOption(); // initialisation de ma classe qui fait relation
            configuration.GetSection("Jwt").Bind(securityOption); // bind la section qui contient la clef secrete

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(securityOption.Key); //Je récup ma clef depuis mon appsetings.json

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true
                };
            });
        }

    public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
            CorsOption corsOption = new CorsOption();
            configuration.GetSection("Cors").Bind(corsOption);


            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(corsOption.Origin) // => Me permet d'accéder à mon fichier appsettings.json.
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY2, builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5502") // => par faciliter ici écrit en dur mais normalement le récup dans un fichier de config comme appsettings.json.
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY3, builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5503")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

    }
}
#region Plus d'infos sur la classe !
// Pour la démonstration je crée deux CORS au quelle je greffe une Policy différentes
// dans le program.cs je passe ma Policy numéro deux qui n'est pas bonne pour mon test Front car
// l'adresse URL fournie dans le WithOrigins() n'est pas correct
// je n'accède effectivement plus a mes data côté Front
// je vais dans le controller et je lui passe comme attribut => [EnableCors(SecurityMethods.DEFAULT_POLICY)]
// qui me permet de passer au dessus de ma premiere restriction de CORS déclarée côté program.cs sur le DEFAULT_POLICY2

// **
// En conclusion le shéma donnée est la pour montrée comment établir plusieur CORS avec des restriction différentes
// et qu'il est possible de spécifier certain controller ou méthode de controller d'être soumis à une ou autre resctriction CORS.
// **
#endregion