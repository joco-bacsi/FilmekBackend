using System.ComponentModel.DataAnnotations;

namespace GameStore.DTOS;

public record class FilmekDto(
    int Id, [Required] string Rendezo, [Required] string Cim, [Required] string Mufaj, 
    DateTime Hossz, [Required] string Nyelv, DateTime MegjelenesiDatum,
    double ImDbErtekeles, double SajatErtekeles
);
