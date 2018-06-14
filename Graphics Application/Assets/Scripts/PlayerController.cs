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
	public float jumpForce = 5.0f;
    public float launchThreshold = 15000.0f;
	public float minLaunchForce = 5000.0f;
	public float crouchHeight = 0.5f;

	// CapsuleCollider used to store the player's capsule collider into
	public CapsuleCollider col;

	// LayerMask used to store the ground layer into for jumping calculations
	public LayerMask groundLayers;

	// Float represents the speed the player is going
    private float speed;

	// Float indicates the launch force for when a player throws a box
    private float launchForce;

	// Float indicates the height of the player when they're standing
	private float standHeight;

	// Represents the difference between the player's stand and crouch height
	private float yOffset;

	// Bool indicates if the player has a box or not
    private bool hasBox;

	// Bool represents whether the player is crouching or not
    private bool crouching;
    
	// Indicates the rigidbody of the player 
	private Rigidbody rb;

	// Where a box will be stored if the player picks it up 
	private GameObject pickUp = null;

	//--------------------------------------------------------------------------------
	// Function is called when the script is run first.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
		// Sets the initial speed to be the player's regular speed
        speed = regSpeed;

		// Sets the launch force to equal the minimum launch force
		launchForce = minLaunchForce;

		// Bool hasBox is initially set to false
        hasBox = false;

		// Sets the computer's cursor to be invisible when script first runs
		Cursor.lockState = CursorLockMode.Locked;

		// Stores the player's rigidbody in the Rigidbody variable
		rb = GetComponent<Rigidbody>();

		// Stores the player's capsule collider inbto variable
		col = GetComponent<CapsuleCollider>();

		// Sets the standing height to equal the player's intial local y scale
        standHeight = transform.localScale.y;

		// Crouching bool is intialised to false
        crouching = false;

		// Sets the y offset to equal the standing height minus the crouching height
        yOffset = standHeight - crouchHeight;
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

		// Detects if "Space" key has been pressed down
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			// Checks if the player is on the ground
			if (IsGrounded()) 
			{
				// Adds an jump force to the player's Rigidbody
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		// Detects if the left mouse button has been pressed down
        if (Input.GetMouseButtonDown(leftMouse)) 
		{
			// Sets the hasBox bool back to false before further checks
            hasBox = false;

			// Checks if the player has not picked up anything
            if (pickUp == null) 
			{
				// Checks if the player is not already crouching
                if (!crouching)
                {
					// Bit shifts the pick up layer by one and stores it into local int
                    int layerMask = 1 << LayerMask.NameToLayer("Pick Up");

					// Sets a box size to detect if there is anything in it to pick up
                    Vector3 boxSize = new Vector3(2, 2, 2);

					// Collider array used to detect if there are one or more boxes to pick up
                    Collider[] boxPickUp = Physics.OverlapBox(transform.position + transform.forward,
                                               boxSize * 0.5f, transform.rotation, layerMask);

					// Checks if the collider array has found any boxes to pick up
                    if (boxPickUp.Length > 0)
                    {
						// Sets the first found box in collider array into the pick up variable
                        pickUp = boxPickUp[0].gameObject;

						// Sets the box to be a child of the player
                        pickUp.transform.parent = transform;

						// Gets the boxes' rigidbody
                        Rigidbody boxRb = pickUp.GetComponent<Rigidbody>();

						// Sets the box to be kinematic
                        boxRb.isKinematic = true;

						// Sets an offset position to be forward and slightly above the player's view
                        pickUp.transform.localPosition = new Vector3(0, 1.5f, 1);

						// Resets the boxes' rotation
                        pickUp.transform.localRotation = Quaternion.identity;
                    }
                }
			} 

			// Else if the player has a box already
			else 
			{
				// Adds 1000 to the launch force to prepare trhe box to be launched
                launchForce += 1000.0f;

				// Sets the hasBox bool to be true
                hasBox = true;

				// Sets the launchForce to be no higher than the set threshold
                if (launchForce > launchThreshold)
                {
                    launchForce = launchThreshold;
                }
			}
		}

		// Detects if the left mouse button has been lifted
        if (Input.GetMouseButtonUp(leftMouse))
        {
			// Checks if the player has a box picked up
            if (hasBox)
            {
				// Sets the box to no longer have a parent
                pickUp.transform.parent = null;

				// Gets the boxes' rigidbody
                Rigidbody boxRb = pickUp.GetComponent<Rigidbody>();

				// Sets the box to no longer be kinematic
                boxRb.isKinematic = false;

				// Adds the launch force to the boxes' rigidbody
                boxRb.AddForce(transform.forward * launchForce);

				// Resets the launch force to equal the minimum
				launchForce = minLaunchForce;

				// Resets pick up to equal null
                pickUp = null;
            }
        }
    }

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
    void Update()
    {
		// Detects if the "Left Control" key has been pressed down
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
			// Checks if the player is already crouching
            if (!crouching)
            {
				// Sets the y scale to equal the crouching height
                Vector3 scale = new Vector3(transform.localScale.x, crouchHeight, 
											transform.localScale.z);

				// Sets the new scale to equal the local scale of the player
                transform.localScale = scale;

				// Sets the crouching bool to be true
                crouching = true;

				// Sets the transform position minus the up Vector3 multiuplied by the y offset
                transform.position -= Vector3.up * yOffset;
            }

			// Else if the player is crouching
            else
            {
				// Defines the up direction to check for anything above the player
                Vector3 up = transform.TransformDirection(Vector3.up);

				// Raycasts upwards to see if there are no objects above the player when crouching
                if (!Physics.Raycast(transform.position, up))
                {
					// Sets the y scale to equal the standing height 
                    Vector3 scale = new Vector3(transform.localScale.x, standHeight, transform.localScale.z);

					// Sets the standing scale to equal the local scale of the player
                    transform.localScale = scale;

					// Resets the crouching bool back to false
                    crouching = false;

					// Sets the transform position plus the up Vector3 multiuplied by the y offset
                    transform.position += Vector3.up * yOffset;
                }
            }
        }

		// Detects if the "Left Shift" key is pressed down and the player is on ground
        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
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

		// Detects if "Escape" key has been pressed down
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			// Makes the computer's cursor visible again
			Cursor.lockState = CursorLockMode.None;
		}
    }

	//--------------------------------------------------------------------------------
	// Function checks if the player is on the ground
	//
	// Return:
	// 		Returns a bool determining if the player is on the ground or not
	//--------------------------------------------------------------------------------
    private bool IsGrounded()
	{
		// Grounding checked using a downward raycast
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
	}
}
