using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<filmkatalogusContext>(connString); 
var app = builder.Build();

app.MapSzineszekEndpoints(); 
app.MapFilmekEndpoints();
app.MapFilm_CastEndpoints();

await app.MigrateDB();

app.Run();
