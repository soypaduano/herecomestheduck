using System.Collections.Generic;

public static class Constantes
{

    public static readonly List<string> animales = new List<string> { "aguila", "burro", "caballo", "cerdo", "cocodrilo", "elefante", "gallina", "gallo", "gato", "gaviota", "leon", "lobo", "loro", "mono", "oso", "oveja", "paloma", "perro", "pollo", "rana", "raton", "serpiente", "tigre", "vaca", "pato"};
    public static readonly List<string> soundAnimals = new List<string> { "chii chii!", "íja íja!", "hiii hiii!", "oink oink!", "argr argr!", "bruu bruu!", "clo clo!", "kikiriki!", "miau miau!", "crot crot!", "grrr grrr!", "auuu auuu!", "trua trua!", "uaa uaa!", "grrr grrr!", "beee beee!", "cucu cucu!", "woof woof!", "pio pio!", "croac croac!", "mimi mimi!", "bsss bsss!", "grrr grrr!", "muuu!", "cua cua!"};
    public static readonly List<string> languages = new List<string> { "Español", "English" };
    public static string FirstLetterToUpper(string str)
    {
        if (str == null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }


    public static bool checkIfRandomInsideArray(List<int> _lista, int _numero)
    {
        foreach (int i in _lista)
        {
            if (i == _numero)
            {
                return true;
            }
        }

        return false;
    }
}
