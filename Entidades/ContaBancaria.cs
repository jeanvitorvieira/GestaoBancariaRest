namespace Entidades
{
    public record InserirContaDto(string Titular, string NumeroConta, EnumTipoConta TipoConta);
    public class ContaBancaria
    {
        public int Id { get; set; }

        public string Titular { get; set; }

        public string NumeroConta { get; set; }

        public EnumTipoConta TipoConta { get; set; }
        public decimal Saldo { get; set; } = 0;
    }

    public enum EnumTipoConta
    {
        Corrente,
        Poupanca
    }
}
