using UnityEngine;
using UnityEngine.AI;

// Script requires a NavMesh in order to compile
[RequireComponent(typeof(NavMesh))]

// Creates a class for the AI script
public class AI : MonoBehaviour
{
    // Private Vec3s used to define the Vectors for all 4 directions
    private Vector3 m_rightVec;
    private Vector3 m_leftVec;
    private Vector3 m_upVec;
    private Vector3 m_downVec;

    //--------------------------------------------------------------------------------
    // Function is called when the script is first ran.
    //--------------------------------------------------------------------------------
    void Awake()
    {
        // Initialises the Vectors for Up, Down, Left and Right
        m_rightVec = new Vector3(0, 90, 0);
        m_leftVec = new Vector3(0, -90, 0);
        m_upVec = new Vector3(0, 0, 0);
        m_downVec = new Vector3(0, 180, 0);
    }

    //--------------------------------------------------------------------------------
    // Function is called once every frame (Not Being Used).
    //--------------------------------------------------------------------------------
    void Update() {}

    //--------------------------------------------------------------------------------
    // Function is called when the AI collides with a trigger.
    //
    // Param:
    //      other: Represents the collider of the object the AI collides with.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collided object's name is "Destination 1"
        if (other.name == "Destination 1")
        {
            // Makes the AI look and run down
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_downVec));
        }

        // Checks if the collided object's name is "Destination 2"
        if (other.name == "Destination 2")
        {
            // Makes the AI look and run left
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_leftVec));
        }

        // Checks if the collided object's name is "Destination 3"
        if (other.name == "Destination 3")
        {
            // Makes the AI look and run up
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_upVec));
        }

        // Checks if the collided object's name is "Destination 4"
        if (other.name == "Destination 4")
        {
            // Makes the AI look and run right
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(m_rightVec));
        }
    }
}
