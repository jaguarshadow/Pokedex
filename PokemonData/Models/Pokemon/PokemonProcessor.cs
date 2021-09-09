using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonData
{
    public class PokemonProcessor
    {
        private const int MAX_POKEMON = 898;
        public static Pokemon LoadPokemon(int id)
        {
            var client = ApiHelper.ApiClient;
            //HTTP GET
            var responseTask = client.GetAsync($"pokemon/{id.ToString()}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                Pokemon pInstance = JsonConvert.DeserializeObject<Pokemon>(readTask.Result);
                pInstance.Initialize();
                pInstance.Description = LoadDescription(pInstance.Id);
                return pInstance;
            }
            else
            {
                // web api sent error response
                throw new Exception(result.ReasonPhrase);
            }
        }

        private static string LoadDescription(int id)
        {
            var client = ApiHelper.ApiClient;
            //HTTP GET
            var responseTask = client.GetAsync($"pokemon-species/{id.ToString()}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                int desc_count = JObject.Parse(readTask.Result)["flavor_text_entries"].Count();
                int desc_index = 0;
                for (int i = 0; i < desc_count; i++)
                {
                    string lang = JObject.Parse(readTask.Result)["flavor_text_entries"][i]["language"]["name"].ToString();
                    if (lang == "en")
                    {
                        desc_index = i;
                        break;
                    }
                }
                string description = JObject.Parse(readTask.Result)["flavor_text_entries"][desc_index]["flavor_text"].ToString();
                string[] sArray = description.Split();
                description = "";
                foreach (string s in sArray)
                {
                    description += s + " ";
                }
                return description;
            }
            else
            {
                // web api sent error response
                throw new Exception(result.ReasonPhrase);
            }
        }

        public static PokemonList LoadAllPokemon()
        {
            var client = ApiHelper.ApiClient;

            // GET LIST
            var responseTask = client.GetAsync($"pokemon?limit={MAX_POKEMON}");
            responseTask.Wait();

            var result = responseTask.Result;
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
