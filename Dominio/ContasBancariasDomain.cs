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
            if (string.IsNullOrWhiteSpace(contaBancaria.Titular))
                throw new Exception("Falta informar o titular.");

            if (string.IsNullOrWhiteSpace(contaBancaria.NumeroConta))
                throw new Exception("Falta informar o número da conta.");

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

       
        public void InserirMovimento(Movimento movimento)
        {
            if (movimento.Valor <= 0)
                throw new Exception("O valor do movimento deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(movimento.Descricao))
                throw new Exception("Falta informar a descrição do movimento.");

            var conta = _contasBancariasRepository.BuscarContaPorId(movimento.ContaBancariaId);

            switch (movimento.TipoMovimento)
            {
                case EnumTipoMovimento.Saida when conta.Saldo < movimento.Valor:
                    throw new Exception("Saldo insuficiente.");
                case EnumTipoMovimento.Saida:
                    conta.Saldo -= movimento.Valor;
                    break;
                default:
                    conta.Saldo += movimento.Valor;
                    break;
            }

            movimento.ContaBancaria = conta;
            _contasBancariasRepository.InserirMovimento(movimento);
        }
    }
}
