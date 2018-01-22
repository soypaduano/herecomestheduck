using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constantes
{

    public static readonly List<string> animales = new List<string> { "aguila", "burro", "caballo", "cerdo", "cocodrilo", "elefante", "gallina", "gallo", "gato", "gaviota", "leon", "lobo", "loro", "mono", "oso", "oveja", "paloma", "perro", "pollo", "rana", "raton", "serpiente", "tigre", "vaca"};
    public static readonly List<string> soundAnimals = new List<string> { "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", "sonido", };


    

   



    public static string FirstLetterToUpper(string str)
    {
        if (str == null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }

}
