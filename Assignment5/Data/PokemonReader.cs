using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assignment5.Data
{
    public class PokemonReader
    {
        private XmlSerializer pokedexSerializer;
        private XmlSerializer pokeBagSerializer;

        /// <summary>
        /// Construtor
        /// </summary>
        public PokemonReader()
        {
            pokedexSerializer = new XmlSerializer(typeof(Pokedex));
            pokeBagSerializer = new XmlSerializer(typeof(PokemonBag));
        }

        /// <summary>
        /// Load a xml file that contains Pokemon Data to be deserialized into a list of Pokemons
        /// </summary>
        /// <param name="filepath">The location of the xml file</param>
        /// <returns>A list of Pokemons</returns>
        public Pokedex Load_Pokedex(string filepath)
        {
            //TODO:: check if file end of .xml DONE
            if (!filepath.EndsWith(".xml")) { filepath = filepath + ".xml"; }
            if (!File.Exists(filepath))
            {
                throw new Exception(string.Format("{0} does not exist", filepath));
            }

            Pokedex dex = null;
            using (var file = new StreamReader(filepath))
            {
                try
                {
                    dex = pokedexSerializer.Deserialize(file) as Pokedex;
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Unable to deserialize the {0} due to following: {1}",
                        filepath, e.Message));
                }
            }

            return dex;
        }

        public PokemonBag Load_PokemonBag(string filePath)
        {
            //TODO:: check if file end of .xml DONE
            if (!filePath.EndsWith(".xml")) { filePath = filePath + ".xml"; }
            if (!File.Exists(filePath))
            {
                throw new Exception(string.Format("{0} does not exist", filePath));
            }

            PokemonBag pokeBag = null;
            using (var file = new StreamReader(filePath))
            {
                try
                {
                    pokeBag = pokeBagSerializer.Deserialize(file) as PokemonBag;
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Unable to deserialize the {0} due to following: {1}",filePath, e.Message));
                }
            }

            return pokeBag;
        }

    }
}
