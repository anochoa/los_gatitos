using System.Text;
using cat.infra.Interfaces;
using cat.infra.modelo;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RestSharp;

namespace cat.infra
{
    public class RepositorioDados : IRepositorioDados
    {
        private IConfiguration _configuration { get; set; }
        public RepositorioDados(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Gatinho>> BuscarGato(FiltroGatinho filtro)
        {
            RestRequest requisicao = BuscarRequisicao(filtro);
            RestClient cliente = BuscarCliente();

            List<Gatinho>? gatinhos = await cliente.GetAsync<List<Gatinho>>(requisicao);

            if (gatinhos is null) throw new ArgumentNullException(nameof(gatinhos));

            return gatinhos;
        }

        public async Task SalvarGato(Gatinho gato)
        {

            string sqlParaInserir = $@"insert into osCat (id, url, width, height) values ('{gato.Id}','{gato.Url}',{gato.Width},{gato.Height});";

            try
            {
                var connectionString = _configuration["ConnectionStrings:Connection"];

                using (MySqlConnection conexao = new MySqlConnection(connectionString))
                {

                    await conexao.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sqlParaInserir, conexao))
                    {
                        int linhasAfetadas = await comando.ExecuteNonQueryAsync();
                        Console.WriteLine($"{linhasAfetadas}");
                    }

                }
            }
            catch(Exception e) 
            {
                throw new Exception($"erro ao salvar: {e}");
            }
        }

        public async Task SalvarGato(List<Gatinho> gatos)
        {
            var listaGatos = new StringBuilder();

            foreach (Gatinho gato in gatos)
            {
                listaGatos.AppendFormat(gato.ToString());
            }

            string sqlParaInserirLote = $@"insert into osCat (id, url, width, height) values {listaGatos}";

            try
            {
                var connectionString = _configuration["ConnectionStrings:Connection"];

                using (MySqlConnection conexao = new MySqlConnection(connectionString))
                {

                    await conexao.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sqlParaInserirLote, conexao))
                    {
                        int linhasAfetadas = await comando.ExecuteNonQueryAsync();
                        Console.WriteLine($"{linhasAfetadas}");
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception($"erro ao salvar: {e}");
            }

        }

        private RestRequest BuscarRequisicao(FiltroGatinho filtro)
        { 
            string? apiKey = _configuration["RestRequest:ApiKeyHeader"];

            if (apiKey is null) throw new ArgumentNullException(nameof(apiKey));

            RestRequest requisicao = new RestRequest($"v1/images/search?limit={filtro.FiltroLimite}")
                .AddHeader("x-api-key", apiKey);

            return requisicao;
        }

        private RestClient BuscarCliente()
        {
            string? urlBase = _configuration["RestRequest:ClientBaseCat"];

            if (urlBase is null ) throw new ArgumentNullException(nameof(urlBase));

            RestClient cliente = new RestClient(urlBase);

            return cliente;
        }

        /*
        AQUI TEM UM CONTAINER PRONTO COM O MYSQL
        
        # Pull da imagem:
        docker pull docker.io/library/mysql:5.7

        # Run da imagem
        docker run --name some-mysql -p 3366:3306 -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mysql:5.7
        
        Dai só acessar e excutar o comando abaixo para criar o schemma e a tabela: 
        
            create schema thecat collate utf8mb4_general_ci;
            create table osCat
            (
                id     varchar(255)  null,
                url    VARCHAR(4000) null,
                width  numeric       null,
                height int           null
            );

        */
    }
}
