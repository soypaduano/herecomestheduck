using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{

    List<Animal> animalList = new List<Animal>();
    GameObject popUpNewAnimal, instruction; //Panel donde se pondra el nuevo animal
    GameObject[] imagesAnimals;
    Text nameOfAnimal, soundOfAnimal;
    Image imageOfAnimalPopUp;
    ControladorAudio audioController;
    OptionsPreferences optionsContoller;
    bool showingAnimal;
    float timeOfImagePanel;
    int contadorAnimales;
    bool gameOver = false;
    [SerializeField]
    Text tituloInstruccion, instruccionInvidente;

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
        searchObjects();
        fillArray();
        timeOfImagePanel = optionsContoller.getGameSpeed();
        //TODO: Falta testear esto!
        /*if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            audioController.Speak(tituloInstruccion.text);
            audioController.Speak(instruccionInvidente.text);
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        else
        {
            audioController.Speak(tituloInstruccion.text);
        }*/
    }

    void fillArray()
    {
        int totalAnimals = 16;
        if (optionsContoller.getGameDifficulty() != 0)
        {
            totalAnimals = 15;
            int posibilidadPato = Random.Range(0, (int)optionsContoller.getGameDifficulty());
            
            if (posibilidadPato == 0)
                animalList.Add(new Animal("pato", "cuac cuac!"));
            else
                print("no hay pato");
        }

        List<int> usedValues = new List<int>();
        for (int i = 0; i < totalAnimals; i++)
        {
            int numeroRandom = Random.Range(0, Constantes.animales.Count);

            do
            {
                numeroRandom = Random.Range(0, Constantes.animales.Count);
            } while (Constantes.checkIfRandomInsideArray(usedValues, numeroRandom));

            usedValues.Add(numeroRandom);
            if(PlayerPrefs.GetInt("Language") == 0)
                animalList.Add(new Animal(Constantes.animales[numeroRandom], Constantes.soundAnimals[numeroRandom]));
            else
                animalList.Add(new Animal(Constantes.animales_Ingles[numeroRandom], Constantes.soundAnimals[numeroRandom]));
        }
    }
    

    void searchObjects()
    {
        optionsContoller = GameObject.Find("GamePreferences").GetComponent<OptionsPreferences>();
        popUpNewAnimal = GameObject.Find("PopUpAnimalNuevo");
        nameOfAnimal = GameObject.Find("TextoAnimal").GetComponent<Text>();
        audioController = GameObject.Find("AudioController").GetComponent<ControladorAudio>();
        soundOfAnimal = GameObject.Find("TextoSonidoAnimal").GetComponent<Text>();
        imageOfAnimalPopUp = GameObject.Find("ImagePopUpAnimal").GetComponent<Image>();
        showHidePopUp(false);
        imagesAnimals = GameObject.FindGameObjectsWithTag("Imagen");
        instruction = GameObject.Find("Instruccion");
        ocultarImagenes();
    }

    void ocultarImagenes()
    {
        foreach (GameObject imagen in imagesAnimals)
        {
            imagen.SetActive(false);
        }
    }

    void showHidePopUp(bool _bool)
    {
        popUpNewAnimal.SetActive(_bool);
        popUpNewAnimal.transform.parent.gameObject.SetActive(_bool); //Ocultamos a su padre, que es el fondo negro
    }

    void finishShowingAnimal()
    {
        timeOfImagePanel = optionsContoller.getGameSpeed();
        showHidePopUp(false);
        audioController.stopSound();
    }

    void restaurarJuego()
    {
        instruction.SetActive(true);
        gameOver = false;
        ocultarImagenes();
        contadorAnimales = 0;
        animalList.Clear();
        fillArray();
    }

    public void volverAJugar()
    {
        print("Executing volver a jugar");
        restaurarJuego();
    }

    public void touchScreen()
    {
        if (!gameOver)
        {
            instruction.SetActive(false);
            int numeroRandom = Random.Range(0, animalList.Count);
            Animal animal = animalList[numeroRandom];
            StartCoroutine(showAnimalCourutine(animal, numeroRandom));
            contadorAnimales++;
        }   
    }

    void setImage(string nombreAnimal)
    {
        //CUADRICULA
        imagesAnimals[contadorAnimales].SetActive(true);
        Image imagenAPoner = imagesAnimals[contadorAnimales].GetComponent<Image>();
        imagesAnimals[contadorAnimales].GetComponent<Image>().sprite = Resources.Load<Sprite>(nombreAnimal);
        //PANEL DE ANIMAL
        imageOfAnimalPopUp.sprite = Resources.Load<Sprite>(nombreAnimal);
    }


    IEnumerator showAnimalCourutine(Animal _animal, int _randomNumber)
    {
        setImage(_animal.name);
        showHidePopUp(true);
        nameOfAnimal.text = Constantes.FirstLetterToUpper(_animal.name);
        soundOfAnimal.text = "Sonido: " + _animal.sound + "!";
        audioController.speak(_animal.name);
        audioController.playSounds(_animal.name);
        animalList.RemoveAt(_randomNumber);

        yield return new WaitForSeconds(optionsContoller.getGameSpeed());
        finishShowingAnimal();

        if(_animal.name == "pato")
        {
            gameOver = true;
            yield return new WaitForSeconds(1f);
            showHidePopUp(true);
            nameOfAnimal.text = "¡Vuelve a intentarlo!";
            soundOfAnimal.text = "";
            imageOfAnimalPopUp.sprite = Resources.Load<Sprite>("animo");
            yield return new WaitForSeconds(4f);
            finishShowingAnimal();
            restaurarJuego();
        }
      
        if (contadorAnimales == 16)
        {
            gameOver = true;
            yield return new WaitForSeconds(1f);
            showHidePopUp(true); 
            nameOfAnimal.text = "¡Has ganado!";
            soundOfAnimal.text = "Enhorabuena";
            audioController.speak("Enhorabuena");
            audioController.playSounds("aplausos");
            imageOfAnimalPopUp.sprite = Resources.Load<Sprite>("gana");
            yield return new WaitForSeconds(4f);
            finishShowingAnimal();
            restaurarJuego();
        }
    }
}


