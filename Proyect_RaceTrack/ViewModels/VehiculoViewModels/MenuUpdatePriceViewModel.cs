
namespace Proyect_RaceTrack.ViewModels;
using System.ComponentModel.DataAnnotations;


public class MenuUpdatePriceViewModel
{
    public int VehiculoId { get; set; }
    [Display(Name = "Nombre Propietario")]
    [Required(ErrorMessage = "Debe ingresar el nombre del propietario")]
    [MinLength(3, ErrorMessage = "El nombre ingresado debe poseer mas de tres letras")]
    [MaxLength(15)]
    public string? VehiculoNombre { get; set; }

    [Display(Name = "Apellido Propietario")]
    [Required(ErrorMessage = "Debe ingresar el apellido del propietario")]
    [MinLength(3, ErrorMessage = "El nombre ingresado debe poseer mas de tres letras")]
    [MaxLength(15)]
    public string? VehiculoApellido { get; set; }

    [Display(Name = "Tipo de vehiculo")]
    [Required(ErrorMessage = "Debe ingresar el tipo de vehiculo")]
    [MinLength(3, ErrorMessage = "El nombre ingresado debe poseer mas de tres letras")]
    [MaxLength(15)]
    public string? VehiculoTipo { get; set; }

    [Display(Name = "Matricula")]
    [Required(ErrorMessage = "Debe ingresar el numero de matricula")]
    // [MinLength(3, ErrorMessage = "El nombre ingresado debe poseer mas de tres letras")]  
    // [MaxLength(15)] 
    [MinLength(6, ErrorMessage = "La matricula debe contener al menos 6 caracteres")]
    [MaxLength(7)]
    public string? VehiculoMatricula { get; set; }

    [Display(Name = "Modelo")]
    [Required(ErrorMessage = "Debe ingresar el año de fabricacion")]

    public DateTime VehiculoFabricacion { get; set; }
    public int VehiculoCosto { get; set; }
    public int Cantidad { get; set; }
    public int Instruccion { get; set; }
    public decimal Total { get; set; }


}