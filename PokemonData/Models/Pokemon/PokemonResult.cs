using System;
using System.Globalization;
using System.Linq;

namespace PokemonData
{
    public class PokemonResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public void Intialize()
        {
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            Name = ti.ToTitleCase(Name);

            Uri uri = new Uri(Url);
            string id = uri.Segments.Last().TrimEnd('/');
            Id = id;
            //Id = int.Parse(id);
        }
    }
}
