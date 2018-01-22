using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour {

    List<Animal> ListaDeAnimales = new List<Animal>();
    GameObject popUpAnimalNuevo, panelWin; //Panel donde se pondra el nuevo animal
    GameObject[] imagenesAnimales;
    Text nombreAnimal;
    Text sonidoAnimal;
    Image imagenAnimalPopUp;
    ControladorAudio controladorAudio;
    bool mostrandoImagen;
    float tiempoImagen = 4f;
    int contadorAnimales;

    public struct Animal
    {
        public string sound;
        public string name;

        public Animal(string _name, string _sound)
        {
            sound = _sound;
            name = _name;
        }
    }

    void Start() {
        SearchObjects();
        rellenarArray();
        EasyTTSUtil.Initialize(EasyTTSUtil.Spain);
    }

    void rellenarArray()
    {
        ListaDeAnimales.Add(new Animal("pato", "cuac cuac"));
        int counter = 0;
        foreach(string animal in Constantes.animales)
        {
            if(counter < 15)
            {
                ListaDeAnimales.Add(new Animal(Constantes.animales[counter], Constantes.soundAnimals[counter]));
                counter++;
            }
            print(ListaDeAnimales.Count);
        }
    }

    void SearchObjects()
    {
        popUpAnimalNuevo = GameObject.Find("PopUpAnimalNuevo");
        nombreAnimal = GameObject.Find("TextoAnimal").GetComponent<Text>();
        controladorAudio = GameObject.Find("AudioController").GetComponent<ControladorAudio>();
        sonidoAnimal = GameObject.Find("TextoSonidoAnimal").GetComponent<Text>();
        imagenAnimalPopUp = GameObject.Find("ImagePopUpAnimal").GetComponent<Image>();
        ShowHidePopUp(false);
        imagenesAnimales = GameObject.FindGameObjectsWithTag("Imagen");
        OcultarImagenes();
    }


    void OcultarImagenes()
    {
        foreach (GameObject imagen in imagenesAnimales)
        {
            imagen.SetActive(false);
        }
    }

    void Update()
    {
        if (mostrandoImagen)
        {
            tiempoImagen -= Time.deltaTime;

            if (tiempoImagen < 0)
            {
                mostrandoImagen = false;
                tiempoImagen = 3f;
                ShowHidePopUp(false);
            }
        }
    }

    void ShowHidePopUp(bool _bool)
    {
        popUpAnimalNuevo.SetActive(_bool);
        popUpAnimalNuevo.transform.parent.gameObject.SetActive(_bool); //Ocultamos a su padre, que es el fondo negro
    }

    void RestaurarJuego()
    {
        OcultarImagenes();
        contadorAnimales = 0;
        ListaDeAnimales.Clear();
        rellenarArray();

    }

    public void TouchScreen()
    {
        int numeroRandom = Random.Range(0, ListaDeAnimales.Count);
        string animal = ListaDeAnimales[numeroRandom].name;

        if (animal == "pato") 
        {
            mostrandoImagen = true;
            ShowHidePopUp(true);
            nombreAnimal.text = "Pato";
            sonidoAnimal.text = "Cuac cuac cuac";
            RestaurarJuego();
        }
        else
        {
            //SI USUARIO GANA
            if (contadorAnimales == 15)
            {
                ShowHidePopUp(true);
                mostrandoImagen = true;
                nombreAnimal.text = "Enhorabuena";
                sonidoAnimal.text = "Has ganado";
                EasyTTSUtil.SpeechAdd("Enhorabuena, has ganado");
                RestaurarJuego();
                return;
            }

            //SIGUE JUGANDO
            SetImage(animal);
            contadorAnimales++;
            mostrandoImagen = true;
            ShowHidePopUp(true);
            nombreAnimal.text = Constantes.FirstLetterToUpper(ListaDeAnimales[numeroRandom].name);
            sonidoAnimal.text = ListaDeAnimales[numeroRandom].sound + "!!";
            EasyTTSUtil.SpeechAdd(ListaDeAnimales[numeroRandom].name);
            controladorAudio.PlaySound(ListaDeAnimales[numeroRandom].name);
            ListaDeAnimales.RemoveAt(numeroRandom);
        }           
    }


    void SetImage(string nombreAnimal)
    {
        //CUADRICULA
        imagenesAnimales[contadorAnimales].SetActive(true);
        Image imagenAPoner = imagenesAnimales[contadorAnimales].GetComponent<Image>();
        print(imagenAPoner.gameObject.name);
        imagenesAnimales[contadorAnimales].GetComponent<Image>().sprite = Resources.Load<Sprite>(nombreAnimal);
        //PANEL DE ANIMAL
        imagenAnimalPopUp.sprite = Resources.Load<Sprite>(nombreAnimal);
    }
}
