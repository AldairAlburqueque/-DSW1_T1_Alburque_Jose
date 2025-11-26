using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dsw1_t1_Alburqueque_Jose.Models
{
  [Table("nivelAcademico")]
  public class NivelAcademico
  {
    [Key]
    [Column("nivelAcademicoId")]
    public int NivelAcademicoId { get; set; }

    [Column("descripcion")]
    public string Descripcion { get; set; }

    [Column("orden")]
    public int Orden { get; set; }

    [JsonIgnore]
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();

  }
}