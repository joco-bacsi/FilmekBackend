using System;
using GameStore.DTOS;
using GameStore.Entities;

namespace GameStore.Mapping;

public static class SzineszekMapping
{
    public static Szineszek ToEntity(this SzineszekHozzaadasDto szineszek)
    {
        return new Szineszek()
        {            
            szinesz = szineszek.szinesz,
        };
    }   

     public static Szineszek ToEntity(this SzineszekUpdateDto szineszek, int id)
    {
        return new Szineszek()
        {   Id=id,
            szinesz = szineszek.szinesz
        };
    } 
    public static SzineszekDto ToSzineszekDetailsDto(this Szineszek szineszek)
    {
        return  new(
            szineszek.Id,
             szineszek.szinesz
           
            );
    }

}
