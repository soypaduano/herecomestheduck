﻿using UnityEngine;
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



    void Awake()
    {
        allTexts = GameObject.FindObjectsOfType<Text>();
    }

    void Start()
    {

        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            DefaultOptions();
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        else
            checkAllOptions();

    }

    void DefaultOptions()
    {
        setVoiceActivated(1);
        setAnimalSoundsActivated(1);
        setUserFont(0);
        setGameSpeed(2);
        //SetGameDifficulty(1);
        //SetLanguage(0);
    }

    //Human Voice 
    void checkVoiceActivated()
    {
        if (PlayerPrefs.GetInt("VoiceActivated") == 1)
            HumanVoiceActivated.text = "Voz Activada: Sí";
        else
            HumanVoiceActivated.text = "Voz Activada: No";

    }

    public void voiceHasBeenChanged()
    {
        if (PlayerPrefs.GetInt("VoiceActivated") == 1)
            setVoiceActivated(0);
        else
            setVoiceActivated(1);

        checkVoiceActivated();
    }

    public void setVoiceActivated(int _voice)
    {
        PlayerPrefs.SetInt("VoiceActivated", _voice);
    }

    //Animal Voice
    void checkAnimalSoundsActivated()
    {
        if (PlayerPrefs.GetInt("AnimalVoiceActivated") == 1)
            AnimalVoiceActivated.text = "Voz de animal: Sí";
        else
            AnimalVoiceActivated.text = "Voz de animal: No";
    }

    public void animalSoundHasBeenChanged()
    {
        if (PlayerPrefs.GetInt("AnimalVoiceActivated") == 0) {
            setAnimalSoundsActivated(1);
        } else
            setAnimalSoundsActivated(0);

        checkAnimalSoundsActivated();
    }

    public void setAnimalSoundsActivated(int _animal)
    {
        PlayerPrefs.SetInt("AnimalVoiceActivated", _animal);
    }

    //Language
  /*  void checkCurrentLanguage()
    {
        if(GetLanguage() == 0){
            languageText.text = "Idioma: Español";
            flag.sprite = Resources.Load<Sprite>("spanish_flag");
            GameObject.Find("AudioController").GetComponent<ControladorAudio>().ChangeLanguage(0);
            localizationManager.LoadLocalizedText("localizedText_es.json");
        } else if(GetLanguage() == 1)
        {
            languageText.text = "Language: English";
            flag.sprite = Resources.Load<Sprite>("english_flag");
            GameObject.Find("AudioController").GetComponent<ControladorAudio>().ChangeLanguage(1);
            localizationManager.LoadLocalizedText("localizedText_en.json");
        }

        TranslateAllTexts();
        CheckAllOptionsAfterLanguage();
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
    }*/

    //FONTS
    void setUserFont(int _fuente)
    {
        PlayerPrefs.SetInt("Font", _fuente);
    }

    int getUserFont()
    {
        return PlayerPrefs.GetInt("Font");
    }

    void setFontToLabels()
    {
        for (int i = 0; i < allTexts.Length; i++)
        {
            allTexts[i].font = fuentes[fontCounter];
        }
        setUserFont(fontCounter);
    }

    public void changeFontButton()
    {
        fontCounter++;
        //Comprobamos si está al final
        if (fontCounter == fuentes.Length)
        {
            fontCounter = 0;
        }

        setFontToLabels();

    }

    //SPEED SLIDER
    public void sliderValueChanged()
    {
        speedGame.text = "Velocidad: " + sliderSpeed.value;
        setGameSpeed(sliderSpeed.value);
    }

    void setGameSpeed(float _speed)
    {
        PlayerPrefs.SetFloat("GameSpeed", _speed);
    }

    public float getGameSpeed()
    {
        return PlayerPrefs.GetFloat("GameSpeed");
    }

    void setGameSpeedToSlider()
    {
        sliderSpeed.value = getGameSpeed();
        speedGame.text = "Velocidad: " + sliderSpeed.value;
    }

    public void changeSpeedButton()
    {
        float counter = sliderSpeed.value;

        if (counter == sliderSpeed.maxValue)
            counter = sliderSpeed.minValue;
        else
            counter++;

        setGameSpeed(counter);
        setGameSpeedToSlider();
    }

    //GAME DIFFICULTY
   public void sliderDifficultyChanged()
    {
       switch ((int)sliderDifficulty.value)
        {
            case 0:
                difficultyText.text = "Dificultad: sin pato";
                setGameDifficulty(0);
                break;
            case 1:
                difficultyText.text = "Dificultad: facil";
                setGameDifficulty(1);
                break;
            case 2:
                difficultyText.text = "Dificultad: mediano";
                setGameDifficulty(2);
                break;
            case 3:
                difficultyText.text = "Dificultad: dificil";
                setGameDifficulty(3);
                break;
            default:
                break;
        }
    }

   void setGameDifficulty(float _difficulty)
    {
        PlayerPrefs.SetFloat("GameDifficulty", _difficulty);
    }

   public float getGameDifficulty()
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

    void setDifficultyToSlider()
    {
        sliderDifficulty.value = PlayerPrefs.GetFloat("GameDifficulty");

        switch ((int)sliderDifficulty.value)
        {
            case 0:
                difficultyText.text = "Dificultad: sin pato";
                setGameDifficulty(0);
                break;
            case 1:
                difficultyText.text = "Dificultad: facil";
                setGameDifficulty(1);
                break;
            case 2:
                difficultyText.text = "Dificultad: mediano";
                setGameDifficulty(2);
                break;
            case 3:
                difficultyText.text = "Dificultad: dificil";
                setGameDifficulty(3);
                break;
            default:
                break;
        }
    }

    public void changeDifficultyButton()
    {
       float counter = sliderDifficulty.value;

        if (counter == sliderDifficulty.maxValue)
            counter = sliderDifficulty.minValue;
        else
            counter++;

        setGameDifficulty(counter);
        setDifficultyToSlider();
    }

   
    //DEFAULT OPTIONS BUTTON
    public void defaultOptionsButton()
    {
        DefaultOptions();
        checkAllOptions();

    }

    void checkAllOptions()
    {
        checkVoiceActivated();
        fontCounter = getUserFont();
        setFontToLabels();
        checkAnimalSoundsActivated();
        setGameSpeedToSlider();
        setDifficultyToSlider();
        
    }

    public void checkAllOptionsAfterLanguage()
    {
        checkVoiceActivated();
        checkAnimalSoundsActivated();
        setGameSpeedToSlider();
        setDifficultyToSlider();
    }
}




