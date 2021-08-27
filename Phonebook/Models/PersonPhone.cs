using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models
{
    [Table("PersonPhone")]
    public class PersonPhone
    {
        [Key]
        [Column("BusinnesEntityID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [DataType("nvarchar")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Tipo de telefone inválido")]
        public int PhoneNumberTypeID { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
    }
}