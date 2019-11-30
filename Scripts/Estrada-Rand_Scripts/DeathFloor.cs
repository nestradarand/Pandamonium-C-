/*
Name: Noah Estrada-Rand
Student ID#: 2272490
Chapman email: estra146@mail.chapman.edu
Course Number and Section: CPSC 236-02
Assignment: Final Project: Pandamonium
*/
//needed to work with unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//applied to the death floor in each level to see if a player touches it and if so, they die
public class DeathFloor : MonoBehaviour
{
    //used to access each player
    public bool player1Death;
    public bool player2Death;
    private bool touched;
    void Start()//both characters alive by default
    {
        player2Death = false;
        player1Death = false;
    }


    private void OnCollisionEnter(Collision collision)//if player collides with death floor then the following occurs
    {
        if(collision.gameObject.CompareTag("Player1"))
        {
            player1Death = true;
            touched = true;
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            player2Death = true;
            touched = true;
        }
    }
    public bool beenTouched()//used by character manager to see if  aplayer touched the floor
    {
        if(touched)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string WhoHit()//returns who hit the floor
    {
        string returner;
        if(touched && player1Death)
        {
            returner = "Player1";
            return returner;
        }
        if(touched && player2Death)
        {
            returner = "Player2";
            return returner;
        }
        else
        {
            return "none";
        }
    }
}
