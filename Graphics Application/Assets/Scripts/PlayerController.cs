using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Creates a class for the PlayerController script
public class PlayerController : MonoBehaviour 
{
    // Animator allows script to get the Player's animator component
    Animator m_anim;

	// Public floats used to adjust characteristics of the player
	public float m_regSpeed = 10.0f;
    public float m_maxSpeed = 15.0f;
    public float m_maxRunTime = 3.0f;

    // Public Slider used to get the Game UI's slider
    public Slider m_timerSlider;

    // Public text used to help display the score through the UI
    public Text m_scoreText;

    // Private float used to store the Player's current speed
    private float m_speed;

    // Private Vec3s used to define the Vectors for all 4 directions
    private Vector3 m_rightVec;
    private Vector3 m_leftVec;
    private Vector3 m_upVec;
    private Vector3 m_downVec;

    // Private Vec3 used to store the current vector that the Player is facing
    private Vector3 m_currentLookVector;

    // Private float used to store the time left that the player can run
    private float m_timer;

    // Private Rigidbody used to access the player's rigidbody
    private Rigidbody m_rb;

    // Private int keeps track of how many pellets the player has collected
    private int m_pelletCount;

    // Private bool verifies if the player is allowed to run or not
    private bool m_canRun;

	//--------------------------------------------------------------------------------
	// Function is called when the script is first ran.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
        // Gets the animator component from the child of the Player and stores it
        m_anim = GetComponentInChildren<Animator>();

        // Gets the Rigidbody component from the Player and stores it
        m_rb = GetComponent<Rigidbody>();

        // Sets the Max Value for the slider to equal the maximum run time float
        m_timerSlider.maxValue = m_maxRunTime;

        // Sets the initial speed to be the player's regular speed
        m_speed = m_regSpeed;

        // Sets the initial value of pellet count to be zero
        m_pelletCount = 0;

        // Sets the timer to equal the maximum value a player can run
        m_timer = m_maxRunTime;

        // Sets Can Run bool to true so the player can run at the start
        m_canRun = true;

        // Initialises the Vectors for Up, Down, Left and Right 
        m_rightVec = new Vector3(0, 90, 0);
        m_leftVec = new Vector3(0, -90, 0);
        m_upVec = new Vector3(0, 0, 0);
        m_downVec = new Vector3(0, 180, 0);

        // Sets the look direction for the player to equal the down direction
        m_currentLookVector = m_downVec;

        // Displays the starting pellet count to the UI
        m_scoreText.text = "SCORE: " + m_pelletCount.ToString();
	}
	
	//--------------------------------------------------------------------------------
	// Function is called with a fixed framerate for smoother game handling.
	//--------------------------------------------------------------------------------
	void FixedUpdate() 
	{
        // Checks to see if either up, down, left or right is pressed
        if (Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            // Adds force to rigidbody so that the player goes forward
            m_rb.AddForce(gameObject.transform.forward * m_speed * Time.deltaTime, 
                          ForceMode.Acceleration);

            // Sets the "Eating" bool to true so the animator plays the Eating animation
            m_anim.SetBool("Eating", true);
        }

        // Else if there is no direction key being pressed
        else
        {
            // Sets rotation direction to equal the current looking direction
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_currentLookVector));

            // Sets the "Eating" bool to false so the animator plays the Idle animation
            m_anim.SetBool("Eating", false);
        }
    }

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
    void Update()
    {
        // Sets the value on the slider to be equal to the timer value every frame
        m_timerSlider.value = m_timer;

        // Makes the player look up if the Up key is pressed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_currentLookVector = m_upVec;
        }

        // Makes the player look down if the down key is pressed
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_currentLookVector = m_downVec;
        }

        // Makes the player look left if the left key is pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_currentLookVector = m_leftVec;
        }

        // Makes the player look right if the right key is pressed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_currentLookVector = m_rightVec;
        }

        // Sets the rotation so the player looks in the current looking direction
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_currentLookVector));

        // Detects if the "Left Shift" key is pressed down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Checks if the timer is greater than zero and if the player can run
            if (m_timer >= 0.0f && m_canRun)
            {
                // Sets the speed to equal the maximum running speed
                m_speed = m_maxSpeed;

                // Decreases the timer down by deltaTime
                m_timer -= Time.deltaTime;
            }

            // Else if the timer equals zero and the player can't run
            else
            {
                // Can Run bool is set to false to disallow player to run
                m_canRun = false;

                // Reset Timer function is called
                ResetTimer();
            }
        }

        // Else if "Left Shift" is not pressed down
        else
        {
            // Reset Timer function is called
            ResetTimer();
        }

        // End function is called every frame
        End();
    }

    //--------------------------------------------------------------------------------
    // Resets the timer back to the maximum running time.
    //--------------------------------------------------------------------------------
    private void ResetTimer()
    {
        // Resets speed so it equals the walking or regular speed
        m_speed = m_regSpeed;

        // Checks if the timer is less than the maximum running time
        if (m_timer < m_maxRunTime)
        {
            // Increases timer based on deltaTime
            m_timer += Time.deltaTime;
        }

        // Else if the timer equals the maximum run time
        else
        {
            // Can Run bool is set to true
            m_canRun = true;
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
    // Loads the End Game scene when the Pellet Count equals 36.
    //--------------------------------------------------------------------------------
    private void End()
    {
        if (m_pelletCount == 36)
        {
            SceneManager.LoadScene(1);
        }
    }

    //--------------------------------------------------------------------------------
    // Makes the Player look right if the Right button is pressed.
    //--------------------------------------------------------------------------------
    public void RightArrow()
    {
        m_currentLookVector = m_rightVec;
    }

    //--------------------------------------------------------------------------------
    // Makes the Player look left if the Left button is pressed.
    //--------------------------------------------------------------------------------
    public void LeftArrow()
    {
        m_currentLookVector = m_leftVec;
    }

    //--------------------------------------------------------------------------------
    // Makes the Player look up if the Up button is pressed.
    //--------------------------------------------------------------------------------
    public void UpArrow()
    {
        m_currentLookVector = m_upVec;
    }

    //--------------------------------------------------------------------------------
    // Makes the Player look down if the Down button is pressed.
    //--------------------------------------------------------------------------------
    public void DownArrow()
    {
        m_currentLookVector = m_downVec;
    }
}
