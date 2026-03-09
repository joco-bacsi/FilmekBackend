using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("FilmekBackend");
builder.Services.AddDbContext<filmkatalogusContext>(options => 
    options.UseMySql(connString, ServerVersion.AutoDetect(connString))); 
var app = builder.Build();

app.MapSzineszekEndpoints(); 
app.MapFilmekEndpoints();
app.MapFilm_CastEndpoints();

await app.MigrateDB();

app.Run();
