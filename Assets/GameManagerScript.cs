using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Method to be called when Start Button is clicked
    public void StartGame()
    {
        // Load the SampleScene (RPS game scene)
        SceneManager.LoadScene("SampleScene");
    }

    // Method to be called when Quit Button is clicked
    public void QuitGame()
    {
        // If running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Optional: Method to return to main menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}