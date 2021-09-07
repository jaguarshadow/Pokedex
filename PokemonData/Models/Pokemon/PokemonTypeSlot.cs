using Newtonsoft.Json;

namespace PokemonData
{
    public class PokemonTypeSlot
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public PokemonType Type { get; set; }
    }


}
