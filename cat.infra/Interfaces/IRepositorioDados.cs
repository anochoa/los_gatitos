using cat.infra.modelo;

namespace cat.infra.Interfaces
{
    public interface IRepositorioDados
    {
        Task<List<Gatinho>> BuscarGato(FiltroGatinho filtro);
        Task SalvarGato(Gatinho gato);
        Task SalvarGato(List<Gatinho> gatos);
    }
}
