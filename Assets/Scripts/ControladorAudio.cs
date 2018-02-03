using UnityEngine;

public class ControladorAudio : MonoBehaviour {

    AudioSource audioSource;

	void Start () {
        SearchObjects();
        EasyTTSUtil.Initialize(EasyTTSUtil.Spain);
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
