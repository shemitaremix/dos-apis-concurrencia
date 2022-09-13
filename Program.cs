using apis_concurrencia;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Text.Json;
using System.IO;
using System.Threading;
using System.Text.Json.Serialization;
using static apis_concurrencia.Nombresillos;
using static apis_concurrencia.Pokemonsillos;

namespace concurrencia
{
    public class concurrencia
    {
        static void Main(string[] args)
        {
            Thread hilopepe = new Thread(new ThreadStart(GetNombres));
            Thread hilopecas = new Thread(new ThreadStart(GetPokemones));

            hilopepe.Start();
            hilopecas.Start();

            hilopepe.Join();
            hilopecas.Join();
            //GetNombres();
            //GetPokemones();

        }

        static void GetNombres()
        {
           
            var Usuario = new RestClient($"http://api-esp32-alexa.herokuapp.com/api/users/");
            var request = new RestRequest("", Method.Get);
            //request.RequestFormat = DataFormat.Json;
            RestResponse response = Usuario.Execute(request);
            var nombres = JsonSerializer.Deserialize<Nombresillos[]>(response.Content);
            Console.WriteLine("-------- Nombres de Personas ----------");
            foreach (var item in nombres)
            {
                Console.WriteLine(item.id + " -. " + item.first_name);
               
            }
        }

        static void GetPokemones()
        {
            var Pokemon = new RestClient($"https://pokeapi.co/api/v2/pokemon?limit=100&offset=0");
            var request = new RestRequest("", Method.Get);
            //request.RequestFormat = DataFormat.Json;
            RestResponse response = Pokemon.Execute(request);
            var nombres = JsonSerializer.Deserialize<Pokemonsillos>(response.Content);
            var id = 0;
            Console.WriteLine("-------- Nombres de Pokemones ----------");
            foreach (var item in nombres.results)
            {
                Console.WriteLine(id++ + " -. " + item.name);

            }
        }
    }
   

}


