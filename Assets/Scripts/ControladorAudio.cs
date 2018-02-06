using UnityEngine;

public class ControladorAudio : MonoBehaviour {

    AudioSource audioSource;
    OptionsPreferences optionsController;

	void Start () {
        optionsController = GameObject.Find("GamePreferences").GetComponent<OptionsPreferences>();
        SearchObjects();
        EasyTTSUtil.Initialize(EasyTTSUtil.Spain);
    }
	

    public void ChangeLanguage(int value) {
        if (value == 0)
        {
            EasyTTSUtil.Initialize(EasyTTSUtil.Spain);
        }
        else if (value == 1)
        {
            EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);
        }
    }

    public void StopSpeak()
    {
        EasyTTSUtil.Stop();
    }

    void SearchObjects()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    
    public void PlaySound(string _name)
    {
        if(PlayerPrefs.GetInt("AnimalVoiceActivated")  == 1)
        {
            audioSource.clip = Resources.Load<AudioClip>(_name);
            audioSource.PlayDelayed(0.5f);
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void Speak(string _word)
    {
        if(PlayerPrefs.GetInt("VoiceActivated") == 1)
            EasyTTSUtil.SpeechAdd(_word);
    }
}
