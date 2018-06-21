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

    private Vector3 m_rightVec;
    private Vector3 m_leftVec;
    private Vector3 m_upVec;
    private Vector3 m_downVec;

    private Vector3 m_currentLookVector;

    private Rigidbody m_rb;

    // Keeps track of how many pellets the player has collected
    private int m_pelletCount;

    private bool m_canRun;

	//--------------------------------------------------------------------------------
	// Function is called when the script is first ran.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
        m_anim = GetComponentInChildren<Animator>();

        m_rb = GetComponent<Rigidbody>();

        m_timerSlider.maxValue = m_maxRunTime;

        // Sets the initial speed to be the player's regular speed
        m_speed = m_regSpeed;

        // Sets the initial value of pellet count to be zero
        m_pelletCount = 0;

        m_timer = m_maxRunTime;

        m_canRun = true;

        m_rightVec = new Vector3(0, 90, 0);
        m_leftVec = new Vector3(0, -90, 0);
        m_upVec = new Vector3(0, 0, 0);
        m_downVec = new Vector3(0, 180, 0);

        m_currentLookVector = m_downVec;

        // Displays the starting pellet count to the UI
        m_scoreText.text = "SCORE: " + m_pelletCount.ToString();
	}
	
	//--------------------------------------------------------------------------------
	// Function is called with a fixed framerate for smoother game handling.
	//--------------------------------------------------------------------------------
	void FixedUpdate() 
	{
        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            m_rb.AddForce(gameObject.transform.forward * m_speed * Time.deltaTime, ForceMode.Acceleration);
            m_anim.SetBool("Eating", true);
        }
        else
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_currentLookVector));
            m_anim.SetBool("Eating", false);
        }
    }

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
    void Update()
    {
        m_timerSlider.value = m_timer;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_currentLookVector = m_upVec;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_currentLookVector = m_downVec;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_currentLookVector = m_leftVec;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_currentLookVector = m_rightVec;
        }

        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_currentLookVector));

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
