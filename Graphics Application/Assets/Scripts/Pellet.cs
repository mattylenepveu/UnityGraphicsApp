using UnityEngine;

// Creates a class for the Pellet script
public class Pellet : MonoBehaviour
{
    // Public GameObject used for a reference to the player
    public GameObject m_player;

    // Private variable used to access the Player Controller script
    private PlayerController m_playerControl;

    //--------------------------------------------------------------------------------
    // Function is called when the script is first ran.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Gets the Player Controller script component
        m_playerControl = m_player.GetComponent<PlayerController>();
    }

    //--------------------------------------------------------------------------------
    // Function runs when a collider enters the Pellet trigger.
    //
    // Param:
    //      pOther: Represents the collider of the colliding object.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider pOther)
    {
        // Checks if the collider of the object has a "Player" tag
        if (pOther.tag == "Player")
        {
            // Calls the Add One to Count function from the player script
            m_playerControl.AddOneToCount();

            // Destroy's the individual pellet object
            Destroy(gameObject);
        }
    }
}
