using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.Domains;
using WSTower.ViewModels;

namespace WSTower.Interfaces
{
    public interface ISelecaoRepository : IRepositoryBase<Selecao>
    {
        public int Pontos(int id);
        public IEnumerable<SelecaoViewModel> OrdemPontos();
        public IEnumerable<Selecao> OrdemNome();
    }
}