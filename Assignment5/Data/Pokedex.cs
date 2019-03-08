using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assignment5.Data
{

    [XmlRoot("Pokedex")]
    public class Pokedex
    {
        [XmlArray("Pokemons")]
        [XmlArrayItem("Pokemon")]
        public List<Pokemon> Pokemons { get; set; }
        public static Pokemon unknowPokemon;
        public static int MAX_POKEMONS = 808; //Max number of pokemons from <Bulbapedia> - 1 , because we use 0 base

        public Pokedex()
        {
            Pokemons = new List<Pokemon>();

            // Return this when try to look for pokemon that current index dont know
            unknowPokemon = new Pokemon
            {
                Index = MAX_POKEMONS,
                Name = "????",
                Type1 = "??",
                Type2 = "??",
                HP = 0,
                Attack = 0,
                Defense = 0,
                MaxCP = 0
            };
        }

        public Pokemon GetPokemonByIndex(int index)
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }
            
            Pokemon returnPokemon;
            if(index < 0 || index > MAX_POKEMONS)
            {
                throw new Exception("Error!, invalid index detected when get pokemon from pokedex");
            }
            else
            {
                returnPokemon = unknowPokemon;
                returnPokemon.Index = index;
                foreach (var element in Pokemons)
                {
                    if (index == element.Index)
                    {
                        returnPokemon = element;
                        break;
                    }
                }
            }

            return returnPokemon;
        }

        public Pokemon GetPokemonByName(string name)
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }

            Pokemon returnPokemon = unknowPokemon;

            foreach (var element in Pokemons)
            {
                if (String.Compare(name, element.Name,true) == 0)
                {
                    returnPokemon = element;
                    break;
                }
            }

            if (returnPokemon == unknowPokemon)
            {
                throw new Exception(string.Format("\n<Pokedex>: Unknow pokemon name '{0}' dectected\n", name));
            }
            return returnPokemon;
        }

        public List<Pokemon> GetPokemonsOfType(string type)
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }

            // Note to check both Type1 and Type2
            List<Pokemon> returnPokemons = new List<Pokemon>();
            foreach (var element in Pokemons)
            {
                if (String.Compare(element.Type1, type, true) == 0 || String.Compare(element.Type2, type, true) == 0)
                {
                    returnPokemons.Add(element);
                }
            }

            return returnPokemons;
        }

        public Pokemon GetHighestHPPokemon()
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }

            Pokemon returnPokemon = unknowPokemon;
            foreach (var element in Pokemons)
            {
                if (element.HP >= returnPokemon.HP)
                {
                    returnPokemon = element;
                }
            }

            return returnPokemon;
        }

        public Pokemon GetHighestAttackPokemon()
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }

            Pokemon returnPokemon = unknowPokemon;
            foreach (var element in Pokemons)
            {
                if (element.Attack >= returnPokemon.Attack)
                {
                    returnPokemon = element;
                }
            }

            return returnPokemon;
        }

        public Pokemon GetHighestDefensePokemon()
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }

            Pokemon returnPokemon = unknowPokemon;
            foreach (var element in Pokemons)
            {
                if (element.Defense >= returnPokemon.Defense)
                {
                    returnPokemon = element;
                }
            }

            return returnPokemon;
        }

        public Pokemon GetHighestMaxCPPokemon()
        {
            // if pokedex is empty throw exception
            if (Pokemons.Count == 0)
            {
                throw new Exception("Can not get pokemon from empty pokedex");
            }
            Pokemon returnPokemon = unknowPokemon;
            foreach (var element in Pokemons)
            {
                if (element.MaxCP >= returnPokemon.MaxCP)
                {
                    returnPokemon = element;
                }
            }

            return returnPokemon;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Pokedex other = (Pokedex)obj;
            if (Pokemons.Count == other.Pokemons.Count)
            {
                for (int i = 0; i < Pokemons.Count; i++)
                {
                    if (!(Pokemons[i].Equals(other.Pokemons[i])))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
