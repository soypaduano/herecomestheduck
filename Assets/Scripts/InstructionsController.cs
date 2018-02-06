using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour {

    [SerializeField]
    ControladorAudio audioController;
    public GameObject[] allTexts;

	// Use this for initialization
	void Start () {
        audioController = GameObject.Find("AudioController").GetComponent<ControladorAudio>();
	}
    
    public void StopInstrucciones()
    {
        audioController.StopSpeak();
    }
      
    public void ReproducirInstrucciones()
    {
        foreach (GameObject txt in allTexts)
        {
            print(txt.name);
            string instruccion = txt.GetComponent<Text>().text;
            audioController.Speak(instruccion);
        }
    }
}
