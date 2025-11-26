using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        // Debug log to verify script is running
        Debug.Log("MainMenuController Started");

        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
            Debug.Log("Start button listener added");
        }
        else
        {
            Debug.LogError("Start button is not assigned!");
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame method called!");

        // Try different methods to see which works:

        // Method 1: Load by build index (most reliable)
        SceneManager.LoadScene(1);

        // Method 2: If above doesn't work, try this:
        // SceneManager.LoadScene("SampleScene");

        // Method 3: Or try this with your exact scene name:
        // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}