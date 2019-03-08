using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Assignment5.Data
{
    class PokemonSaver
    {
        private XmlSerializer pokedexSerializer;
        private XmlSerializer pokeBagSerializer;

        public PokemonSaver()
        {
            pokedexSerializer = new XmlSerializer(typeof(Pokedex));
            pokeBagSerializer = new XmlSerializer(typeof(PokemonBag));
        }

        public void Save_Pokedex(Pokedex dex, string fileName)
        {
            //TODO:: check if file end of .xml DONE
            if (!fileName.EndsWith(".xml")) { fileName = fileName + ".xml"; }
            if (File.Exists(fileName))
            {
                throw new Exception(string.Format("File {0} already exist, " +
                                                  "overwrite a existing pokedex is not allowed, " +
                                                  "the pokedex onwer going to be upset", fileName));
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                pokedexSerializer.Serialize(fs, dex);
            }
        }

        public void Save_PokeBag(PokemonBag pokeBag, string fileName)
        {
            //TODO:: check if file end of .xml DONE
            if (!fileName.EndsWith(".xml")) { fileName = fileName + ".xml"; }
            if (File.Exists(fileName))
            {
                throw new Exception(string.Format("File {0} already exist, " +
                                                  "overwrite a existing pokemonBag is not allowed, " +
                                                  "the bag onwer going to be upset", fileName));
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                pokeBagSerializer.Serialize(fs, pokeBag);
            }

        }
    }
}
