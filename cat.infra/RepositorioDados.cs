using cat.infra.modelo;
using RestSharp;

using MySql.Data.MySqlClient;

namespace cat.infra
{
    public class RepositorioDados : IRespositorioDados
    {
        public string conn { get; set; }
        public RepositorioDados()
        {
            string connectionString = "Host=localhost;Port=3366;user=root;database=thecat;password=my-secret-pw;";
            conn = connectionString;
        }
        public async Task<List<Gatinho>> PegarGatos(CatFilter filter)
        {
            var client = new RestClient("https://api.thecatapi.com/");

            var request = new RestRequest($"v1/images/search?limit={filter.filtroLimit}")
                .AddHeader("x-api-key", "live_XkJhgtoqBRSj2LEgpST5FVL3sXTZ6OrsmFKQPUn9FGLV4OSIhd7MxwGjaB07MHi9");

            var gatineos = await client.GetAsync<List<Gatinho>>(request);

            return gatineos;
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
        public async Task<bool> SalvarGato(Gatinho cat)
        {

            string sqlToInsert = $@"insert into osCat (id, url, width, height) values ('{cat.Id}','{cat.Url}',{cat.Width},{cat.Height});";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(conn))
                {

                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sqlToInsert, connection))
                    {
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine($"{rowsAffected}");
                    }

                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
