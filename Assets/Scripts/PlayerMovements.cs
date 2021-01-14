using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{

    public float speed;                //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float moveHorizontal, moveVertical;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        if (GetComponent<PlayerCombat>().dead)
        {
            return;
        }

        //Store the current horizontal input in the float moveHorizontal.
        moveHorizontal = Input.GetAxis("Horizontal");
        //Store the current vertical input in the float moveVertical.
        moveVertical = Input.GetAxis("Vertical");

        if(moveHorizontal > 0f) this.GetComponent<Animator>().SetFloat("horizontalMovement", 1f);
        if(moveHorizontal < 0f) this.GetComponent<Animator>().SetFloat("horizontalMovement", -1f);
        if(moveHorizontal == 0f) this.GetComponent<Animator>().SetFloat("horizontalMovement", 0f);

        if (moveVertical > 0f) this.GetComponent<Animator>().SetFloat("verticalMovement", 1f);
        if (moveVertical < 0f) this.GetComponent<Animator>().SetFloat("verticalMovement", -1f);
        if (moveVertical == 0f) this.GetComponent<Animator>().SetFloat("verticalMovement", 0f);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}