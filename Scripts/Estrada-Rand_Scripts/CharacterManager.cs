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

public class CharacterManager : MonoBehaviour
{
    //used to references players, who has won and healthbars
    public Character player1;
    public Character player2;
    public int player1Life;
    public int player2Life;
    private string winner;
    public HealthBarScript player1HealthBar;
    public HealthBarScript player2HealthBar;
    // Start is called before the first frame update
    void Start()//used to set variables equal to each character's unique health level
    {
        player1Life = player1.health;
        player2Life = player2.health;
        player1HealthBar.maxHealth = player1.health;
        player2HealthBar.maxHealth = player2.health;
    }

    // Update is called once per frame
    void Update()
    {
        if(player1.BeenHit())//checks to see if player1 was hit by an attack
        {
            //following decrements life by calling method from character script
            Debug.Log("player 1hit " +player2.facingRight);
            if(!player1.heavy)
            {
                player1Life -= player2.lightAttackPower;
            }
            else
            {
                player1Life -= player2.heavyAttackPower;
            }          
            //updates healthbar as well
            player1.AllHitInfo(player2.hitForce, player2.facingRight);
            player1HealthBar.health = player1Life;
        }
        if(player2.BeenHit())//checks to see if player 2 was hit by an attack
        {
            //following decrements life by calling method from character script
            Debug.Log("player2 hit " + player1.facingRight);
            if (!player2.heavy)
            {
                player2Life -= player1.lightAttackPower;
            }
            else
            {
                player2Life -= player1.heavyAttackPower;
            }
            //updates healthbar as well
            player2.AllHitInfo(player1.hitForce, player1.facingRight);
            player2HealthBar.health = player2Life;
        }
        if(player1.BlockedTheAttack())//checks to see if a player has blocked an attack
        {
            Debug.Log("Player2 was blocked");
            player2.Blocked();
            player1.ResetBlock();
        }
        if(player2.BlockedTheAttack())//checks to see if a player blocked an attack
        {
            Debug.Log("Player 1 was blocked");
            player1.Blocked();
            player2.ResetBlock();
        }
        
    }
    public bool GameIsOver()//if a characters health falls below 1 then the game is over
    {
        if(player2Life<1)
        {
            winner = "Player1";
            return true;
        }
        else if(player1Life<1)
        {
            winner = "Player2";
            return true;
        }
        else
        {
            return false;
        }
    }
    public string WhoWon()//returns who won
    {
        return winner;
    }
    public void Reset()//resets the game for characters
    {
        Start();
        winner = string.Empty;

    }

}
