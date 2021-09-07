using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonData
{
    public class PokemonProcessor
    {
        public static Pokemon LoadPokemon(int id)
        {
            var client = ApiHelper.ApiClient;
            //HTTP GET
            var responseTask = client.GetAsync(id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                Pokemon pInstance = JsonConvert.DeserializeObject<Pokemon>(readTask.Result);
                pInstance.Initialize();
                return pInstance;
            }
            else
            {
                // web api sent error response
                throw new Exception(result.ReasonPhrase);
            }
        }

        public static PokemonList GetAllPokemon()
        {
            int pokemonCount = 0;

            string url = $"http://pokeapi.co/api/v2/pokemon/";
            var client = ApiHelper.ApiClient;
            //GET COUNT
            var responseTask = client.GetAsync("?limit=0");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PokemonList>();
                readTask.Wait();

                pokemonCount = readTask.Result.Count;
            }
            else
            {
                // web api sent error response
                throw new Exception(result.ReasonPhrase);
            }

            // GET LIST
            responseTask = client.GetAsync($"?limit={pokemonCount}");
            responseTask.Wait();

            result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                PokemonList list = JsonConvert.DeserializeObject<PokemonList>(readTask.Result);
                list.Results.ForEach(p => p.Intialize());
                return list;
            }
            else
            {
                // web api sent error response
                throw new Exception(result.ReasonPhrase);
            }
        }
    }
}
