/*
Name: Robby Jones
Student ID#: 2295678
Chapman email: robejones@chapman.edu
Course Number and Section: CPSC 236-02
Assignment: Final Project: Pandamonium
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//used to load a scene by a specific index
public class LoadSceneOnClick : MonoBehaviour
{ 


    public void LoadByIndex(int sceneIndex)//whatever int is specified in the inspector will be jumped to from this script
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
