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
    Image imageFillInstrucciones, imageFillControles, imageInstruction, imageControl;
    [SerializeField] //TODO: Queremos esto serialized?
    GameObject botonInstrucciones, botonControles;
    InstructionsController instructionsController;

	// Use this for initialization
	void Start () {
        panelInstrucciones.SetActive(false);
        panelControles.SetActive(false);
    }

	
	// Update is called once per frame
	void Update () {
        if (isTouchingInstrucciones)
        {
            tiempoInstrucciones += Time.deltaTime;
            imageFillInstrucciones.fillAmount = imageFillInstrucciones.fillAmount + (Time.deltaTime / 2f);
            if (tiempoInstrucciones > 2.0f)
            {
                print("Ha tocado el botón de instrucciones mas de 2 segunods");
                imageFillInstrucciones.fillAmount = 0;
                panelInstrucciones.SetActive(true);
                isTouchingInstrucciones = false;
            }
        }

        if (isTouchingControles)
        {
            tiempoControles += Time.deltaTime;
            imageFillControles.fillAmount = imageFillControles.fillAmount + (Time.deltaTime / 2f);
            if (tiempoControles > 2.0f)
            {
                imageFillControles.fillAmount = 0;
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
        imageFillInstrucciones.fillAmount = 0;
        //Reiniciamos todo lo de controles
        isTouchingControles = false;
        tiempoControles = 0;
        imageFillControles.fillAmount = 0;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(gameObject.name == "BotonInstrucciones")
        {
            if (botonControles.activeInHierarchy)
            {
                panelInstrucciones.SetActive(true);
                botonControles.SetActive(false);
                botonInstrucciones.GetComponentInChildren<Text>().text = "Cerrar";
                imageInstruction.sprite =  Resources.Load<Sprite>("close");
                instructionsController = GameObject.Find("ControladorInstruccionesº").GetComponent<InstructionsController>();
                instructionsController.ReproducirInstrucciones();
            }  else
            {
                panelInstrucciones.SetActive(false);
                botonControles.SetActive(true);
                botonInstrucciones.GetComponentInChildren<Text>().text = "Instrucciones";
                imageInstruction.sprite =  Resources.Load<Sprite>("info");
            }
        } else if(gameObject.name == "BotonControles")
        {
            if (botonInstrucciones.activeInHierarchy)
            {
                panelControles.SetActive(true);
                botonInstrucciones.SetActive(false);
                botonControles.GetComponentInChildren<Text>().text = "Cerrar";
                imageControl.sprite = Resources.Load<Sprite>("close");
            } else
            {
                panelControles.SetActive(false);
                botonInstrucciones.SetActive(true);
                botonControles.GetComponentInChildren<Text>().text = "Controles";
                imageControl.sprite = Resources.Load<Sprite>("settings");
            }
            
        }

    }
}
