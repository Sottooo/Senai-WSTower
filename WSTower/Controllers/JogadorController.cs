using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSTower.Domains;
using WSTower.Interfaces;
using WSTower.Repositories;

namespace WSTower.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JogadorController : ControllerBase
    {
        private IJogadorRepository _jogadorRepository { get; set; }

        public JogadorController()
        {
            _jogadorRepository = new JogadorRepository();
        }

        /// <summary>
        /// Retorna todos os jogadores.
        /// </summary>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: api/<Jogador>
        [HttpGet]
        public IEnumerable<Jogador> Get()
        {
            return _jogadorRepository.GetAll();
        }

        /// <summary>
        /// Retorna um jogador por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Jogador Object</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET api/<Jogador>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (_jogadorRepository.GetById(id) != null)
            {
                return Ok(_jogadorRepository.GetById(id));
            }
            else
            {
                return BadRequest("Jogador nao encontrado.");
            }
        }

        /// <summary>
        /// Cadastra um usuario.
        /// </summary>
        /// <param name="jogador"></param>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/<Jogador>
        [HttpPost]
        public IActionResult Post(Jogador jogador)
        {
            try
            {
                _jogadorRepository.Add(jogador);
                return Ok("Jogador cadastrado com sucesso");
            }
            catch
            {
                return BadRequest("Erro ao cadastrar jogador.");
            }
        }

        /// <summary>
        /// Atualiza um jogador passando o seu id na url
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jogador"></param>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT api/<Jogador>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Jogador jogador)
        {
            try
            {
                Jogador jogador1 = new Jogador
                {
                    Id = id,
                    Nome = jogador.Nome,
                    Nascimento = jogador.Nascimento,
                    Posicao = jogador.Posicao,
                    Qtdefaltas = jogador.Qtdefaltas,
                    QtdecartoesAmarelo = jogador.QtdecartoesAmarelo,
                    QtdecartoesVermelho = jogador.QtdecartoesVermelho,
                    Qtdegols = jogador.Qtdegols,
                    Informacoes = jogador.Informacoes,
                    NumeroCamisa = jogador.NumeroCamisa,
                    Foto = jogador.Foto,
                    SelecaoId = jogador.SelecaoId

                };

                _jogadorRepository.Update(jogador1);
                return Ok("Jogador atualizado.");

            }
            catch
            {
                return BadRequest("Erro ao atualizar o Jogador ");

            }
        }

        /// <summary>
        /// Deleta um jogador passando o seu id na url
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE api/<Jogador>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Jogador jogador1 = _jogadorRepository.GetById(id);
                _jogadorRepository.Delete(jogador1);
                return Ok("jogador Deletado.");
            }
            catch
            {
                return BadRequest("Erro ao deletar Jogador.");
            }
        }
    }
}