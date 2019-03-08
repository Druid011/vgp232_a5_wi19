using Assignment5.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Assignment 5 - Pokemon Edition");

            PokemonReader reader = new PokemonReader();
            PokemonSaver saver = new PokemonSaver();
            Pokedex pokedex = reader.Load_Pokedex("pokemon151.xml");

            // List out all the pokemons loaded
            foreach (Pokemon pokemon in pokedex.Pokemons)
            {
                Console.WriteLine(pokemon.Name);
            }

            
            PokemonBag theBag = new PokemonBag();
            theBag.AddPokemon(pokedex, "bulbsaur");
            theBag.AddPokemon(pokedex, "bulbsaur");
            theBag.AddPokemon(pokedex, "charlizard");
            theBag.AddPokemon(pokedex, "mew");
            theBag.AddPokemon(pokedex, "dragonite");


            string fileName = "myPokemonBag.xml";
            // Serializting and saving pokemonBeg object with exception handling
            try
            {
                saver.Save_PokeBag(theBag, fileName);
            }
            catch (Exception e)
            {

                Console.WriteLine(string.Format("\nSaving {0} failed, due to error: {1}\n",fileName, e.Message));
            }

            // Deserializting and loading pokemonBeg object with exception handling
            PokemonBag newBag = new PokemonBag();
            try
            {
                newBag = reader.Load_PokemonBag(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Loading {0} failed, due to error: {1}", fileName, e.Message));
            }

            //TODO::add a beg pretty print function in pokemonbeg class. ----DONE
            newBag.PrettyPrint(pokedex);

            Console.ReadKey();
        }
    }
}
