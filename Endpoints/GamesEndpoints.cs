using GameStore.Data;
using GameStore.DTOS;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame"; 
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        //Get /games
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", async (GameStoreContext dbContext) =>
         await dbContext.Games.Include(game => game.Genre)
                        .Select(game => game.ToGameSummaryDto())
                        .AsNoTracking().ToListAsync());
          
        // Get /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        });

        //Post /games
        group.MapPost("/", async (creategameDto newGame, GameStoreContext dbContext) =>
        {
        Game game = newGame.ToEntity();
           
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
            
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });

        // Put /games/1
        group.MapPut("/{id}", async (int id, UpdateGamedto updatedGame, GameStoreContext dbContex) =>
        {
            var existingGame = await dbContex.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContex.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            await dbContex.SaveChangesAsync();
            return Results.NoContent();
        });

        // Delete /games/1

        group.MapDelete("/{id}", async (int id,GameStoreContext dbContex) =>
        {
           await dbContex.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
