using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    Dictionary<string, string> localizedText;
    string missingText = "No value found";
    bool dataOnDict;

    //Ojo con este metodo, puede pasar que tarde en cargar todo el JSON, por tanto deberiamos poner un bool
    //de Is True, es decir, el json ha sido leido y cargado en el dict.
    public void LoadLocalizedText(string fileName)
    {
        dataOnDict = false;
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson); //We loaded the Json Format into an local object

            //Now we have to fill the empty dictionary
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            dataOnDict = true;

        } else
            print("file path / name couldnt be found");
    }

    public string GetLocalizedValue(string _key)
    {
        if (dataOnDict)
        {
            string result = missingText;
            if (localizedText.ContainsKey(_key))
                result = localizedText[_key];

            return result;
        } else
        {
            print("Data not loaded yet");
            return "perras";
        }

    }
}

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}
