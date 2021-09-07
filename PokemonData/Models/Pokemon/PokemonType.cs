using Newtonsoft.Json;

namespace PokemonData
{
    public class PokemonType
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
