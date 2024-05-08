using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneName = "Level1"; 

    void Update()
    {
        if (Input.anyKeyDown) // Check if any key is pressed
        {
            LoadGameLevel();
        }
    }

    void LoadGameLevel()
    {
        SceneManager.LoadScene(sceneName); // Load the game scene
    }
}
