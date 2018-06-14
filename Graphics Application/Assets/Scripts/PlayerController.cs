using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

// Creates a class for the PlayerController script
public class PlayerController : MonoBehaviour 
{
	// Public floats used to adjust characteristics of the player
	public float regSpeed = 10.0f;
    public float maxSpeed = 15.0f;

	// Float represents the speed the player is going
    private float speed;

	//--------------------------------------------------------------------------------
	// Function is called when the script is run first.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
		// Sets the initial speed to be the player's regular speed
        speed = regSpeed;
	}
	
	//--------------------------------------------------------------------------------
	// Function is called with a fixed framerate for smoother game handling.
	//--------------------------------------------------------------------------------
	void FixedUpdate() 
	{
		// Stores the left mouse input into local int to improve readability
        int leftMouse = 0;

		// Stores the x movement to equal the horizontal input multiplied by the speed
		float moveX = Input.GetAxis("Horizontal") * speed;

		// Stores the z movement to equal the vertical input multiplied by speed
		float moveZ = Input.GetAxis("Vertical") * speed;

		// Multiplies the x movement by deltatime
		moveX *= Time.deltaTime;

		// Multiplies the z movement by deltatime
		moveZ *= Time.deltaTime;

		// Translate any movement of the player on the x and z axis'
		transform.Translate(moveX, 0, moveZ);
    }

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
    void Update()
    {
		// Detects if the "Left Shift" key is pressed down
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			// Sets the speed to equal the max or running speed
            speed = maxSpeed;
        }

		// Detects if the "Left Shift" key has been lifted
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
			// Resets speed so it equals the walking or regular speed
            speed = regSpeed;
        }
    }
}
