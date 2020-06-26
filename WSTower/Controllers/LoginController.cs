using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WSTower.Domains;
using WSTower.Interfaces;
using WSTower.Repositories;
using WSTower.ViewModels;

namespace WSTower.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        
        /// <summary>
        /// Loga o usuario por email ou apelido
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token</returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            Usuario userSelect = _usuarioRepository.BuscarEmailSenha(user.Email, user.Senha);

            if (userSelect == null)
            {
                return NotFound("E-mail ou senha inválidos");
            }

            //payload

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userSelect.Email),
                new Claim(JwtRegisteredClaimNames.Jti, userSelect.Id.ToString()),
                new Claim(ClaimTypes.Role, "Usuario") // MUDAR ESSA LINHA QUANDO FOR ADD ADM
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("wstower-key-auth"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "WSTower.WebApi",                // emissor do token
                audience: "WSTower.WebApi",              // destinatário do token
                claims: claims,                          // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),    // tempo de expiração
                signingCredentials: creds                // credenciais do token
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
