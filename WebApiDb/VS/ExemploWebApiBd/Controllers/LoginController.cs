using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExemploWebApiBd.Interfaces;
using ExemploWebApiBd.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExemploWebApiBd.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]

    [ApiController]
    public class LoginController : ControllerBase
    {
       private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

    }
}
