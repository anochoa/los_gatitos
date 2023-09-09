using cat.infra.modelo;
using RestSharp;

using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace cat.infra
{
    public class RepositorioDados
    {
        private IConfiguration _configuration { get; set; }
        public RepositorioDados(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Gatinho>> BuscarGato(FiltroGatinho filtro)
        {
            RestRequest requisicao = PegarRequisicao(filtro);
            RestClient cliente = PegarCliente();

            List<Gatinho>? gatinhos = await cliente.GetAsync<List<Gatinho>>(requisicao);

            if (gatinhos is null) throw new ArgumentNullException(nameof(gatinhos));

            return gatinhos;
        }

        public async Task<bool> SalvarGato(Gatinho gato)
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
            catch {}

            return true;
        }

        private RestRequest PegarRequisicao(FiltroGatinho filtro)
        { 
            string? apiKey = _configuration["RestRequest:ApiKeyHeader"];

            if (apiKey is null) throw new ArgumentNullException(nameof(apiKey));

            RestRequest requisicao = new RestRequest($"v1/images/search?limit={filtro.FiltroLimit}")
                .AddHeader("x-api-key", apiKey);

            return requisicao;
        }

        private RestClient PegarCliente()
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
