using Entidades;
using Repositorio.Infra;

namespace Repositorio
{
    public class ContasBancariasRepository
    {
        private readonly DataContext _dataContext;
        
        public ContasBancariasRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Inserir(ContaBancaria contaBancaria)
        {
            _dataContext.Add(contaBancaria);

            _dataContext.SaveChanges();
        }

        public List<ContaBancaria> BuscarContas()
        {
            return _dataContext.ContasBancarias.ToList();
        }

        public ContaBancaria BuscarContaPorId(int id)
        {
            return _dataContext.ContasBancarias.Find(id) ?? throw new InvalidOperationException();
        }

        public void Deletar(int id)
        {
            var conta = _dataContext.ContasBancarias.Find(id);
            if (conta == null) return;
            _dataContext.ContasBancarias.Remove(conta);
            _dataContext.SaveChanges();
        }

        public void EnviarPix(Movimento movimentos)
        {
            _dataContext.Movimentos.Add(movimentos);

            _dataContext.ContasBancarias.Update(movimentos.ContaBancaria);

            _dataContext.SaveChanges();
        }
    }
}
