using UnityEngine;
using UnityEngine.UI;

public class OptionsPreferences : MonoBehaviour
{
    int fontCounter = 0;
    [SerializeField]
    Font[] fuentes;
    [SerializeField]
    GameObject checkmarkVoice, checkmarkVoiceAnimal;
    [SerializeField]
    Text languageText;
    [SerializeField]
    Image flag;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text speedGame;

    void Awake()
    {

        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            DefaultOptions();
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        else
        {
            checkCurrentLanguage();
            checkVoiceActivated();
            fontCounter = GetUserFont();
            SetFontToLabels();
            SetGameSpeedToSlider();
        }
    }

    void DefaultOptions()
    {
        SetLanguage(0);
        SetVoiceActivated(1);
        SetAnimalSoundsActivated(1);
        SetUserFont(0);
        SetGameSpeed(2);
    }

    //Human Voice 
    bool GetVoiceActivated()
    {
        if (PlayerPrefs.GetInt("VoiceActivated") == 0)
            return false;
        else
            return true;
    }

    void checkVoiceActivated()
    {
        if (GetVoiceActivated())
            checkmarkVoice.SetActive(true);
        else
            checkmarkVoice.SetActive(false);
    }

    public void VoiceHasBeenChanged()
    {
        if (GetVoiceActivated())
            SetVoiceActivated(0);
        else
            SetVoiceActivated(1);

        checkVoiceActivated();
    }

    public void SetVoiceActivated(int _voice)
    {
        PlayerPrefs.SetInt("VoiceActivated", _voice);
    }

    //Animal Voice
    bool GetAnimalSoundsActivated()
    {
        if (PlayerPrefs.GetInt("AnimalVoiceActivated") == 0)
            return false;
        else
            return true;
    }

    void checkAnimalSoundsActivated()
    {
        if (GetAnimalSoundsActivated())
            checkmarkVoiceAnimal.SetActive(true);
        else
            checkmarkVoiceAnimal.SetActive(false);
    }

    public void AnimalSoundHasBeenChanged()
    {
        if (GetAnimalSoundsActivated())
            SetAnimalSoundsActivated(0);
        else
            SetAnimalSoundsActivated(1);

        checkAnimalSoundsActivated();
    }

    public void SetAnimalSoundsActivated(int _animal)
    {
        PlayerPrefs.SetInt("AnimalVoiceActivated", _animal);
    }

    //Language
    void checkCurrentLanguage()
    {
        if(GetLanguage() == 0){
            languageText.text = "Idioma: Español";
            flag.sprite = Resources.Load<Sprite>("spanish_flag");
        } else if(GetLanguage() == 1)
        {
            languageText.text = "Language: English";
            flag.sprite = Resources.Load<Sprite>("english_flag");
        }
    }

    public void LanguageChanged()
    {
        print(GetLanguage());
        print(Constantes.languages.Count);
        int languageCounter = GetLanguage();
        if(languageCounter == Constantes.languages.Count - 1)
        {
            SetLanguage(0);
        } else
        {
            SetLanguage(++languageCounter);
        }

        checkCurrentLanguage();
    }

    int GetLanguage()
    {
        return PlayerPrefs.GetInt("Language");
    }

    void SetLanguage(int _language)
    {
        PlayerPrefs.SetInt("Language", _language);
    }

    //FONTS
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
        print("PONEMOS LA FUENTE A TODOS LOS TEXTOS");
        Text[] allTexts = GameObject.FindObjectsOfType<Text>();
        for (int i = 0; i < allTexts.Length; i++)
        {
            allTexts[i].font = fuentes[fontCounter];
        }
        SetUserFont(fontCounter);
    }

    public void ChangeFontButton()
    {
        fontCounter++;
        //Comprobamos si está al final
        if (fontCounter == fuentes.Length)
        {
            fontCounter = 0;
        }

        SetFontToLabels();

    }

    //SPEED SLIDER
    public void SliderValueChanged()
    {
        speedGame.text = "Velocidad de juego: " + slider.value;
        SetGameSpeed(slider.value);
    }

    void SetGameSpeed(float _speed)
    {
        PlayerPrefs.SetFloat("GameSpeed", _speed);
    }

    public float getGameSpeed()
    {
        return PlayerPrefs.GetFloat("GameSpeed");
    }

    void SetGameSpeedToSlider()
    {
        slider.value = getGameSpeed();
    }
}




