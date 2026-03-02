using GameStore.Data;
using GameStore.DTOS;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Endpoints;

public static class Film_CastEndpoints
{
    const string GetGameEndpointName = "GetGame"; 
    public static RouteGroupBuilder MapFilm_CastEndpoints(this WebApplication app)
    {
        //Get /games
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", async (filmkatalogusContext dbContext) =>
         await dbContext.Film_Casts
                        .Select(Film_cast => Film_cast.ToFilm_CastDetailsDto())
                        .AsNoTracking().ToListAsync());
          
        // Get /games/1
        group.MapGet("/{id}", async (int id, filmkatalogusContext dbContext) =>
        {
            Film_Cast? cast = await dbContext.Film_Casts.FindAsync(id);

            return cast is null ? Results.NotFound() : Results.Ok(cast.ToFilm_CastDetailsDto);
        });

        //Post /games
        group.MapPost("/", async (Film_CastHozzaadasDto newcast, filmkatalogusContext dbContext) =>
        {
        Film_Cast cast = newcast.ToEntity();
        
        dbContext.Film_Casts.Add(cast);
        await dbContext.SaveChangesAsync();
            
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = cast.SzineszId }, cast.ToFilm_CastDetailsDto());
        });

        // Put /games/1
        group.MapPut("/{id}", async (int id, Film_CastUpdateDto updatedFilm, filmkatalogusContext dbContex) =>
        {
            var existingGame = await dbContex.Film_Casts.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContex.Entry(existingGame).CurrentValues.SetValues(updatedFilm.ToEntity(id));
            await dbContex.SaveChangesAsync();
            return Results.NoContent();
        });

        // Delete /games/1

        group.MapDelete("/{id}", async (int id,filmkatalogusContext dbContex) =>
        {
           await dbContex.Film_Casts.Where(game => game.SzineszId == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
