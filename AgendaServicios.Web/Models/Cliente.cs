using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaServicios.Web.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(50)]
    [MinLength(3, ErrorMessage = "El apellido debe tener al menos 3 caracteres")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50)]
    [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
    public string Nombre { get; set; } = null!;

    [Display(Name = "Fecha de nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [Display(Name = "Tipo de documento")]
    public string TipoDocumento { get; set; } = null!;

    [Display(Name = "Número de documento")]
    [Required]
    [Range(1, 99999999, ErrorMessage = "Número de documento inválido")]
    public int NumeroDocumento { get; set; }

    public string Calle { get; set; } = null!;

    public int Altura { get; set; }

    public string Barrio { get; set; } = null!;

    public string? Partido { get; set; }

    [Display(Name = "Provincia")]
    public int ProvinciaId { get; set; }

    [Display(Name = "Localidad")]
    public int LocalidadId { get; set; }

    [Display(Name = "Código postal")]
    public int CodigoPostal { get; set; }

    [Display(Name = "CUIT/CUIL")]
    public string? CuitCuil { get; set; }

    [Display(Name = "Razón social")]
    public string? RazonSocial { get; set; }

    [Display(Name = "Correo Electrónico")]
    public string CorreoElectronico { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual Localidad? Localidad { get; set; } = null!;

    public virtual Provincia? Provincia { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
