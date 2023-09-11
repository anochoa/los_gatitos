namespace cat.infra.modelo
{
    public class FiltroGatinho
    {
        public int FiltroLimite { get; set; }
        public FiltroGatinho() { FiltroLimite = 10; }

        public FiltroGatinho(int limite)
        {
            if (limite < 0) throw new ArgumentOutOfRangeException(nameof(limite));

            FiltroLimite = limite;
        }
    }
}
