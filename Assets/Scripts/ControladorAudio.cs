using UnityEngine;

public class ControladorAudio : MonoBehaviour {

    AudioSource audioSource;
    OptionsPreferences optionsController;

	void Start () {
        optionsController = GameObject.Find("GamePreferences").GetComponent<OptionsPreferences>();
        searchObjects();
        EasyTTSUtil.Initialize(EasyTTSUtil.Spain);
    }
	

    public void changeLanguage(int value) {
        if (value == 0)
        {
            EasyTTSUtil.Initialize(EasyTTSUtil.Spain);
        }
        else if (value == 1)
        {
            EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);
        }
    }

    public void stopSpeak()
    {
        EasyTTSUtil.StopSpeech();
    }

    void searchObjects()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    
    public void playSounds(string _name)
    {
        if(PlayerPrefs.GetInt("AnimalVoiceActivated")  == 1)
        {
            audioSource.clip = Resources.Load<AudioClip>(_name);
            audioSource.PlayDelayed(0.5f);
        }
    }

    public void stopSound()
    {
        audioSource.Stop();
    }

    public void speak(string _word)
    {
        if(PlayerPrefs.GetInt("VoiceActivated") == 1)
            EasyTTSUtil.SpeechAdd(_word);
    }
}
