using GameStore.Data;
using GameStore.DTOS;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class FilmekEndpoints
{
    const string GetGameEndpointName = "GetGame"; 
    public static RouteGroupBuilder MapFilmekEndpoints(this WebApplication app)
    {
        //Get /games
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", async (filmkatalogusContext dbContext) =>
         await dbContext.Filmek
                        .Select(film => film.ToFilmekDetailsDto())
                        .AsNoTracking().ToListAsync());
          
        // Get /games/1
        group.MapGet("/{id}", async (int id, filmkatalogusContext dbContext) =>
        {
            Filmek? film = await dbContext.Filmek.FindAsync(id);

            return film is null ? Results.NotFound() : Results.Ok(film.ToFilmekDetailsDto);
        });

        //Post /games
        group.MapPost("/", async (FilmekHozzaadasDto newFilm, filmkatalogusContext dbContext) =>
        {
        Filmek film = newFilm.ToEntity();
        
        dbContext.Filmek.Add(film);
        await dbContext.SaveChangesAsync();
            
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = film.Id }, film.ToFilmekDetailsDto());
        });

        // Put /games/1
        group.MapPut("/{id}", async (int id, FilmekUpdateDto updatedFilm, filmkatalogusContext dbContex) =>
        {
            var existingGame = await dbContex.Filmek.FindAsync(id);
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
           await dbContex.Filmek.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
