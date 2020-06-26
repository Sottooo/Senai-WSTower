using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.Contexts;
using WSTower.Domains;
using WSTower.Interfaces;

namespace WSTower.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        WSTowerContext ctx = new WSTowerContext();

        public Usuario BuscarEmailSenha(string email, string senha)
        {
            return ctx.Usuario.FirstOrDefault(u => (u.Email == email && u.Senha == senha) || (u.Apelido == email && u.Senha == senha));
        }

    }
}
