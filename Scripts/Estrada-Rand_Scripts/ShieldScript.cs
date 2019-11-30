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

//used to detect hits to the shield to stun the attacker
public class ShieldScript : MonoBehaviour
{
    public bool blockedAttack;//stores if an attack was blocked
    // Start is called before the first frame update
    void Start()
    {
        blockedAttack = false;
    }

        //if an attack colides with the shield's collider then it checks to see what kind of attack it was
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LightAttack"))
        {
            blockedAttack = true;
        }        
        if(other.CompareTag("HeavyAttack"))
        {
            blockedAttack = true;
        }
    }
    //called by character manager to see if an attack was blocked
    public bool hasBeenBlocked()
    {
        if(blockedAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
