using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a class for the PlayerController script
public class PlayerController : MonoBehaviour 
{
	// Public floats used to adjust characteristics of the player
	public float m_regSpeed = 10.0f;
    public float m_maxSpeed = 15.0f;

    // Public text used to help display the score through the UI
    public Text m_scoreText;

	// Float represents the speed the player is going
    private float m_speed;

    // Keeps track of how many pellets the player has collected
    private int m_pelletCount;

	//--------------------------------------------------------------------------------
	// Function is called when the script is first ran.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
        // Sets the initial speed to be the player's regular speed
        m_speed = m_regSpeed;

        // Sets the initial value of pellet count to be zero
        m_pelletCount = 0;

        // Displays the starting pellet count to the UI
        m_scoreText.text = "SCORE: " + m_pelletCount.ToString();
	}
	
	//--------------------------------------------------------------------------------
	// Function is called with a fixed framerate for smoother game handling.
	//--------------------------------------------------------------------------------
	void FixedUpdate() 
	{
		// Stores the x movement to equal the horizontal input multiplied by the speed
		float moveX = Input.GetAxis("Horizontal") * m_speed;

		// Stores the z movement to equal the vertical input multiplied by speed
		float moveZ = Input.GetAxis("Vertical") * m_speed;

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
            m_speed = m_maxSpeed;
        }

		// Detects if the "Left Shift" key has been lifted
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Resets speed so it equals the walking or regular speed
            m_speed = m_regSpeed;
        }
    }

    //--------------------------------------------------------------------------------
    // Adds one to the pellet count when called.
    //--------------------------------------------------------------------------------
    public void AddOneToCount()
    {
        // Adds one to whatever value the Pellet Count is
        m_pelletCount += 1;

        // Displays the updated pellet count to the UI
        m_scoreText.text = "SCORE: " + m_pelletCount.ToString();
    }

    //--------------------------------------------------------------------------------
    // Allows other classes to access the pellet count when called.
    //
    // Return:
    //      Returns the pellet count as an int.
    //--------------------------------------------------------------------------------
    public int GetPelletCount()
    {
        return m_pelletCount;
    }
}
