using ExemploWebApiBd.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploWebApiBd.Interfaces
{
    interface IUsuarioRepository
    {
        void Atualizar(int id, UsuarioDomain usuario);
        void Cadastrar(UsuarioDomain usuario);
        void Deletar(int id);

        void Logar(UsuarioDomain usuaio);
    }
}
