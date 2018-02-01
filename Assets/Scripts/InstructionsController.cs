using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour {

    [SerializeField]
    ControladorAudio audioController;
    GameObject[] allTexts;

	// Use this for initialization
	void Start () {
        allTexts = GameObject.FindGameObjectsWithTag("TextInstrucciones");
	}


    public void ReproducirInstrucciones()
    {
        foreach(GameObject txt in allTexts)
        {
            audioController.PlaySound(txt.GetComponent<Text>().text);
        }
    }
}
