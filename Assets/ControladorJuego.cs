using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{

    List<Animal> animalList = new List<Animal>();
    GameObject popUpNewAnimal; //Panel donde se pondra el nuevo animal
    GameObject[] imagesAnimals;
    Text nameOfAnimal, soundOfAnimal;
    Image imageOfAnimalPopUp;
    ControladorAudio controladorAudio;
    bool showingAnimal, showingWin;
    float timeOfImagePanel = 5f;
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

    void Start()
    {
        SearchObjects();
        FillArray();
    }

    void FillArray()
    {
        List<int> usedValues = new List<int>();
        for (int i = 0; i < 16; i++)
        {
            int numeroRandom = Random.Range(0, Constantes.animales.Count);

            do
            {
                numeroRandom = Random.Range(0, Constantes.animales.Count);
            } while (Constantes.checkIfRandomInsideArray(usedValues, numeroRandom));

            usedValues.Add(numeroRandom);
            animalList.Add(new Animal(Constantes.animales[numeroRandom], Constantes.soundAnimals[numeroRandom]));
        }
    }
    

    void SearchObjects()
    {
        popUpNewAnimal = GameObject.Find("PopUpAnimalNuevo");
        nameOfAnimal = GameObject.Find("TextoAnimal").GetComponent<Text>();
        controladorAudio = GameObject.Find("AudioController").GetComponent<ControladorAudio>();
        soundOfAnimal = GameObject.Find("TextoSonidoAnimal").GetComponent<Text>();
        imageOfAnimalPopUp = GameObject.Find("ImagePopUpAnimal").GetComponent<Image>();
        ShowHidePopUp(false);
        imagesAnimals = GameObject.FindGameObjectsWithTag("Imagen");
        OcultarImagenes();
    }

    void OcultarImagenes()
    {
        foreach (GameObject imagen in imagesAnimals)
        {
            imagen.SetActive(false);
        }
    }

    void Update()
    {
        //PANEL DE LA IMAGEN SE MUESTRA
        if (showingAnimal)
        {
            timeOfImagePanel -= Time.deltaTime;

            if (timeOfImagePanel < 0)
            {
                showingAnimal = false;
                timeOfImagePanel = 1f;
                ShowHidePopUp(false);
                controladorAudio.StopSound();

                //CUANDO JUGADOR GANAA.....
                if (contadorAnimales == 16)
                {
                    showingWin = true;
                    timeOfImagePanel = 5f;
                    ShowHidePopUp(true);
                    nameOfAnimal.text = "YOU FUCKING WON";
                    soundOfAnimal.text = "YEY BRO";
                }

            }
        }

        if (showingWin)
        {
            timeOfImagePanel -= Time.deltaTime;
            //Y OCULTAMOS EL PANEL DE GANAR
            if(timeOfImagePanel < 0)
            {
                showingWin = false;
                timeOfImagePanel = 5f;
                ShowHidePopUp(false);
                RestaurarJuego();
            }
        }
    }

    void ShowHidePopUp(bool _bool)
    {
        popUpNewAnimal.SetActive(_bool);
        popUpNewAnimal.transform.parent.gameObject.SetActive(_bool); //Ocultamos a su padre, que es el fondo negro
    }

    void RestaurarJuego()
    {
        OcultarImagenes();
        contadorAnimales = 0;
        animalList.Clear();
        FillArray();
    }

    public void TouchScreen()
    {

        int numeroRandom = Random.Range(0, animalList.Count);
        Animal animal = animalList[numeroRandom];

        //USUARIO PIERDE
        if (animal.name == "pto")
        {
            showingAnimal = true;
            ShowHidePopUp(true);
            nameOfAnimal.text = animal.name;
            soundOfAnimal.text = "Sonido: " + Constantes.FirstLetterToUpper(animal.sound) + "!";
            controladorAudio.PlaySound(animal.name);
            SetImage(animal.name);
            RestaurarJuego();
        }
        else
        {

            //SIGUE JUGANDO
            SetImage(animal.name);
            contadorAnimales++;
            showingAnimal = true;
            ShowHidePopUp(true);
            nameOfAnimal.text = Constantes.FirstLetterToUpper(animal.name);
            soundOfAnimal.text = "Sonido: " + animal.sound + "!";
            controladorAudio.Speak(animal.name);
            controladorAudio.PlaySound(animal.name);
            animalList.RemoveAt(numeroRandom);
        }
    }

    void SetImage(string nombreAnimal)
    {
        //CUADRICULA
        imagesAnimals[contadorAnimales].SetActive(true);
        Image imagenAPoner = imagesAnimals[contadorAnimales].GetComponent<Image>();
        imagesAnimals[contadorAnimales].GetComponent<Image>().sprite = Resources.Load<Sprite>(nombreAnimal);
        //PANEL DE ANIMAL
        imageOfAnimalPopUp.sprite = Resources.Load<Sprite>(nombreAnimal);
    }
}
