using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models
{
    [Table("PhoneNumberType")]
    public class PhoneNumberType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(80, ErrorMessage = "Este campo deve conter no máximo 80 caracteres")]
        [DataType("nvarchar")]
        public string Name { get; set; }
    }
}