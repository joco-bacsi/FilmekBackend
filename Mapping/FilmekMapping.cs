using System;
using GameStore.DTOS;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class FilmekMapping
{
    public static Filmek ToEntity(this FilmekHozzaadasDto film)
    {
        return new Filmek()
        {
            Rendezo= film.Rendezo, 
            Cim = film.Cim, 
            Mufaj = film.Mufaj, 
            Hossz = film.Hossz, 
            Nyelv = film.Nyelv, 
            MegjelenesiDatum = film.MegjelenesiDatum,
            ImDbErtekeles = film.ImDbErtekeles,
            SajatErtekeles = film.SajatErtekeles
        };
    }   

     public static Filmek ToEntity(this FilmekUpdateDto film, int id)
    {
        return new Filmek()
        {   
            Id = id,
            Rendezo= film.Rendezo, 
            Cim = film.Cim, 
            Mufaj = film.Mufaj, 
            Hossz = film.Hossz, 
            Nyelv = film.Nyelv, 
            MegjelenesiDatum = film.MegjelenesiDatum,
            ImDbErtekeles = film.ImDbErtekeles,
            SajatErtekeles = film.SajatErtekeles
        };
    } 

   

     public static FilmekDto ToFilmekDetailsDto(this Filmek film)
    {
        return  new(
            film.Id,
            film.Rendezo, 
            film.Cim, 
            film.Mufaj, 
            film.Hossz, 
            film.Nyelv, 
            film.MegjelenesiDatum,
            film.ImDbErtekeles,
            film.SajatErtekeles
            );
    }
}
