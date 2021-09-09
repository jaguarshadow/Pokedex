using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokemonData;

namespace Pokedex.Controllers
{
    public class PokemonController : Controller
    {
        // GET: Pokemon
        public ActionResult Index()
        {
            PokemonList list = PokemonProcessor.LoadAllPokemon();
            return View(list.Results);
        }

        public ActionResult Details(int id)
        {
            var model = PokemonProcessor.LoadPokemon(id);

            //var model = new Pokemon();
            //model.Name = "Pikachu";
            //model.Id = 25;
            //model.Height = 5;
            //model.Weight = 5;
            //model.Sprite = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/25.png";
            //var pType = new PokemonType();
            //pType.Name = "Electric";
            //var typeSlot = new PokemonTypeSlot();
            //typeSlot.Slot = 0;
            //typeSlot.Type = pType;
            //model.Types = new List<PokemonTypeSlot>();
            //model.Types.Add(typeSlot);
            //model.Description = "Pokemon Description goes here when the model is loaded";

            return View(model);
        }
    }
}