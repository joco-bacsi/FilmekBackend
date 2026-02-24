using System;

namespace GameStore.Entities;

public class Filmek
{
public int Id { get; set; }
public required string Rendezo { get; set; }
public required string Cim { get; set; }
public  required string Mufaj { get; set; }
public DateTime Hossz { get; set; }
public required string Nyelv { get; set; }
public DateTime MegjelenesiDatum {get; set; }
public double ImDbErtekeles {get; set;}
public double SajatErtekeles {get; set;}
}
