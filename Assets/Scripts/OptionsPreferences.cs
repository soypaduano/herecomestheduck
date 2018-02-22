using UnityEngine;
using UnityEngine.UI;

public class OptionsPreferences : MonoBehaviour
{
    int fontCounter = 0;
    [SerializeField]
    Font[] fuentes;
    [SerializeField]
    Text languageText;
    [SerializeField]
    Image flag;
    [SerializeField]
    Slider sliderSpeed, sliderDifficulty;
    [SerializeField]
    Text speedGame, HumanVoiceActivated, AnimalVoiceActivated, difficultyText;
    Text[] allTexts;

    LocalizationManager localizationManager;


    void Awake()
    {
        allTexts = GameObject.FindObjectsOfType<Text>();
    }

    void Start()
    {

        localizationManager = GameObject.Find("LocalizationManager").GetComponent<LocalizationManager>();

        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            DefaultOptions();
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        else
            CheckAllOptions();

    }

    void DefaultOptions()
    {
        SetVoiceActivated(1);
        SetAnimalSoundsActivated(1);
        SetUserFont(0);
        SetGameSpeed(2);
        //SetGameDifficulty(1);
        SetLanguage(0);
    }

    //Human Voice 
    void checkVoiceActivated()
    {
        if (PlayerPrefs.GetInt("VoiceActivated") == 1)
            HumanVoiceActivated.text = localizationManager.GetLocalizedValue(HumanVoiceActivated.gameObject.name) + localizationManager.GetLocalizedValue("Yes");
        else
            HumanVoiceActivated.text = localizationManager.GetLocalizedValue(HumanVoiceActivated.gameObject.name) + "No";
        
    }

    public void VoiceHasBeenChanged()
    {
        if (PlayerPrefs.GetInt("VoiceActivated") == 1)
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
    void checkAnimalSoundsActivated()
    {
        if (PlayerPrefs.GetInt("AnimalVoiceActivated") == 1)
            AnimalVoiceActivated.text = localizationManager.GetLocalizedValue(AnimalVoiceActivated.gameObject.name) + localizationManager.GetLocalizedValue("Yes");
        else
            AnimalVoiceActivated.text = localizationManager.GetLocalizedValue(AnimalVoiceActivated.gameObject.name) + "No";
    }

    public void AnimalSoundHasBeenChanged()
    {
        if (PlayerPrefs.GetInt("AnimalVoiceActivated") == 0) {
            SetAnimalSoundsActivated(1);
        } else
            SetAnimalSoundsActivated(0);

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
            GameObject.Find("AudioController").GetComponent<ControladorAudio>().ChangeLanguage(0);
            localizationManager.LoadLocalizedText("localizedText_es.json");
            TranslateAllTexts();
        } else if(GetLanguage() == 1)
        {
            languageText.text = "Language: English";
            flag.sprite = Resources.Load<Sprite>("english_flag");
            GameObject.Find("AudioController").GetComponent<ControladorAudio>().ChangeLanguage(1);
            localizationManager.LoadLocalizedText("localizedText_en.json");
            TranslateAllTexts();
        }
    }

    void TranslateAllTexts()
    {
        for (int i = 0; i < allTexts.Length; i++)
        {

            allTexts[i].text = localizationManager.GetLocalizedValue(allTexts[i].gameObject.name);
        }

    }

    public void LanguageChanged()
    {
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

    public int GetLanguage()
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
        speedGame.text = localizationManager.GetLocalizedValue(speedGame.gameObject.name) + sliderSpeed.value;
        SetGameSpeed(sliderSpeed.value);
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
        sliderSpeed.value = getGameSpeed();
        speedGame.text = localizationManager.GetLocalizedValue(speedGame.gameObject.name) + sliderSpeed.value;
    }

    public void ChangeSpeedButton()
    {
        float counter = sliderSpeed.value;

        if (counter == sliderSpeed.maxValue)
            counter = sliderSpeed.minValue;
        else
            counter++;

        SetGameSpeed(counter);
        SetGameSpeedToSlider();
    }

    //GAME DIFFICULTY
   public void SliderDifficultyChanged()
    {
       switch ((int)sliderDifficulty.value)
        {
            case 0:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("NoDuck");
                SetGameDifficulty(0);
                break;
            case 1:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("Easy");
                SetGameDifficulty(1);
                break;
            case 2:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("Medium");
                SetGameDifficulty(2);
                break;
            case 3:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("Hard");
                SetGameDifficulty(3);
                break;
            default:
                break;
        }
    }

   void SetGameDifficulty(float _difficulty)
    {
        PlayerPrefs.SetFloat("GameDifficulty", _difficulty);
    }

   public float GetGameDifficulty()
    {
        int value;
        switch ((int)PlayerPrefs.GetFloat("GameDifficulty")) {

            case 0: //SIN PATO
                value = 0;
                break;
            case 1:
                value = 6;
                break;
            case 2:
                value =  3;
                break;
            case 3:
                value = 2;
                break;
            default:
                value = 1;
                break;
        }

        return value;

    }

    void SetDifficultyToSlider()
    {
        sliderDifficulty.value = PlayerPrefs.GetFloat("GameDifficulty");

        switch ((int)sliderDifficulty.value)
        {
            case 0:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("NoDuck");
                break;
            case 1:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("Easy");
                break;
            case 2:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("Medium");
                break;
            case 3:
                difficultyText.text = localizationManager.GetLocalizedValue(difficultyText.gameObject.name) + localizationManager.GetLocalizedValue("Hard");
                break;
            default:
                break;
        }
    }

    public void ChangeDifficultyButton()
    {
       float counter = sliderDifficulty.value;

        if (counter == sliderDifficulty.maxValue)
            counter = sliderDifficulty.minValue;
        else
            counter++;

        SetGameDifficulty(counter);
        SetDifficultyToSlider();
    }

   
    //DEFAULT OPTIONS BUTTON
    public void DefaultOptionsButton()
    {
        DefaultOptions();
        CheckAllOptions();

    }

    void CheckAllOptions()
    {
        checkCurrentLanguage();
        checkVoiceActivated();
        fontCounter = GetUserFont();
        SetFontToLabels();
        checkAnimalSoundsActivated();
        SetGameSpeedToSlider();
        SetDifficultyToSlider();
        
    }
}




