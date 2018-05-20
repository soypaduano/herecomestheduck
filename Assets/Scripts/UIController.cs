using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler{

    //Variables del juego
    bool isTouchingInstrucciones, isTouchingControles;
    float tiempoInstrucciones, tiempoControles;
    //Paneles
    public GameObject panelInstrucciones, panelControles;
    [SerializeField]
    Image imageInstruction, imageControl;
    [SerializeField] //TODO: Queremos esto serialized?
    GameObject botonInstrucciones, botonControles;
    InstructionsController instructionsController;
    ControladorAudio audioController;


	void Start () {
        panelInstrucciones.SetActive(false);
        panelControles.SetActive(false);
        audioController = GameObject.Find("AudioController").GetComponent<ControladorAudio>();
    }

	
	void Update () {
        if (isTouchingInstrucciones)
        {
            tiempoInstrucciones += Time.deltaTime;
            if (tiempoInstrucciones > 2.0f)
            {
                panelInstrucciones.SetActive(true);
                isTouchingInstrucciones = false;
            }
        }

        if (isTouchingControles)
        {
            tiempoControles += Time.deltaTime;
            if (tiempoControles > 2.0f)
            {
                panelControles.SetActive(true);
                isTouchingControles = false;
            }
        }
	}

    public void OnPointerUp(PointerEventData data)
    {
        //Reiniciamos todo lo de instrucciones
        isTouchingInstrucciones = false;
        tiempoInstrucciones = 0;
        //Reiniciamos todo lo de controles
        isTouchingControles = false;
        tiempoControles = 0;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(gameObject.name == "Instrucciones")
        {
            if (botonControles.activeInHierarchy)
            {
                audioController.speak(gameObject.transform.GetComponentInChildren<Text>().text);
                panelInstrucciones.SetActive(true);
                botonControles.SetActive(false);
                botonInstrucciones.GetComponentInChildren<Text>().text = "Cerrar";
                imageInstruction.sprite =  Resources.Load<Sprite>("close");
                instructionsController = GameObject.Find("ControladorInstrucciones").GetComponent<InstructionsController>();
                instructionsController.reproducirInstrucciones();
            }  else
            {
                audioController.stopSpeak();
                panelInstrucciones.SetActive(false);
                botonControles.SetActive(true);
                botonInstrucciones.GetComponentInChildren<Text>().text = "Instrucciones";
                imageInstruction.sprite =  Resources.Load<Sprite>("info");
                audioController.speak(GameObject.FindGameObjectWithTag("TituloPantallaJuego").GetComponent<Text>().text);

            }
        } else if(gameObject.name == "Controles")
        {
            if (botonInstrucciones.activeInHierarchy)
            {
                audioController.speak(gameObject.transform.GetComponentInChildren<Text>().text);
                panelControles.SetActive(true);
                botonInstrucciones.SetActive(false);
                botonControles.GetComponentInChildren<Text>().text = "Cerrar";
                imageControl.sprite = Resources.Load<Sprite>("close");
            } else
            {
                audioController.speak(GameObject.FindGameObjectWithTag("TituloPantallaJuego").GetComponent<Text>().text);
                panelControles.SetActive(false);
                botonInstrucciones.SetActive(true);
                botonControles.GetComponentInChildren<Text>().text = "Controles";
                imageControl.sprite = Resources.Load<Sprite>("settings");
            }
        }
    }
}
