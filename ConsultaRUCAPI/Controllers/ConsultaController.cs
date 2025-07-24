using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConsultaRUCAPI.Data;
using Microsoft.EntityFrameworkCore;
using ConsultaRUCAPI.Models;

namespace ConsultaRUCAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonasController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("buscar")]
    public IActionResult BuscarPorRuc([FromQuery] string ruc)
    {
        if (string.IsNullOrWhiteSpace(ruc))
            return BadRequest("RUC no proporcionado");

        var tablas = new[] {
            "Azuay", "Bolivar", "Cañar", "Carchi", "Chimborazo", "Cotopaxi",
            "El_Oro", "Esmeraldas", "Galapagos", "Guayas", "Imbabura", "Loja",
            "Los_Rios", "Manabi", "Morona_Santiago", "Napo", "Orellana", "Pastaza",
            "Pichincha", "Santa_Elena", "Santo_Domingo", "Sucumbios", "Tungurahua",
            "Zamora_Chinchipe"
        };

        foreach (var tabla in tablas)
        {
            var sql = $"SELECT * FROM {tabla} WHERE th_per_cedula = @p0";
            try
            {
                var resultado = _context.Set<Persona>().FromSqlRaw(sql, ruc).AsEnumerable().FirstOrDefault();
                if (resultado != null)
                {
                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                // Log para tablas que no existan o error SQL
                Console.WriteLine($"Error en tabla {tabla}: {ex.Message}");
            }
        }

        return NotFound("No se encontró el RUC en ninguna provincia.");
    }
}

