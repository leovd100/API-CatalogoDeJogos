using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDejogos.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {

        private readonly IJogoService _jogoService;




        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        /// <summary>
        ///  Busca todos os jogos de forma paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1,int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade = 5)
        {
            var result = await _jogoService.Obter(pagina, quantidade);
            if(result.Count() == 0) {
                return NoContent();
            } 
            return Ok(result);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var result = await _jogoService.Obter(idJogo);

            if(result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }



        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var result = await _jogoService.Inserir(jogoInputModel);

                return Ok(result);
            }
           // catch(JogoJaCadastradoException ex)
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
            
        }


        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            //JogoNaoCadastradoException
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }


        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {

            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            //JoogNaoCadastradoException 
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }

        }



        
        [HttpDelete]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            //JogoNaoCadastradoException
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }


    }
}