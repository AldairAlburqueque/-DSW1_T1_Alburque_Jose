using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dsw1_t1_Alburqueque_Jose.Models
{
  [Table("cursos")]
  public class Curso
  {
    [Key]
    [Column("cursoId")]
    public int CursoId { get; set; }

    [Required(ErrorMessage = "El codigo curso es obligatorio")]
    [Column("codigoCurso")]
    public string CodigoCurso { get; set; }

    [Required(ErrorMessage = "El nombre del curso es obligatorio")]
    [Column("nombreCurso")]
    public string NombreCurso { get; set; }

    [Column("creditos")]
    public int Creditos { get; set; }

    [Column("horasSemanales")]
    public int HorasSemanales { get; set; }

    [ForeignKey("NivelAcademico")]
    [Column("nivelAcademicoId")]
    public int NivelAcademicoId { get; set; }

    public NivelAcademico? NivelAcademico { get; set; }

  }
}