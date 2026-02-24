using System.ComponentModel.DataAnnotations;

namespace GameStore.DTOS;

public record class SzineszekDto(
    int Id, [Required] string szinesz
);
