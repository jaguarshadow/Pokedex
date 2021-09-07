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
            PokemonList list = PokemonProcessor.GetAllPokemon();
            return View(list.Results);
        }

        public ActionResult Details(int id)
        {
            var model = PokemonProcessor.LoadPokemon(id);
            Console.WriteLine(model);
            return View(model);
        }
    }
}