using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{

    public void PlayGame()

    {

        SceneManager.LoadScene("GameScene");

    }
    
    public void GoToCredit()

    {

        SceneManager.LoadScene("CreditScene");

    }
    

    public void GoToMenu()

    {

        SceneManager.LoadScene("MenuScene");

    }

    public void QuitGame()

    {

        Application.Quit();

        Debug.Log("Game Exited");

    }

}