using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
    public class SelecaoRepository : RepositoryBase<Selecao>, ISelecaoRepository
    {
        WSTowerContext ctx = new WSTowerContext();

        public IEnumerable<Selecao> OrdemNome()
        {
            IEnumerable<Selecao> selecaos = GetAll();

            return selecaos.OrderBy(s => s.Nome).ToList();
        }

        public int Pontos(int id)
        {
            Selecao selecao = GetById(id);

            int pontos = 0;
            int vitorias = 0;
            int empates = 0;

            foreach (var item in selecao.JogoSelecaoCasaNavigation)
            {
                var vitoria = selecao.JogoSelecaoCasaNavigation.Any(j => j.PlacarCasa > j.PlacarVisitante);
                var empate = selecao.JogoSelecaoCasaNavigation.Any(j => j.PlacarCasa == j.PlacarVisitante);

                if (vitoria)
                {
                    vitorias += 1;
                }

                if (empate)
                {
                    empates += 1;
                }
            }

            foreach (var item in selecao.JogoSelecaoVisitanteNavigation)
            {
                var vitoria = selecao.JogoSelecaoVisitanteNavigation.Any(j => j.PlacarCasa < j.PlacarVisitante);
                var empate = selecao.JogoSelecaoVisitanteNavigation.Any(j => j.PlacarCasa == j.PlacarVisitante);

                if (vitoria)
                {
                    vitorias += 1;
                }

                if (empate)
                {
                    empates += 1;
                }
            }

            return pontos = (vitorias * 3) + empates;
        }

        public IEnumerable<SelecaoViewModel> OrdemPontos()
        {
            IEnumerable<Selecao> selecaos = GetAll();

            List<SelecaoViewModel> selecaoOrdenada = new List<SelecaoViewModel>();

            foreach (var item in selecaos)
            {
                SelecaoViewModel selecaoViewModel = new SelecaoViewModel();

                selecaoViewModel.Id = item.Id;
                selecaoViewModel.Nome = item.Nome;
                selecaoViewModel.Bandeira = item.Bandeira;
                selecaoViewModel.Pontos = Pontos(item.Id);

                selecaoOrdenada.Add(selecaoViewModel);

            }

          return selecaoOrdenada.OrderByDescending(x => x.Pontos);
        }

    }
}
