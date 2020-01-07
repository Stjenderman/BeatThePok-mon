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
            Soort s = new Soort((Soort.TypeSoorten)pcvm.Type, null);
            List<Aanval> aanvallen = new List<Aanval>();
            aanvallen.Add(pcvm.ToeTeVoegenAanval);
            MemoryStream memoryStream = new MemoryStream();
            pcvm.Image.CopyTo(memoryStream);
            return new Pokemon(pcvm.Naam, s, aanvallen, memoryStream.ToArray());
        }
    }
}
