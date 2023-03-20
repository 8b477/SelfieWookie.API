using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SelfieWookie.API.UI.ExtensionMethods;
using SelfieWookie.API.UI.Middlewares;
using SelfieWookie.Core.Infrastructure.DB;
using SelfieWookie.Core.Infrastructure.Logger;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddProvider(new LoggerCustomProvider());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection à ma base de données
builder.Services.AddDbContext<SelfieContext>
    (
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBSelfieWookie"))
    );


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // options.SignIn.RequireConfirmedEmail = true; => Je peux ajouter des restrictions / contraintes basé sur Identity.
}).AddEntityFrameworkStores<SelfieContext>(); // => Ajout du context qui nous servira de données d'authentification.


#region Injections de dépendance
// Lié une interface à une classe, quand celle ci se retrouve appelé
// commme argument dans un constructeur celui ci est instaciée
// En gros je dit : => Quand tu voi ISelfieRepository tu vas faire un new DefaultSelfieRepository().
// **Injection de dépendance**

// => builder.Services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();


// Ici je fait le même que plus haut mais de façon plus propre en passant par ma classe statique < DIMethod >.
#endregion
builder.Services.AddInjections();
builder.Services.AddCustomSecurity(builder.Configuration);// Ajout de ma sécurité configurée
builder.Services.AddCustomOptions(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(SecurityMethods.DEFAULT_POLICY2); // => Utilise les config de nos CORS.
app.UseAuthentication(); // => Utilise la config de notre JWT.
app.UseMiddleware<LogRequestMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
