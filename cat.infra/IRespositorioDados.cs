using cat.infra.modelo;

namespace cat.infra
{
    public interface IRespositorioDados
    {
        Task<List<Gatinho>> PegarGatos(CatFilter filter);
        Task<bool> SalvarGato(Gatinho cat);
    }
}
