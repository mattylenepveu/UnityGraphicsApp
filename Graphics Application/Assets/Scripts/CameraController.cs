using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a class for the CameraController script
public class CameraController : MonoBehaviour 
{
	// Public floats used for adjusting the camera's movement
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	// GameObject used to store the player in
	private GameObject character;

	// Vector2 used to store where the ccamera is pointing
	private Vector2 mouseLook;

	// Vector2 used to store the smoother vector of the camera's movement
	private Vector2 smoothV;

	//--------------------------------------------------------------------------------
	// Function is called when the script is run first.
	//--------------------------------------------------------------------------------
	void Awake() 
	{
		// Makes the Player a parent of the camera to allow first person movement
		character = transform.parent.gameObject;
    }
	
	//--------------------------------------------------------------------------------
	// Function is called with a fixed framerate for smoother game handling.
	//--------------------------------------------------------------------------------
	void FixedUpdate() 
	{
		// Gets the mouse's position and stores in a local variable
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		// Stores the scale between the mouse's position and the sensitivity and smoothing
		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

		// Lerps the x value of smooth vector's x between that and the mouse pos' x
		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1.0f / smoothing);

		// Lerps the y value of smooth vector's y between that and the mouse pos' y
		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1.0f / smoothing);

		// Adds the smooth vector's values to the mouselook vector
		mouseLook += smoothV;

		// Clamps the y of the mouselook vector to avoid 360 rotation on the x axis
		mouseLook.y = Mathf.Clamp (mouseLook.y, -60.0f, 60.0f);

		// Updates the camera's local rotation
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

		// Updates the player's local rotation to match the camera's x rotation
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

		// Gets the rigidbody component of the player
        Rigidbody rb = character.GetComponent<Rigidbody>();

		// Freezes all rotation of the player
        rb.freezeRotation = true;
	}
}
