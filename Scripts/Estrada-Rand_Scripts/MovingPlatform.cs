/*
Name: Robby Jones
Student ID#: 2295678
Chapman email: robejones@chapman.edu
Course Number and Section: CPSC 236-02
Assignment: Final Project: Pandamonium
*/


using System.Collections;//these are used for unity functionality
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //stores starting and end positions and the speed at which it moves
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;//stores overall location
 

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;//works to transform the position
    }

    // Update is called once per frame
    void Update()//used to transform the position of the platform based on a stop and start point
    {
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()//draws line for the platform to follow
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
