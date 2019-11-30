/*
Name: Noah Estrada-Rand
Student ID#: 2272490
Chapman email: estra146@mail.chapman.edu
Course Number and Section: CPSC 236-02
Assignment: Final Project: Pandamonium
*/

//namespaces necessary to work with unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;//how fast the characters can be moved
    public float jumpForce;//how high they can jump
    private float originalJumpForce;
    public float moveInput;//numerical value for where the character is going
    public bool facingRight = true;//if the character is facing right or not
    public float gravity; //what value is gravity
    float xPos;//x position variable
    float yPos;//y position variable
    float facing = 180; //variable to help change which way the character is facing
    public bool isGrounded; //boolean to tell if the character is touching the ground
    public GameObject shield;  //variable storing the shield gameobject
    public ShieldScript blockAttackDetector; // collider for the shield to detect hits
    Vector3 playerPos = Vector3.zero; // player position
    private Animator anim; // animator
    public bool shielded; // tells whether character is shielded or not
    private Rigidbody rb;//accesses the rigidbody
    private bool hit; // tells whether the character was hit or not
    public int health; // health
    public int lightAttackPower;//how much damage is done by a light attack
    public int heavyAttackPower; // how much damage is done by a heavy attack
    private Vector3 enemyAttack; // gets direction of other player
    private bool usable; //reflects if the player is allowed to move and act or not
    public int hitForce; //blowback force
    public GameObject pow; // graphic gameobject when character is hit
    public GameObject powLeft; // same as above but for other side of character
    public GameObject lightAttack; // light attack collider
    public GameObject lightAttackEffect; // light attack effect
    public GameObject fullCharacter; // character prefab
    public GameObject deathGraphic; // what graphic to play upon character death
    public GameObject heavyAttack; // heavy attack collider
    public GameObject heavyAttackEffect;//particle effect for heavy attack
    private bool lightAttacking; // whether the character is attacking with a light attack
    public bool heavy; //if the hit suffered by a character was a heavy attack
    private bool attacking; // if the character is light attacking or not
    public int playerNum; // what player number is the character 1 or 2
    public KeyCode upInput;//used for debugging 
    public KeyCode downInput; // used for debugging
    public KeyCode leftInput; // used for debugging
    public KeyCode rightInput; // used for debuggin
    public KeyCode attackInput; // used for debugging
    public KeyCode shieldInput; //used for debugging
    public bool birdStatus; //if the character is a bird
    public bool pandaStatus;//if the character is a panda
    public bool foxStatus;//if the character is a fox
    public bool lizardStatus;//if the character is a lizard
    public GameObject exclaim; //graphic to play when the character is stunned 
    private CharacterController controller; //used for debugging as well
    private bool attackInProgress; // if an attack is in progress
    private bool heavyAttacking; // if a heavy attack is in progress




    // Start is called before the first frame update and ensures that everything is set to normal
    void Start()
    {
        rb = GetComponent<Rigidbody>();//gets rigidbody of assigned component
        anim = GetComponent<Animator>();
        usable = true;
        heavy = false;
        lightAttack.SetActive(false);
        heavyAttack.SetActive(false);
        if(birdStatus)
        {
            originalJumpForce = jumpForce;//sets the baseline value for the bird's initial jump force
        }
    
    }
    
    void Flip()//changes the characters orientation based on directional input
    {
        facingRight = !facingRight;//changes value of the bool
        facing *= (-1);
        transform.Rotate(0, facing, 0);//changes the rotation of the sprite
    }
    private void FixedUpdate()//like update but occurs at a certain interval, here used to check which way character is facing
    {
        if (facingRight == false && moveInput > 0) //tells if the player is facing leftt and moving
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)//if player is facing right and not moving
        {
            Flip();
        }
    }
    void Update()//calls methods responsible for checking user input
    {
        if (usable)
        {
            if(!heavyAttacking)
            {
                Attack();
                HeavyAttack();
                if (!attacking)//if character is attacking they are not allowed to shield or move
                {
                    Shield();
                    Movement();
                }
            }
           
        }

    }
    private void Attack()//checks for joystick input for attacks (x button)
    {
        //these statements check to see if the character is a bird and if the down input on the joystick is pushed when attacking
        if(Input.GetButtonDown("Attack" + playerNum)&&!birdStatus&&Input.GetAxisRaw("Vertical"+playerNum) !=1)
        {
            lightAttackEffect.SetActive(true);//activates particle effect
            lightAttacking = true;//indicates that the character is attacking
            lightAttack.SetActive(true);//activates the attacking collider
            Invoke("UnAttack", .4f);
        }
        //runs if the character is a bird
        if (Input.GetButtonDown("Attack" + playerNum) && birdStatus && Input.GetAxisRaw("Vertical" + playerNum) != 1)
        {
            lightAttackEffect.SetActive(true);
            lightAttacking = true;
            lightAttack.SetActive(true);
            Invoke("UnAttack", .2f);
        }

    }
    private void Attack2()//used to modularize heavy attacks
    {      
        if(!birdStatus)//all non bird characters follow this precedent
        {
            heavyAttack.SetActive(true);
            Invoke("UnAttack", 1.3f);
        }
        if(birdStatus)//bird is propelled forward in its heavy attack
        {
            heavyAttack.SetActive(true);
            if (facingRight)
            {
                rb.velocity = new Vector3(20, 3, 0);
            }
            else
            {
                rb.velocity = new Vector3(-20, 3, 0);
            }
            Invoke("UnAttack", 1.3f);//disables attack hitbox even if user continues holding attack down
        }

    }
    private void HeavyAttack()//scans for input for a heavy attack (down and attack)
    {
        if(!lightAttacking)
        {
            if (Input.GetButton("Attack" + playerNum) && Input.GetAxisRaw("Vertical" + playerNum) == 1)
            {
                anim.SetTrigger("Idle");//makes character appear still when attacking
                heavyAttacking = true;
                if(!birdStatus)//all non birds follow this precedent
                {
                    attacking = true;
                    heavyAttackEffect.SetActive(true);
                    Invoke("Attack2", .7f);//delay for attack as heavy attacks charge up
                }
                if(birdStatus)//same as above but for the bird
                {
                    attacking = true;
                    heavyAttackEffect.SetActive(true);
                    Invoke("Attack2", 1f);
                }
                
            }
        }
       
    }
    private void UnAttack()//resets all values concerning attacking to original state
    {
        lightAttackEffect.SetActive(false);
        lightAttack.SetActive(false);
        lightAttacking = false;
        heavyAttack.gameObject.SetActive(false);
        heavyAttackEffect.SetActive(false);
        attacking = false;
        heavyAttacking = false;
    }
    private void Shield()//scans for if user shields
    {
        if (Input.GetButton("Shield"+playerNum))
        {
            shield.gameObject.SetActive(true);//activates the shield
            shielded = true;//indicates the user is shielded
            usable = false;
            Invoke("UnShield", .5f);
        }
   
    }
    private void UnShield()//turns off shield and delays user from moving to punish a failed shield
    {
        shield.gameObject.SetActive(false);
        shielded = false;
        exclaim.SetActive(true);
        Invoke("MakeUsable", .85f);//makes character usable again after a limited amount of time
    }
    private void MakeUsable()//allows character to move again
    {
        exclaim.SetActive(false);
        usable = true;
    }

    private void Movement()//scans for player movement based on custom axes
    {
     
        if(birdStatus)//bird flies in place 
        {
            anim.Play("Fly Inplace");
        }
        if (!shielded)
        {
            if (Input.GetButtonDown("Jump"+playerNum))//when the user hits A they jump 
            {
                if (birdStatus)
                {
                    anim.Play("Fly Inplace");
                    if (isGrounded)
                    {
                        jumpForce = originalJumpForce + 3;//extra jump force on first jump
                        Fly();//calls the appropriate fly mechanics for bird
                    }
                    else//need to make it so it only decrements once
                    {
                        jumpForce = originalJumpForce;
                        Fly();
                    }
                }
                else
                {
                    if(isGrounded)//jump mechanics for all other characters
                    {
                        rb.velocity = Vector3.up * jumpForce;//adds velocity to the object upward
                        isGrounded = false;
                    }                                        
                }
            }
            if (Input.GetAxisRaw("Horizontal" + playerNum) ==1)//if the user hits right they go right and the run animation plays
            {
                moveInput = 1f;
                if (!birdStatus)//bird has no run animation
                {
                    anim.Play("Run");
                }
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNum)==-1)//if user hits left thenh they run and turn left
            {
                moveInput = -1f;
                if(!birdStatus)//bird has no run animation
                {
                    anim.Play("Run");
                }
            }
            else
            {
                moveInput = 0;
                if (!birdStatus)//bird is always flying
                {
                    anim.SetTrigger("Idle");
                }
            }
            yPos = transform.position.y + (Input.GetAxis("Vertical" +playerNum) * gravity);//constant gravity applied to panda
            xPos = transform.position.x + (moveInput * speed);//moves player horizontally
            playerPos = new Vector3(xPos, yPos);//updates the players position
            transform.position = playerPos;//transforms user position
        }
                
    }
    private void Fly()//activates appropriate flying mechanis for bird
    {
        anim.Play("Fly Inplace");
        rb.velocity = Vector3.up * jumpForce;//adds velocity to the item upward
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision col)//detects if the character is touching the ground
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }
        
    }
    public void OnTriggerEnter(Collider other)//detects if the user is attacked and what kind of attack it is
    {
        if (other.gameObject.CompareTag("LightAttack"))
        {
            if (!shielded)
            {
                hit = true;
            }
        }
        else if (other.gameObject.CompareTag("HeavyAttack"))
        {
            if (!shielded)
            {
                hit = true;
                heavy = true;
            }
        }
    }
    public bool BeenHit()//is called by character manager to determine if a character was hit or not
    {
        if (hit)
        {
            return true;
        }
        return false;
    }
    //method to find and determine how much damage to sustain based on enemy's attack and pushback power
    public void AllHitInfo(int enemyHitForce, bool facingRight)
    {
        if (heavy)
        {
            if (facingRight)
            {
                rb.velocity = new Vector3(enemyHitForce, enemyHitForce/3, 0);//pushes character in a direction right
                hit = false;
                Stun(enemyHitForce);//user cannot move momentarily when hit
                heavy = false;
            }
            else if (!facingRight)
            {
                rb.velocity = new Vector3(-enemyHitForce, enemyHitForce/3, 0);//pushes character left
                hit = false;
                Stun(enemyHitForce);
                heavy = false;
            }
        }
        else
        {
            if (facingRight)
            {
                rb.velocity = Vector3.right * (enemyHitForce / 2);//same as above
                hit = false;
                Stun(enemyHitForce);
            }
            else if (!facingRight)
            {
                rb.velocity = Vector3.left * (enemyHitForce / 2);//sanme as above
                hit = false;
                Stun(enemyHitForce);
            }
        }

    }

    void Stun(int enemyHitForce)//prvents character from moving when called and activates damage graphic
    {
        usable = false;
        if (facingRight)
        {
            pow.SetActive(true);
        }
        else
        {
            powLeft.SetActive(true);
        }
        if (enemyHitForce > 10)
        {
            Invoke("UnStun", .5f);//delay for unstun longer when user hit by a large hit force
        }
        else
        {
            Invoke("UnStun", .2f);
        }
    }
    void UnStun()//allows user to move again
    {
        usable = true;
        pow.SetActive(false);
        powLeft.SetActive(false);
        exclaim.SetActive(false);
    }
    public bool BlockedTheAttack()//used by character manager to see if the character blocked an attack
    {
        if(blockAttackDetector.hasBeenBlocked())
        {
            return true;
        }
        else
        {
            return false;
        }        
    }
    public void Blocked()//is called if the user is blocked and stuns them
    {
        usable = false;
        lightAttack.SetActive(false);
        heavyAttack.SetActive(false);
        exclaim.SetActive(true);
        anim.SetTrigger("Idle");
        Invoke("UnStun", 1.3f);
    }
    public void Death()//desetroys the character if they lose
    {
        Destroy(fullCharacter);
    }
    public void deathPlay()//plays the death graphic when lost
    {
        usable = false;
        fullCharacter.SetActive(false);
        deathGraphic.gameObject.SetActive(true);
    }
    public void Reset()//resets the character when called
    {
        usable = true;
        fullCharacter.SetActive(true);
        deathGraphic.SetActive(false);
    }
    public void HasWon()//is called when character wins
    {
        usable = false;
    }
    public void ResetBlock()//resets the collider for the shield
    {
        blockAttackDetector.blockedAttack = false;
    }
}

