using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WSTower.Domains;
using WSTower.Interfaces;
using WSTower.Repositories;
using WSTower.ViewModels;

namespace WSTower.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SelecaoController : ControllerBase
    {
        private ISelecaoRepository _selecaoRepository { get; set; }

        public SelecaoController()
        {
            _selecaoRepository = new SelecaoRepository();
        }

        /// <summary>
        /// Retorna todas as seleções.
        /// </summary>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: api/<Selecao>
        [HttpGet]
        public IEnumerable<Selecao> Get()
        {
            return _selecaoRepository.GetAll();
        }

        /// <summary>
        /// Retorna todas as seleções ordenada por pontos.
        /// </summary>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: api/<Selecao>
        [HttpGet("Pontos")]
        public IEnumerable<SelecaoViewModel> GetOrderByDescending()
        {
            return _selecaoRepository.OrdemPontos();
        }

        /// <summary>
        /// Retorna todas as seleções ordenada por nomes.
        /// </summary>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: api/<Selecao>
        [HttpGet("Nomes")]
        public IEnumerable<Selecao> GetOrderByNames()
        {
            return _selecaoRepository.OrdemNome();
        }

        /// <summary>
        /// Retorna uma seleção por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Selecao Object</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET api/<Selecao>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_selecaoRepository.GetById(id));
        }

        /// <summary>
        /// Cadastra uma seleção.
        /// </summary>
        /// <param name="selecao"></param>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/<Selecao>
        [HttpPost]
        public IActionResult Post(Selecao selecao)
        {
            try
            {
                _selecaoRepository.Add(selecao);
                return Ok("Seleção cadastrada com sucesso");
            }
            catch
            {
                return BadRequest("Erro ao cadastrar Seleção.");
            }
        }

        /// <summary>
        /// Atualiza uma seleção passando o seu id na url
        /// </summary>
        /// <param name="id"></param>
        /// <param name="selecao"></param>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT api/<Selecao>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Selecao selecao)
        {
            try
            {
                Selecao selec = new Selecao
                {
                    Id = id,
                    Nome = selecao.Nome,
                    Bandeira = selecao.Bandeira,
                    Uniforme = selecao.Uniforme,
                    Escalacao = selecao.Escalacao,
                    Jogador = selecao.Jogador,
                    JogoSelecaoCasaNavigation = selecao.JogoSelecaoCasaNavigation,
                    JogoSelecaoVisitanteNavigation = selecao.JogoSelecaoVisitanteNavigation

                };

                _selecaoRepository.Update(selec);
                return Ok("Seleção atualizada.");

            }
            catch
            {
                return BadRequest("Erro ao atualizar a Seleção ");

            }
        }

        /// <summary>
        /// Deleta uma seleção passando o seu id na url
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE api/<Selecao>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Selecao selecao = _selecaoRepository.GetById(id);
                _selecaoRepository.Delete(selecao);
                return Ok("Seleção Deletada.");
            }
            catch
            {
                return BadRequest("Erro ao deletar Seleção.");
            }
        }
    }
}