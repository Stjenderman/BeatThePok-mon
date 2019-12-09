using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeatThePokemon.Models.ViewModels;

namespace BeatThePokemon.Models.Convert
{
    public class PokemonCreateConvert
    {
        public Pokemon CreateToPokemon(PokemonCreateViewModel pcvm)
        {
            Soort s = new Soort((Soort.TypeSoorten)pcvm.Type, ((Soort.TypeSoorten)pcvm.Type).ToString());
            MemoryStream memoryStream = new MemoryStream();
            pcvm.Image.CopyTo(memoryStream);
            return new Pokemon(pcvm.Naam, s, memoryStream.ToArray());
        }
    }
}
