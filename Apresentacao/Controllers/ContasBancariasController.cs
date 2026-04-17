using Dominio;
using Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacao
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ContasBancariasController : ControllerBase
    {
        private readonly ContasBancariasDomain _contasBancariasDomain;

        public ContasBancariasController(ContasBancariasDomain domain)
        {
            _contasBancariasDomain = domain;
        }
        
        [HttpPost]
        public IActionResult Inserir(InserirContaDto inserirContaDto)
        {
            
            try
            {
                var conta = new ContaBancaria();

                conta.Titular = inserirContaDto.Titular;
                conta.NumeroConta = inserirContaDto.NumeroConta;
                conta.TipoConta = inserirContaDto.TipoConta;

                _contasBancariasDomain.Inserir(conta);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult BuscarContas()
        {
            try
            {
                var listaDeContas = _contasBancariasDomain.BuscarContas();

                return Ok(listaDeContas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarContaPorId(int id)
        {
            try
            {
                var conta = _contasBancariasDomain.BuscarContaPorId(id);

                return Ok(conta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _contasBancariasDomain.Deletar(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("movimentos")]
        public IActionResult InserirMovimento(InserirMovimentoDto dto)
        {
            try
            {
                var movimento = new Movimento
                {
                    ContaBancariaId = dto.ContaBancariaId,
                    Valor = dto.Valor,
                    TipoMovimento = dto.TipoMovimento,
                    Descricao = dto.Descricao
                };

                _contasBancariasDomain.InserirMovimento(movimento);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
