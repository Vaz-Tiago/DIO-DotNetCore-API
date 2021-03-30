using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.CursosAPI.Models.Usuario
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }

    }
}
