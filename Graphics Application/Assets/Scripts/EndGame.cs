using UnityEngine;
using UnityEngine.SceneManagement;

// Creates a class for the EndGame script
public class EndGame : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    // Function is called when the script is first ran.
    //--------------------------------------------------------------------------------
    void Awake() {}

    //--------------------------------------------------------------------------------
    // Function is called once every frame (Not Being Used).
    //--------------------------------------------------------------------------------
    void Update() {}

    //--------------------------------------------------------------------------------
    // Public function loads the game scene when the "Restart" button is clicked.
    //--------------------------------------------------------------------------------
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
