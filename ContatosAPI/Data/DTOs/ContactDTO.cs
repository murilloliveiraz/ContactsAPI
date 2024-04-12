using System.ComponentModel.DataAnnotations;

namespace ContatosAPI.Data.DTOs
{
    public class ContactDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(50, ErrorMessage = "O nome não pode ter mais do que 50 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O número é obrigatório")]
        [MinLength(11, ErrorMessage = "O número deve conter 11 digitos (ex: 11 988884444)")]
        [MaxLength(11, ErrorMessage = "Voce excedeu o limite de 11 caracteres")]
        public string Number { get; set; }
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}
