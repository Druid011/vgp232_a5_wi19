using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace Assignment5.Data
{
    public class Test_PokemonReader
    {
        //PokemonReader tests
        PokemonReader testReader;
        string pokedex_sourceExample_XML;
        string pokemonBag_sourceExample_XML;
        string path;

        [SetUp]
        public void SetUp()
        {
            testReader = new PokemonReader();
            pokedex_sourceExample_XML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                                            "<Pokedex>\n" +
                                                "<Pokemons>\n" +
                                                    "<Pokemon>\n" +
                                                        "<Index>1</Index>\n" +
                                                        "<Name>Bulbasaur</Name>\n" +
                                                        "<Type1>Grass</Type1>\n" +
                                                        "<Type2>Poison</Type2>\n" +
                                                        "<HP>128</HP>\n" +
                                                        "<Attack>118</Attack>\n" +
                                                        "<Defense>111</Defense>\n" +
                                                        "<MaxCP>1115</MaxCP>\n" +
                                                     "</Pokemon>\n" +
                                                 "</Pokemons>\n" + 
                                             "</Pokedex>";

            pokemonBag_sourceExample_XML = "<?xml version=\"1.0\"?>\n" +
                                                "<PokemonBag>\n" +
                                                    "<Pokemons>\n" +
                                                        "<int>151</int>\n" +
                                                        "<int>149</int>\n" +
                                                    "</Pokemons>\n" +
                                                 "</PokemonBag>";

            string now = DateTime.Now.ToString("MMddyyyyhmm");
            string dir = System.AppContext.BaseDirectory;
            path = dir + now;
        }
        [TearDown]
        public void TearDown()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        [Test]
        public void PokemonReader_Load_Pokedex_HappyPath()
        {

            Pokedex expect = new Pokedex();
            Pokedex actrual;

            // set up expect 
            Pokemon thePokemon = new Pokemon
            {
                                    Index = 1,
                                    Name = "Bulbasaur",
                                    Type1 = "Grass",
                                    Type2 = "Poison",
                                    HP = 128,
                                    Attack = 118,
                                    Defense = 111,
                                    MaxCP = 1115
                                };
            expect.Pokemons.Add(thePokemon);


            // Create a simple test xml for Pokedex
            
            string fileName = "testUse_pokedex.xml";
            path += fileName;

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(pokedex_sourceExample_XML);
                }
            }

            actrual = testReader.Load_Pokedex(path);

            Assert.AreEqual(expect, actrual);

        }

        [Test]
        public void PokemonReader_Load_Pokedex_Case_FileNotExist()
        {
            string fileName = "testUse_pokedex.xml";
            path += fileName;

            // Delete the file in case its really exist
            File.Delete(path);

            Pokedex dex = new Pokedex();
            Assert.That(() => testReader.Load_Pokedex(path), Throws.TypeOf<Exception>());

        }

        [Test]
        public void PokemonReader_Load_PokemonBag_HappyPath()
        {
            PokemonBag expect = new PokemonBag();
            PokemonBag actrual;

            // set up expect 
            expect.Pokemons.Add(151);
            expect.Pokemons.Add(149);


            // Create a simple test xml for Pokedex
            string fileName = "testUse_pokemonBag.xml";
            path += fileName;

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(pokemonBag_sourceExample_XML);
                }
            }

            actrual = testReader.Load_PokemonBag(path);

            Assert.AreEqual(expect, actrual);
        }

        [Test]
        public void PokemonReader_Load_PokemonBag_Case_FileNotExist()
        {
            string fileName = "testUse_pokedex.xml";
            path += fileName;

            // Delete the file in case its really exist
            File.Delete(path);

            PokemonBag theBag = new PokemonBag();
            Assert.That(() => testReader.Load_Pokedex(path), Throws.TypeOf<Exception>());
        }

    }

    public class Test_PokemonSaver
    {
        PokemonSaver saver;
        //for checking result
        PokemonReader reader;

        string path;

        [SetUp]
        public void Init()
        {
            saver = new PokemonSaver();
            reader = new PokemonReader();

            string now = DateTime.Now.ToString("MMddyyyyhmm");
            string dir = System.AppContext.BaseDirectory;
            path = dir + now;
        }

        [TearDown]
        public void Cleanup()
        {
            // clean up all possiable file that can be created
            string path1 = path;
            string path2 = path + ".xml";
            if (File.Exists(path1))
            {
                File.Delete(path1);
            }

            if (File.Exists(path2))
            {
                File.Delete(path2);
            }
        }

        [TestCase("pokedex.xml")]
        [TestCase("pokedex")]
        public void PokemonSaver_SavePokedex_HappyPass(string fileName)
        {
            Pokedex expect = new Pokedex();
            Pokedex actrul;

            Pokemon thePokemon = new Pokemon
            {
                Index = 1,
                Name = "Bulbasaur",
                Type1 = "Grass",
                Type2 = "Poison",
                HP = 128,
                Attack = 118,
                Defense = 111,
                MaxCP = 1115
            };
            expect.Pokemons.Add(thePokemon);

            //set up unique fileName
            
            path += fileName;
            saver.Save_Pokedex(expect, path);

            // Load up the actrul
            actrul = reader.Load_Pokedex(path);

            Assert.AreEqual(expect, actrul);
        }

        [TestCase("dex.xml")]
        [TestCase("dex")]
        public void PokemonSaver_SavePokedex_FileAlreadyExist(string fileName)
        {
            Pokedex myDex = new Pokedex();

            path += fileName;
            saver.Save_Pokedex(myDex,path);

            Assert.That(() => saver.Save_Pokedex(myDex, path), Throws.TypeOf<Exception>());

        }

        [TestCase("pokemonBag.xml")]
        [TestCase("pokemonBag")]
        public void PokemonSaver_SavePokemonBag_HappyPass(string fileName)
        {
            PokemonBag expect = new PokemonBag();
            PokemonBag actrul;

            expect.Pokemons.Add(1); // Add a Bulbasaur

            //set up unique fileName

            path += fileName;
            saver.Save_PokeBag(expect, path);

            // Load up the actrul
            actrul = reader.Load_PokemonBag(path);

            Assert.AreEqual(expect, actrul);
        }

        [TestCase("bag.xml")]
        [TestCase("bag")]
        public void PokemonSaver_SavePokemonBag_FileAlreadyExist(string fileName)
        {
            PokemonBag myBag = new PokemonBag();

            path += fileName;
            saver.Save_PokeBag(myBag, path);

            Assert.That(() => saver.Save_PokeBag(myBag, path), Throws.TypeOf<Exception>());
        }
    }

    public class Test_Pokedex
    {
        Pokedex dex;
       

        [SetUp]
        public void SetUp()
        {
            dex = new Pokedex();
            dex.Pokemons.Add(new Pokemon
            {
                Index = 1,
                Name = "Bulbasaur",
                Type1 = "Grass",
                Type2 = "Poison",
                HP = 128,
                Attack = 118,
                Defense = 111,
                MaxCP = 1115
            });
            dex.Pokemons.Add(new Pokemon
            {
                Index = 2,
                Name = "Ivysaur",
                Type1 = "Grass",
                Type2 = "Poison",
                HP = 155,
                Attack = 151,
                Defense = 143,
                MaxCP = 1699
            });
            dex.Pokemons.Add(new Pokemon
            {
                Index = 6,
                Name = "Charizard",
                Type1 = "Fire",
                Type2 = "Flying",
                HP = 186,
                Attack = 223,
                Defense = 173,
                MaxCP = 2889
            });
            dex.Pokemons.Add(new Pokemon
            {
                Index = 9,
                Name = "Blastoise",
                Type1 = "Water",
                Type2 = "",
                HP = 188,
                Attack = 171,
                Defense = 207,
                MaxCP = 2466
            });

        }

        // GetPokemonByIndex
        [Test]
        public void Pokedex_GetPokemonByIndex_HappyPass()
        {
            // Index 3 is Blastoise
            Pokemon expect = dex.Pokemons[3]; 
            Pokemon actural = dex.GetPokemonByIndex(9);
            Assert.AreEqual(actural, expect);
        }

        [TestCase(810)] // current max number of pokemons + 1
        [TestCase(-2)]
        public void Pokedex_GetPokemonByIndex_CaseInvalidIndex(int index)
        {
            Assert.That(() => dex.GetPokemonByIndex(index), Throws.TypeOf<Exception>());
        }

        [TestCase(386)]
        [TestCase(151)]
        public void Pokedex_GetPokemonByIndex_CaseUnknowIndexLessThanMax(int index)
        {
            Pokemon actrual = dex.GetPokemonByIndex(index);
            Pokemon expect = Pokedex.unknowPokemon;
            Assert.AreEqual(actrual, expect);
        }

        [Test]
        public void Pokedex_GetPokemonByIndex_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetPokemonByIndex(0), Throws.TypeOf<Exception>());
        }

        // GetPokemonByName
        [Test]
        public void Pokedex_GetPokemonByName_HappyPass()
        {
            Pokemon expect = dex.Pokemons[0];
            Pokemon actural = dex.GetPokemonByName("Bulbasaur");
            Assert.AreEqual(actural, expect);
        }

        [TestCase("Bulba")]
        [TestCase("mew")]
        public void Pokedex_GetPokemonByName_CaseUnknowName(string name)
        {
            Assert.That(() => dex.GetPokemonByName(name), Throws.TypeOf<Exception>());
        }

        [TestCase("bulbasaur")]
        [TestCase("buLbaSAur")]
        public void Pokedex_GetPokemonByName_CaseRandomCapitalize(string name)
        {
            Pokemon expect = dex.Pokemons[0];
            Pokemon actural = dex.GetPokemonByName(name);
            Assert.AreEqual(actural, expect);
        }

        [Test]
        public void Pokedex_GetPokemonByName_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetPokemonByName("Bulbasaur"), Throws.TypeOf<Exception>());
        }

        // GetPokemonOfType
        [Test]
        public void Pokedex_GetPokemonOfType_HappyPass()
        {
            List<Pokemon> expect = new List<Pokemon>();
            expect.Add(dex.Pokemons[0]);
            expect.Add(dex.Pokemons[1]);
            List<Pokemon> actural = dex.GetPokemonsOfType("Grass");
            Assert.AreEqual(actural, expect);
        }

        [TestCase("mud")]
        [TestCase("air")]
        public void Pokedex_GetPokemonOfType_CaseInvalidType(string type)
        {
            List<Pokemon> expect = new List<Pokemon>();
            List<Pokemon> actural = dex.GetPokemonsOfType(type);
            Assert.AreEqual(actural, expect);
        }

        [Test]
        public void Pokedex_GetPokemonOfType_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetPokemonsOfType(""), Throws.TypeOf<Exception>());
        }

        // GetHighestHPPokemon
        [Test]
        public void Pokedex_GetHighestHPPokemon_HappyPass()
        {
            // Index 3 is blastoise
            Pokemon expect = dex.Pokemons[3];
            Pokemon actural = dex.GetHighestHPPokemon();
            Assert.AreEqual(actural, expect);
        }

        [Test]
        public void Pokedex_GetHighestHPPokemon_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetHighestHPPokemon(), Throws.TypeOf<Exception>());
        }

        // GetHighestAttackPokemon
        [Test]
        public void Pokedex_GetHighestAttackPokemon_HappyPass()
        {
            // Index 2 is Charizard
            Pokemon expect = dex.Pokemons[2];
            Pokemon actural = dex.GetHighestAttackPokemon();
            Assert.AreEqual(actural, expect);
        }

        [Test]
        public void Pokedex_GetHighestAttackPokemon_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetHighestAttackPokemon(), Throws.TypeOf<Exception>());
        }

        // GetHighestDefensePokemon
        [Test]
        public void Pokedex_GetHighestDefensePokemon_HappyPass()
        {
            // Index 3 is Blastiose
            Pokemon expect = dex.Pokemons[3];
            Pokemon actural = dex.GetHighestDefensePokemon();
            Assert.AreEqual(actural, expect);
        }

        [Test]
        public void Pokedex_GetHighestDefensePokemon_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetHighestDefensePokemon(), Throws.TypeOf<Exception>());
        }

        // GetHighestMaxCPPokemon
        [Test]
        public void Pokedex_GetHighestMaxCPPokemon_HappyPass()
        {
            // Index 2 is Charizard
            Pokemon expect = dex.Pokemons[2];
            Pokemon actural = dex.GetHighestMaxCPPokemon();
            Assert.AreEqual(actural, expect);
        }

        [Test]
        public void Pokedex_GetHighestMaxCPPokemon_CaseEmptyPokedex()
        {
            dex.Pokemons.Clear();
            Assert.That(() => dex.GetHighestMaxCPPokemon(), Throws.TypeOf<Exception>());
        }
    }

    public class Test_PokemonBag
    {
        PokemonBag bag;
        Pokedex dex;

        [SetUp]
        public void SetUp()
        {
            bag = new PokemonBag();
            dex = new Pokedex();
            dex.Pokemons.Add(new Pokemon
            {
                Index = 1,
                Name = "Bulbasaur",
                Type1 = "Grass",
                Type2 = "Poison",
                HP = 128,
                Attack = 118,
                Defense = 111,
                MaxCP = 1115
            });
            dex.Pokemons.Add(new Pokemon
            {
                Index = 2,
                Name = "Ivysaur",
                Type1 = "Grass",
                Type2 = "Poison",
                HP = 155,
                Attack = 151,
                Defense = 143,
                MaxCP = 1699
            });
            dex.Pokemons.Add(new Pokemon
            {
                Index = 6,
                Name = "Charizard",
                Type1 = "Fire",
                Type2 = "Flying",
                HP = 186,
                Attack = 223,
                Defense = 173,
                MaxCP = 2889
            });
            dex.Pokemons.Add(new Pokemon
            {
                Index = 9,
                Name = "Blastoise",
                Type1 = "Water",
                Type2 = "",
                HP = 188,
                Attack = 171,
                Defense = 207,
                MaxCP = 2466
            });

        }

        [Test]
        public void PokemonBag_AddPokemon_HappyPath()
        {
            PokemonBag expect = new PokemonBag();
            expect.Pokemons.Add(9);
            bag.AddPokemon(dex,"Blastoise");

            Assert.AreEqual(bag, expect);
        }

        [TestCase("Blasise")]
        [TestCase("sdfsa")]
        public void PokemonBag_AddPokemon_CaseUnknowName(string name)
        {
            PokemonBag expect = new PokemonBag();
            bag.AddPokemon(dex, "Blasise");

            Assert.AreEqual(bag, expect);
        }
    }
}
