using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a class for the PlayerController script
public class PlayerController : MonoBehaviour 
{
    Animator m_anim;

	// Public floats used to adjust characteristics of the player
	public float m_regSpeed = 10.0f;
    public float m_maxSpeed = 15.0f;

    public float m_maxRunTime = 3.0f;

    public Slider m_timerSlider;

    // Public text used to help display the score through the UI
    public Text m_scoreText;

	// Float represents the speed the player is going
    private float m_speed;

    private float m_timer;

    private float m_moveX;

    private float m_moveZ;

    // Keeps track of how many pellets the player has collected
    private int m_pelletCount;

    private bool m_canRun;

	//--------------------------------------------------------------------------------
	// Function is called when the script is first ran.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
        m_anim = GetComponent<Animator>();

        m_timerSlider.maxValue = m_maxRunTime;

        // Sets the initial speed to be the player's regular speed
        m_speed = m_regSpeed;

        // Sets the initial value of pellet count to be zero
        m_pelletCount = 0;

        m_timer = m_maxRunTime;

        m_moveX = 0.0f;

        m_moveZ = 0.0f;

        m_canRun = true;

        // Displays the starting pellet count to the UI
        m_scoreText.text = "SCORE: " + m_pelletCount.ToString();
	}
	
	//--------------------------------------------------------------------------------
	// Function is called with a fixed framerate for smoother game handling.
	//--------------------------------------------------------------------------------
	void FixedUpdate() 
	{
        // Stores the x movement to equal the horizontal input multiplied by the speed
        m_moveX = Input.GetAxis("Horizontal") * m_speed;

        // Stores the z movement to equal the vertical input multiplied by speed
        m_moveZ = Input.GetAxis("Vertical") * m_speed;

        // Multiplies the x movement by deltatime
        m_moveX *= Time.deltaTime;

        // Multiplies the z movement by deltatime
        m_moveZ *= Time.deltaTime;

		// Translate any movement of the player on the x and z axis'
		transform.Translate(m_moveX, 0, m_moveZ);

		if (m_moveX != 0 || m_moveZ != 0)
		{
			// Creates a "new" direction Vector3 of the left control sticks direction
			Vector3 directionVector = new Vector3(m_moveX, 0, m_moveZ);

			// Makes the player look in direction of the directionVector
			transform.rotation = Quaternion.LookRotation(directionVector);
		}
    }

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
    void Update()
    {
        if (m_moveX == 0.0f && m_moveZ == 0.0f)
        {
            m_anim.SetBool("Eating", false);
        }
        else
        {
            m_anim.SetBool("Eating", true);
        }

        m_timerSlider.value = m_timer;

        // Detects if the "Left Shift" key is pressed down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (m_timer >= 0.0f && m_canRun)
            {
                // Sets the speed to equal the max or running speed
                m_speed = m_maxSpeed;

                m_timer -= Time.deltaTime;
            }
            else
            {
                m_canRun = false;
                ResetTimer();
            }
        }
        else
        {
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        // Resets speed so it equals the walking or regular speed
        m_speed = m_regSpeed;

        if (m_timer <= m_maxRunTime)
        {
            m_timer += Time.deltaTime;
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
