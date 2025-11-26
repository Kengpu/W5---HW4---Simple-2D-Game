using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI Elements")]
    public Image imageYou;
    public Image imageCom;
    public Text txtYou;
    public Text txtCom;
    public Text txtResult;
    public Text txtScoreYou;
    public Text txtScoreCom;

    [Header("Sprites for Choices")]
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorSprite;
    public Sprite questionMarkSprite;

    [Header("Buttons")]
    public Button rockButton;
    public Button paperButton;
    public Button scissorButton;

    [Header("Menu Button")]
    public Button menuButton;

    private int playerScore = 0;
    private int comScore = 0;
    private bool gameEnded = false;

    // Choices: 0 = Rock, 1 = Paper, 2 = Scissor
    private string[] choices = { "Rock", "Paper", "Scissor" };

    void Start()
    {
        // Initialize UI
        ResetImages();
        UpdateScores();
        txtResult.text = "Choose Rock, Paper or Scissor!";

        // Add button listeners
        rockButton.onClick.AddListener(() => OnPlayerChoice(0));
        paperButton.onClick.AddListener(() => OnPlayerChoice(1));
        scissorButton.onClick.AddListener(() => OnPlayerChoice(2));

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    public void OnPlayerChoice(int playerChoice)
    {
        if (gameEnded) return;

        // Computer random choice
        int comChoice = Random.Range(0, 3);

        // Update images
        UpdateChoiceImage(imageYou, playerChoice);
        UpdateChoiceImage(imageCom, comChoice);

        // Determine winner
        string result = DetermineWinner(playerChoice, comChoice);
        txtResult.text = result;

        // Update scores
        UpdateScores();

        // Check for game end
        CheckGameEnd();
    }

    private void UpdateChoiceImage(Image image, int choice)
    {
        switch (choice)
        {
            case 0: // Rock
                image.sprite = rockSprite;
                break;
            case 1: // Paper
                image.sprite = paperSprite;
                break;
            case 2: // Scissor
                image.sprite = scissorSprite;
                break;
        }
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private string DetermineWinner(int playerChoice, int comChoice)
    {
        if (playerChoice == comChoice)
        {
            return "It's a Tie!";
        }

        // Winning conditions: 0>2, 1>0, 2>1
        if ((playerChoice == 0 && comChoice == 2) || // Rock beats Scissor
            (playerChoice == 1 && comChoice == 0) || // Paper beats Rock
            (playerChoice == 2 && comChoice == 1))   // Scissor beats Paper
        {
            playerScore++;
            return "You Win! " + choices[playerChoice] + " beats " + choices[comChoice];
        }
        else
        {
            comScore++;
            return "Computer Wins! " + choices[comChoice] + " beats " + choices[playerChoice];
        }
    }

    private void UpdateScores()
    {
        txtScoreYou.text = playerScore.ToString();
        txtScoreCom.text = comScore.ToString();
    }

    private void CheckGameEnd()
    {
        if (playerScore >= 10 || comScore >= 10)
        {
            gameEnded = true;
            string winner = playerScore >= 10 ? "You" : "Computer";
            txtResult.text = "Game Over! " + winner + " wins the game!";

            // Restart game after 3 seconds
            StartCoroutine(RestartGameAfterDelay(3f));
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartGame();
    }

    public void RestartGame()
    {
        playerScore = 0;
        comScore = 0;
        gameEnded = false;

        ResetImages();
        UpdateScores();
        txtResult.text = "New Game! Choose Rock, Paper or Scissor!";
    }

    private void ResetImages()
    {
        imageYou.sprite = questionMarkSprite;
        imageCom.sprite = questionMarkSprite;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}