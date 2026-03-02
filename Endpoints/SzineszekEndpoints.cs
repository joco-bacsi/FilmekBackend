using GameStore.Data;
using GameStore.DTOS;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class SzineszekEndpoints
{
    const string GetGameEndpointName = "GetGame"; 
    public static RouteGroupBuilder MapSzineszekEndpoints(this WebApplication app)
    {
        //Get /games
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", async (filmkatalogusContext dbContext) =>
         await dbContext.szineszeks
                        .Select(Szineszek => Szineszek.ToSzineszekDetailsDto())
                        .AsNoTracking().ToListAsync());
          
        // Get /games/1
        group.MapGet("/{id}", async (int id, filmkatalogusContext dbContext) =>
        {
            Szineszek? szineszek = await dbContext.szineszeks.FindAsync(id);

            return szineszek is null ? Results.NotFound() : Results.Ok(szineszek.ToSzineszekDetailsDto);
        });

        //Post /games
        group.MapPost("/", async (SzineszekHozzaadasDto newSzinesz, filmkatalogusContext dbContext) =>
        {
        Szineszek szineszek = newSzinesz.ToEntity();
        
        dbContext.szineszeks.Add(szineszek);
        await dbContext.SaveChangesAsync();
            
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = szineszek.Id }, szineszek.ToSzineszekDetailsDto());
        });

        // Put /games/1
        group.MapPut("/{id}", async (int id, SzineszekUpdateDto updatedFilm, filmkatalogusContext dbContex) =>
        {
            var existingGame = await dbContex.szineszeks.FindAsync(id);
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
           await dbContex.szineszeks.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
