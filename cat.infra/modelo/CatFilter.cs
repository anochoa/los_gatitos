namespace cat.infra.modelo
{
    public class CatFilter
    {
        public CatFilter() { filtroLimit = 10; }

        public CatFilter(float _limit)
        {
            if (_limit < 0 && int.TryParse(_limit.ToString(), out int _))
                filtroLimit = int.Parse(_limit.ToString());
        }
        public int filtroLimit { get; set; }
    }
}
