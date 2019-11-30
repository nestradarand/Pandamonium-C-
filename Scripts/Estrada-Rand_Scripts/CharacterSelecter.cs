/*
Name: Noah Estrada-Rand
Student ID#: 2272490
Chapman email: estra146 @mail.chapman.edu
Course Number and Section: CPSC 236-02
Assignment: Final Project: Pandamonium
*/
//needed to work with unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//gameobject to select characters by not being destroyed throughout scene loading
//also loads prefabs based on character selection
public class CharacterSelecter : MonoBehaviour
{
    // setting values so that there is no glitch when you try loading a scene without going through the character selection
    //these store prefabs as well as information concerning how many scenes have been loaded and how many choices made
    public int player1Choice;
    public int player2Choice;
    public int totalPlayerChoice;
    private int currentPrefab;
    public GameObject[] prefabs;
    private GameObject currentLevel;
    public GameObject thisThing;
    public int sceneCount = 0;
    public int sceneChecker;
    public int chosen1;
    public int chosen2;

    public GameObject [] player2Value;
    public Canvas thisCanvas;
    public GameObject firstScreen;
    public GameObject secondScreen;
    private int choiceCount;
    //buttons in the scene to select characters
    public GameObject first1;
    public GameObject first2;
    public GameObject first3;
    public GameObject first4;
    public GameObject second1;
    public GameObject second2;
    public GameObject second3;
    public GameObject second4;

    //buttons preventing selection once a character has already been chosen
    public GameObject noPanda;
    public GameObject noBird;
    public GameObject noFox;
    public GameObject noLizard;
    //booleans to determine what characters ahve been selected
    private bool pandaPicked;
    private bool birdPicked;
    private bool foxPicked;
    private bool lizardPicked;
    private int trueIndex;
    //allows a method to be called only once
    private bool once;
    private GameObject [] found;
    public GameObject thisObject;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(thisThing);//prevents destruction on scene loading
        choiceCount = 0;//number of choices is zero
        found = GameObject.FindGameObjectsWithTag("PrefabLoader");//looks for other objects with the tag
        if(found.Length >1)
        {
            Destroy(gameObject);//destroys itself if there is already a game object
        }

    }
    void OnEnable()//used to enable the second script after this
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)//checks to see if a new scene was loaded
    {
        sceneCount += 1;//adds to the scene count everytime a scene is loaded
        once = false;
        if(trueIndex ==11)//if the scene index is 11 (start screen) then the following runs
        {
            choiceCount = 0;
            sceneCount = 0;
            Debug.Log("Counts reset");
            thisCanvas.gameObject.SetActive(true);
            first1.gameObject.SetActive(true);
            first2.gameObject.SetActive(true);
            first3.gameObject.SetActive(true);
            first4.gameObject.SetActive(true);
            firstScreen.gameObject.SetActive(true);
            secondScreen.gameObject.SetActive(false);
         
        }
    //if only once choice has been made then player 2 buttons appear
        if(choiceCount>=1)
        {
            first1.gameObject.SetActive(false);
            first2.gameObject.SetActive(false);
            first3.gameObject.SetActive(false);
            first4.gameObject.SetActive(false);
            firstScreen.gameObject.SetActive(false);
            secondScreen.gameObject.SetActive(true);
            if(pandaPicked)//this and following if statements determine if a character has already been picked
            {
                noPanda.gameObject.SetActive(true);
            }
            if(birdPicked)
            {
                noBird.gameObject.SetActive(true);
            }
            if(foxPicked)
            {
                noFox.gameObject.SetActive(true);
            }
            if(lizardPicked)
            {
                noLizard.gameObject.SetActive(true);
            }
            //turns on the second wave of buttons for player 2
            second1.gameObject.SetActive(true);
            second2.gameObject.SetActive(true);
            second3.gameObject.SetActive(true);
            second4.gameObject.SetActive(true);
        }
        if(sceneCount >2)//if more than two scenes have gone by then all of the canvas is reset for next use
        {
            second1.gameObject.SetActive(false);
            second2.gameObject.SetActive(false);
            second3.gameObject.SetActive(false);
            second4.gameObject.SetActive(false);
            noPanda.gameObject.SetActive(false);
            noLizard.gameObject.SetActive(false);
            noBird.gameObject.SetActive(false);
            noFox.gameObject.SetActive(false);
            foxPicked = false;
            lizardPicked = false;
            pandaPicked = false;
            birdPicked = false;
            thisCanvas.gameObject.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()//constantly checks for new choices from the players
    {
        chosen1 = player1Choice;
        chosen2 = player2Choice;
        sceneChecker = sceneCount;
        trueIndex = SceneManager.GetActiveScene().buildIndex;//tells which index the scene currently is placed at
        if(trueIndex == 10)//if returned to the main menu then the loaded prefab is destroyed
        {
            Destroy(currentLevel);
        }

        if(!once)
        {
            if (trueIndex == 3 || trueIndex == 5 || trueIndex == 4)//loads a level once per scene load
            {
                preFabLoader();
                once = true;
            }
        }


    }

    public void PandaSelected1()//runs if panda is selected for player 1
    {
        player1Choice = 1;
        Debug.Log("Panda Selected");
        choiceCount += 1;
        pandaPicked = true;
    }

    public void BirdSelected1()//runs if bird is selected for player 1
    {
        player1Choice = 2;
        choiceCount += 1;
        birdPicked = true;
    }

    public void FoxSelected1()//runs if fox is selected for player 1
    {
        player1Choice = 3;
        choiceCount += 1;
        foxPicked = true;
    }

    public void LizardSelected1()//runs if lizard is selected for player 1
    {
        player1Choice = 4;
        choiceCount += 1;
        lizardPicked = true;
    }

    public void PandaSelected2()
    {
        player2Choice = 10;
        choiceCount += 1;
    }

    public void BirdSelected2()
    {
        player2Choice = 20;
        choiceCount += 1;
    }

    public void FoxSelected2()
    {
        player2Choice = 30;
        choiceCount += 1;
    }

    public void LizardSelected2()
    {
        player2Choice = 40;
        choiceCount += 1;
    }

    public void characterNumber()//calculates a unique number based on values associated with selections
    {
        totalPlayerChoice = player1Choice + player2Choice;
    }


    
    public void preFabLoader()//loads prefab determined by total player choice
    {
        // player 1 then player 2
        characterNumber();
        if (currentLevel != null)//checks to see if there is a current level loaded, if there is it must be destroyed
        {
            Destroy(currentLevel);
        }
        //panda bird
        if (totalPlayerChoice == 12)
        {
            currentLevel = Instantiate(prefabs[0], prefabs[0].transform.position, prefabs[0].transform.rotation) as GameObject;
        }
        //panda fox
        if (totalPlayerChoice == 42)
        {
            currentLevel = Instantiate(prefabs[1], prefabs[1].transform.position, prefabs[1].transform.rotation) as GameObject;
        }
        //panda lizard
        if (totalPlayerChoice == 32)
        {
            currentLevel = Instantiate(prefabs[2], prefabs[2].transform.position, prefabs[2].transform.rotation) as GameObject;
        }
        //bird panda
        if (totalPlayerChoice == 21)
        {
            currentLevel = Instantiate(prefabs[3], prefabs[3].transform.position, prefabs[3].transform.rotation) as GameObject;
        }
        // bird fox
        if (totalPlayerChoice == 41)
        {
            currentLevel = Instantiate(prefabs[4], prefabs[4].transform.position, prefabs[4].transform.rotation) as GameObject;
        }
        // bird lizard
        if (totalPlayerChoice == 31)
        {
            currentLevel = Instantiate(prefabs[5], prefabs[5].transform.position, prefabs[5].transform.rotation) as GameObject;
        }
        // fox panda
        if (totalPlayerChoice == 14)
        {
            currentLevel = Instantiate(prefabs[6], prefabs[6].transform.position, prefabs[6].transform.rotation) as GameObject;
        }
        // fox bird
        if (totalPlayerChoice == 34)
        {
            currentLevel = Instantiate(prefabs[7], prefabs[7].transform.position, prefabs[7].transform.rotation) as GameObject;
        }
        // fox lizard
        if (totalPlayerChoice == 24)
        {
            currentLevel = Instantiate(prefabs[8], prefabs[8].transform.position, prefabs[8].transform.rotation) as GameObject;
        }
        // lizard panda
        if (totalPlayerChoice == 13)
        {
            currentLevel = Instantiate(prefabs[9], prefabs[9].transform.position, prefabs[9].transform.rotation) as GameObject;
        }
        // lizard bird
        if (totalPlayerChoice == 23)
        {
            currentLevel = Instantiate(prefabs[10], prefabs[10].transform.position, prefabs[10].transform.rotation) as GameObject;
        }
        // lizard fox
        if (totalPlayerChoice == 43)
        {
            currentLevel = Instantiate(prefabs[11], prefabs[11].transform.position, prefabs[11].transform.rotation) as GameObject;
        }
        
    }
  
}
