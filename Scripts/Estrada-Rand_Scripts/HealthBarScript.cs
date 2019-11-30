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
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    //this code is from AlexanderZotov on youtube
    //variables to store character health and access the character manager
    Image healthBar;
    public  float maxHealth;
    public  float health;
    public CharacterManager cm;
    public bool player1Status;
    public bool player2Status;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();//gets the healthbar image referenced in the instance variables
        if(player2Status)//if player 2 is hit then their life is decremented
        {
            maxHealth = cm.player2Life;
        }
        if(player1Status)//if player 1 is hit then their life is decremented
        {
            maxHealth = cm.player1Life;
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()//changes the healthbar fill by health/maxhealth
    {
        healthBar.fillAmount = health / maxHealth;
    }
    public void Reset()
    {
        health = maxHealth;
    }
}
