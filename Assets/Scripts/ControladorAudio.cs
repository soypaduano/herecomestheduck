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
        print(_name);
        audioSource.clip = Resources.Load<AudioClip>(_name);
        audioSource.PlayDelayed(0.5f);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void Speak(string _word)
    {
        EasyTTSUtil.SpeechAdd(_word);
    }
}
