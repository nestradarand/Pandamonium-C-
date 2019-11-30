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
//quits the game when the button is pushed
public class ExitGame : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
