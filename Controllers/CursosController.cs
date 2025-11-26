using dsw1_t1_Alburqueque_Jose.Data;
using dsw1_t1_Alburqueque_Jose.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace dsw1_t1_Alburqueque_Jose.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CursosController : ControllerBase
  {
    private readonly AplicationsContext _context;

    public CursosController(AplicationsContext context)
    {
      _context = context;
    }

    //1.Primera pregunta con paginacion
    [HttpGet("listar/{nivelId}")]
    public async Task<ActionResult<IEnumerable<Curso>>> ListarCursosPorNivel(
    int nivelId,
    int page = 1,
    int pageSize = 10)
    {
      if (page <= 0) page = 1;
      if (pageSize <= 0) pageSize = 10;

      var query = _context.Cursos
                          .Where(c => c.NivelAcademicoId == nivelId)
                          .Include(c => c.NivelAcademico);

      var totalRegistros = await query.CountAsync();
      var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);

      var cursos = await query
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync();

      var resultado = new
      {
        nivelId,
        paginaActual = page,
        tamanoPagina = pageSize,
        totalRegistros,
        totalPaginas,
        data = cursos
      };

      return Ok(resultado);
    }

    //2.Segunda pregunta


    // 1. GET: Listar todos los cursos con NivelAcademico incluido
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
    {
      var cursos = await _context.Cursos
                                .Include(c => c.NivelAcademico)
                                .ToListAsync();

      return Ok(cursos);
    }


    // 2. GET: Obtener cursos por nivel académico
    [HttpGet("nivel/{nivelId}")]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorNivel(int nivelId)
    {
      var cursos = await _context.Cursos
                                .Where(c => c.NivelAcademicoId == nivelId)
                                .Include(c => c.NivelAcademico)
                                .ToListAsync();

      return Ok(cursos);
    }

    // 3. POST: Crear nuevo curso
    [HttpPost]
    public async Task<ActionResult<Curso>> CrearCurso([FromBody] Curso curso)
    {
      _context.Cursos.Add(curso);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCursosPorNivel),
          new { nivelId = curso.NivelAcademicoId }, curso);
    }

    // 4. PUT: Actualizar curso existente
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCurso(int id, [FromBody] Curso curso)
    {

      var cursoExistente = await _context.Cursos.FindAsync(id);
      if (cursoExistente == null)
        return NotFound("El curso no existe");

      cursoExistente.NombreCurso = curso.NombreCurso;
      cursoExistente.Creditos = curso.Creditos;
      cursoExistente.HorasSemanales = curso.HorasSemanales;
      cursoExistente.NivelAcademicoId = curso.NivelAcademicoId;

      await _context.SaveChangesAsync();

      return NoContent();
    }

  }

}