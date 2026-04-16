using Entidades;
using Repositorio;

namespace Dominio
{
    public class ContasBancariasDomain
    {
        private readonly ContasBancariasRepository _contasBancariasRepository;

        public ContasBancariasDomain(ContasBancariasRepository repository)
        {
            _contasBancariasRepository = repository;
        }

        public void Inserir(ContaBancaria contaBancaria)
        {
            if (string.IsNullOrEmpty(contaBancaria.Titular))
            {
                throw new Exception("Falta informar o titular.");
            }

            _contasBancariasRepository.Inserir(contaBancaria);
        }

        public List<ContaBancaria> BuscarContas()
        {
            return _contasBancariasRepository.BuscarContas();
        }

        public ContaBancaria BuscarContaPorId(int id)
        {
            return _contasBancariasRepository.BuscarContaPorId(id);
        }

        public void Deletar(int id)
        {
            _contasBancariasRepository.Deletar(id);
        }

        public void EnviarPix(Movimento movimento)
        {
            var conta = _contasBancariasRepository.BuscarContaPorId(movimento.ContaBancariaId);

            if (conta == null)
                throw new Exception("Conta não encontrada!");

            switch (movimento.TipoMovimento)
            {
                case EnumTipoMovimento.Saida when conta.Saldo < movimento.Valor:
                    throw new Exception("Saldo insuficiente!");
                case EnumTipoMovimento.Saida:
                    conta.Saldo -= movimento.Valor;
                    break;
                default:
                    conta.Saldo += movimento.Valor;
                    break;
            }

            movimento.ContaBancaria = conta;

            _contasBancariasRepository.EnviarPix(movimento);
        
        }
    }
}
