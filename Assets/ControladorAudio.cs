using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControladorAudio : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        SearchObjects();
	}
	
    void SearchObjects()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    public void PlaySound(string _name)
    {
        print(_name);
        audioSource.clip = Resources.Load<AudioClip>(_name);
        audioSource.Play();
    }



	// Update is called once per frame
	void Update () {
		
	}
}
