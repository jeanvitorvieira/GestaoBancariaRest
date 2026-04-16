namespace Entidades
{
    public record InserirMovimentoDto(int ContaBancariaId, decimal Valor, EnumTipoMovimento TipoMovimento, string Descricao);
    
    public class Movimento
    {
        public int Id { get; set; }
        public int ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; } 
        public decimal Valor { get; set; }
        public EnumTipoMovimento TipoMovimento { get; set; }
        public string Descricao { get; set; }
    }

    public enum EnumTipoMovimento
    {
        Entrada,
        Saida
    }
}