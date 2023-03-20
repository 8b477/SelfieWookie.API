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

// Connection � ma base de donn�es
builder.Services.AddDbContext<SelfieContext>
    (
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBSelfieWookie"))
    );


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // options.SignIn.RequireConfirmedEmail = true; => Je peux ajouter des restrictions / contraintes bas� sur Identity.
}).AddEntityFrameworkStores<SelfieContext>(); // => Ajout du context qui nous servira de donn�es d'authentification.


#region Injections de d�pendance
// Li� une interface � une classe, quand celle ci se retrouve appel�
// commme argument dans un constructeur celui ci est instaci�e
// En gros je dit : => Quand tu voi ISelfieRepository tu vas faire un new DefaultSelfieRepository().
// **Injection de d�pendance**

// => builder.Services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();


// Ici je fait le m�me que plus haut mais de fa�on plus propre en passant par ma classe statique < DIMethod >.
#endregion
builder.Services.AddInjections();
builder.Services.AddCustomSecurity(builder.Configuration);// Ajout de ma s�curit� configur�e
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
