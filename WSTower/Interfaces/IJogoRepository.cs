using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.Domains;
using WSTower.ViewModels;

namespace WSTower.Interfaces
{
    public interface IJogoRepository : IRepositoryBase<Jogo>
    {
        IEnumerable<Jogo> JogoPorData(DateTime data);
        IEnumerable<Jogo> JogoPorEstadioSelecao(string estadioSelecao);
        List<JogoViewModel> JogoFiltro();
    }
}
