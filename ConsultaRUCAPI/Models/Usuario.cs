namespace ConsultaRUCAPI.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public DateTime FechaCreacion { get; set; }
}
