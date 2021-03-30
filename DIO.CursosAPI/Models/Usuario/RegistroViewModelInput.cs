using System.ComponentModel.DataAnnotations;

namespace DIO.CursosAPI.Models.Usuario
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
