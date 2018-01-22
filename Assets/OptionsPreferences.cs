using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsPreferences : MonoBehaviour
{
    //Variables para cambiar las fuentes
    public int contadorFuentes = 0;
    Text[] textosJuego;
    [SerializeField]
    Font[] fuentes;

    // Use this for initialization
    void Start()
    {
        print("Se inicia el script");
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            DefaultOptions();
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        else
        {
            //Poner el lenguaje
            //Actualizar los checkmarcks 
            //Y ponemos la fuente del usuario  
            contadorFuentes = GetUserFont();
            SetFontToLabels();
        }
    }

   

    void DefaultOptions()
    {
        SetLanguage("Spanish");
        SetVoiceActivated(1);
        SetAnimalVoiceActivated(1);
        SetUserFont(0);
    }

    public bool GetVoiceActivated()
    {
        if (PlayerPrefs.GetInt("VoiceActivated") == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetVoiceActivated(int _voice)
    {
        PlayerPrefs.SetInt("VoiceActivated", _voice);
    }

    public bool GetAnimalSounds()
    {
        if (PlayerPrefs.GetInt("AnimalVoiceActivated") == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetAnimalVoiceActivated(int _animal)
    {
        PlayerPrefs.SetInt("AnimalVoiceActivated", _animal);
    }

    public string GetLanguage()
    {
        return PlayerPrefs.GetString("Language");
    }

    public void SetLanguage(string _language)
    {
        PlayerPrefs.SetString("Language", _language);
    }

    void SetUserFont(int _fuente)
    {
        PlayerPrefs.SetInt("Font", _fuente);
    }

    int GetUserFont()
    {
        return PlayerPrefs.GetInt("Font");
    }

    void SetFontToLabels()
    {
        textosJuego = GameObject.FindObjectsOfType<Text>();
        for (int i = 0; i < textosJuego.Length; i++)
        {
            textosJuego[i].font = fuentes[contadorFuentes];
        }
        SetUserFont(contadorFuentes);
    }
    

    public void ChangeFontButton()
    {
        contadorFuentes++;
        //Comprobamos si está al final
        if (contadorFuentes == fuentes.Length)
        {
            contadorFuentes = 0;
        }

        SetFontToLabels();
        
    }
}
