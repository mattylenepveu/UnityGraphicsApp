using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Public GameObject used to store the opposite teleporter as the player's destination
    public GameObject m_destination;

    // Public GameObject used for a reference to the player
    public GameObject m_player;

    // Private variable used to access the Player Controller script
    private PlayerController m_playerControl;

    // Private Vector3 used to store the offset between both teleporters
    private Vector3 m_offset;

    // Private Vector3 stored the entends of the teleporter trigger in
    private Vector3 m_extents;

    //--------------------------------------------------------------------------------
    // Function is called when the script is first ran.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Gets the Player Controller script component
        m_playerControl = m_player.GetComponent<PlayerController>();

        // Sets the offset between the two triggers positions
        m_offset = transform.position - m_destination.transform.position;

        // Defines the value for the extents Vector3
        m_extents = new Vector3(3, 0, 0);
    }

    //--------------------------------------------------------------------------------
    // Function runs when a collider enters the Teleporter trigger.
    //
    // Param:
    //      pOther: Represents the collider of the colliding object.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider pOther)
    {
        // Detects if the colliding object has a "Player" tag
        if (pOther.tag == "Player")
        {
            if (name == "Left Teleporter")
            {
                // Sets the player's position to equal the offset plus the extents 
                m_playerControl.transform.position -= m_offset + m_extents;
            }
            else
            {
                // Sets the player's position to equal the offset plus the extents 
                m_playerControl.transform.position -= m_offset - m_extents;
            }
        }
    }
}
