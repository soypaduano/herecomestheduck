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
    Image imageInstrucciones, imageControles;

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
            imageInstrucciones.fillAmount = imageInstrucciones.fillAmount + (Time.deltaTime / 2f);
            if (tiempoInstrucciones > 2.0f)
            {
                print("Ha tocado el botón de instrucciones mas de 2 segunods");
                imageInstrucciones.fillAmount = 0;
                panelInstrucciones.SetActive(true);
                isTouchingInstrucciones = false;
            }
        }

        if (isTouchingControles)
        {
            tiempoControles += Time.deltaTime;
            imageControles.fillAmount = imageControles.fillAmount + (Time.deltaTime / 2f);
            if (tiempoControles > 2.0f)
            {
                imageControles.fillAmount = 0;
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
        imageInstrucciones.fillAmount = 0;
        //Reiniciamos todo lo de controles
        isTouchingControles = false;
        tiempoControles = 0;
        imageControles.fillAmount = 0;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(gameObject.name == "BotonInstrucciones")
        {
            isTouchingInstrucciones = true;
            isTouchingControles = false;
        } else if(gameObject.name == "BotonControles")
        {
            isTouchingControles = true;
            isTouchingInstrucciones = false;
        }

    }
}
