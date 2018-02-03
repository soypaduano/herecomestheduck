﻿using System.Collections;
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
    OptionsPreferences optionsContoller;
    bool showingAnimal;
    float timeOfImagePanel;
    int contadorAnimales;
    bool gameOver = false;

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
        timeOfImagePanel = optionsContoller.getGameSpeed();
        print(timeOfImagePanel);
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
        optionsContoller = GameObject.Find("GamePreferences").GetComponent<OptionsPreferences>();
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

    void ShowHidePopUp(bool _bool)
    {
        popUpNewAnimal.SetActive(_bool);
        popUpNewAnimal.transform.parent.gameObject.SetActive(_bool); //Ocultamos a su padre, que es el fondo negro
    }

    void FinishShowingAnimal()
    {
        timeOfImagePanel = optionsContoller.getGameSpeed();
        ShowHidePopUp(false);
        controladorAudio.StopSound();
    }

    void RestaurarJuego()
    {
        gameOver = false;
        OcultarImagenes();
        contadorAnimales = 0;
        animalList.Clear();
        FillArray();
    }

    public void TouchScreen()
    {
        if (!gameOver)
        {
            int numeroRandom = Random.Range(0, animalList.Count);
            Animal animal = animalList[numeroRandom];
            StartCoroutine(ShowAnimalCorutine(animal, numeroRandom));
            contadorAnimales++;
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


    IEnumerator ShowAnimalCorutine(Animal _animal, int _randomNumber)
    {

        SetImage(_animal.name);
        ShowHidePopUp(true);
        nameOfAnimal.text = Constantes.FirstLetterToUpper(_animal.name);
        soundOfAnimal.text = "Sonido: " + _animal.sound + "!";
        controladorAudio.Speak(_animal.name);
        controladorAudio.PlaySound(_animal.name);
        animalList.RemoveAt(_randomNumber);

        yield return new WaitForSeconds(PlayerPrefs.GetFloat("GameSpeed"));
        FinishShowingAnimal();

        if(_animal.name == "pato")
        {
            gameOver = true;
            yield return new WaitForSeconds(1f);
            ShowHidePopUp(true);
            nameOfAnimal.text = "¡Vuelve a intentarlo!";
            soundOfAnimal.text = "";
            imageOfAnimalPopUp.sprite = Resources.Load<Sprite>("animo");
            yield return new WaitForSeconds(4f);
            FinishShowingAnimal();
            RestaurarJuego();
        }
      
        if (contadorAnimales == 16)
        {
            gameOver = true;
            yield return new WaitForSeconds(1f);
            ShowHidePopUp(true); 
            nameOfAnimal.text = "¡Has ganado!";
            soundOfAnimal.text = "Enhorabuena";
            controladorAudio.Speak("Enhorabuena");
            controladorAudio.PlaySound("aplausos");
            imageOfAnimalPopUp.sprite = Resources.Load<Sprite>("win");
            yield return new WaitForSeconds(4f);
            FinishShowingAnimal();
            RestaurarJuego();
        }
    }
}

