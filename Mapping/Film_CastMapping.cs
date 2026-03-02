using System;
using GameStore.DTOS;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class Film_CastMapping
{
    public static Film_Cast ToEntity(this Film_CastHozzaadasDto cast)
    {
        return new Film_Cast()
        {
            filmCim = cast.filmCim,
        };
    }  
    public static Film_Cast ToEntity(this Film_CastUpdateDto cast, int id)
    {
        return new Film_Cast()
        {  
            SzineszId = id,
            filmCim = cast.filmCim,
        };
    } 
    public static Film_CastDto ToFilm_CastDetailsDto(this Film_Cast cast)
    {
        return  new(
            cast.SzineszId,
            cast.filmCim
            );
    }
}  
