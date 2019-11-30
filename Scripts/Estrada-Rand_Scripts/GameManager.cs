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

public class GameManager : MonoBehaviour
{
    //variables used to reference character manager and players individually and their health bars
    public Character player1;
    public Character player2;
    public CharacterManager charManager;
    private int player1Life;
    private int player2Life;
    public Text gameWinner;
    public GameObject resetButton;
    public HealthBarScript healthBar1;
    public HealthBarScript healthBar2;
    public DeathFloor floor;



    // Update is called once per frame
    void Update()
    {
        CheckFloor();//checks the death floor for collisions
        CheckGame();//checks if the game is won or not
    }
    void CheckGame()//checks to see if the game has been won based on the character manager
    {
        if (charManager.GameIsOver())
        {
            if(charManager.WhoWon().Equals("Player1"))
            {
                player2.deathPlay();
                player1.HasWon();
                DisplayWinnerText("Player1");
                player1.gameObject.SetActive(false);
            }
            else if(charManager.WhoWon().Equals("Player2"))
            {
                player1.deathPlay();
                player2.HasWon();
                DisplayWinnerText("Player2");
                player2.gameObject.SetActive(false);
            }
        }
        
    }
    void DisplayWinnerText(string winner)//displays who won
    {
        gameWinner.text = winner + " Wins!";
        gameWinner.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);
    }

    void CheckFloor()//checks to see if the floor was touched and thus who won the game
    {
        if (floor.beenTouched())
        {
            string deathHit = floor.WhoHit();
            if(deathHit == "Player1")
            {
                player1.deathPlay();
                player2.HasWon();
                player2.gameObject.SetActive(false);
                DisplayWinnerText("Player2");
            }
            if(deathHit == "Player2")
            {
                player2.deathPlay();
                player1.HasWon();
                DisplayWinnerText("Player1");
                player1.gameObject.SetActive(false);
            }
        }
    }
}
