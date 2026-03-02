namespace GameStore.DTOS;

public record class FilmekUpdateDto(
     
    string Rendezo, 
    string Cim, 
     string Mufaj, 
    DateTime Hossz, 
     string Nyelv, 
    DateTime MegjelenesiDatum,
    double ImDbErtekeles, 
    double SajatErtekeles
);

