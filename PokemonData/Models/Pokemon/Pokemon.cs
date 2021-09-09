using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace PokemonData
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("types"), Display(Name = "Types")]
        public List<PokemonTypeSlot> Types { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        public string Sprite { get; set; }

        public string Description { get; set; }

        public void Initialize()
        {
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            Name = ti.ToTitleCase(Name);
            foreach (PokemonTypeSlot slot in Types)
            {
                slot.Type.Name = ti.ToTitleCase(slot.Type.Name);
            }
            Sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{Id}.png";
        }
    }
}
