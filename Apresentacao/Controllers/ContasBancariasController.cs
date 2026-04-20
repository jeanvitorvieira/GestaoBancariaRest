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
        public async Task<IActionResult> Inserir(InserirContaDto inserirContaDto)
        {
            try
            {
                var conta = new ContaBancaria
                {
                    Titular = inserirContaDto.Titular,
                    NumeroConta = inserirContaDto.NumeroConta,
                    TipoConta = inserirContaDto.TipoConta
                };
                await _contasBancariasDomain.InserirAsync(conta);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarContas()
        {
            try
            {
                return Ok(await _contasBancariasDomain.BuscarContasAsync());
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarContaPorId(int id)
        {
            try
            {
                return Ok(await _contasBancariasDomain.BuscarContaPorIdAsync(id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}/extrato")]
        public async Task<IActionResult> BuscarExtrato(int id)
        {
            try
            {
                return Ok(await _contasBancariasDomain.BuscarExtratoAsync(id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _contasBancariasDomain.DeletarAsync(id);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("movimentos")]
        public async Task<IActionResult> InserirMovimento(InserirMovimentoDto dto)
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
                await _contasBancariasDomain.InserirMovimentoAsync(movimento);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}