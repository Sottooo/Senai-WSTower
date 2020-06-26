using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSTower.Contexts;
using WSTower.Domains;
using WSTower.Interfaces;
using WSTower.ViewModels;

namespace WSTower.Repositories
{
    public class JogoRepository : RepositoryBase<Jogo>, IJogoRepository
    {
        WSTowerContext ctx = new WSTowerContext();
        public IEnumerable<Jogo> OrdemDecrescente()
        {
            IEnumerable<Jogo> jogos = GetAll();

            return jogos.OrderByDescending(j => j.Data).ToList();
        }

        public IEnumerable<Jogo> JogoPorData(DateTime data)
        {
            IEnumerable<Jogo> jogos = GetAll();

            return jogos.Where(j => j.Data == data).ToList();
        }

        public IEnumerable<Jogo> JogoPorEstadioSelecao(string estadioSelecao)
        {
            //IEnumerable<Jogo> jogos = GetAll();

            return ctx.Jogo.Where(j => j.Estadio == estadioSelecao || j.SelecaoCasaNavigation.Nome == estadioSelecao || j.SelecaoVisitanteNavigation.Nome == estadioSelecao).ToList();
        }

        public List<JogoViewModel> JogoFiltro()
        {
            List<Jogo> listaJogos = ctx.Jogo.ToList();

            foreach (var item in listaJogos)
            {
                item.SelecaoCasaNavigation = ctx.Selecao.Find(item.SelecaoCasa);
                item.SelecaoVisitanteNavigation = ctx.Selecao.Find(item.SelecaoVisitante);
            }

            List<JogoViewModel> jogosFiltrado = new List<JogoViewModel>();

            foreach (var item in listaJogos)
            {
                JogoViewModel jogoView = new JogoViewModel
                {
                    Id = item.Id,
                    SelecaoCasa = item.SelecaoCasaNavigation.Nome,
                    SelecaoVisitante = item.SelecaoVisitanteNavigation.Nome,
                    PlacarCasa = item.PlacarCasa,
                    PlacarVisitante = item.PenaltisVisitante,
                    UniformeCasa = item.SelecaoCasaNavigation.Uniforme,
                    UniformeVisitante = item.SelecaoVisitanteNavigation.Uniforme
                };
                jogosFiltrado.Add(jogoView);
            }

            return jogosFiltrado;
        }
        
    }
}
