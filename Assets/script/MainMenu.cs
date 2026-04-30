using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{

    public void PlayGame()

    {

        SceneManager.LoadScene("GameScene");

    }

    // เพิ่มฟังก์ชันนี้สำหรับปุ่ม Credit

    public void GoToCredit()

    {

        SceneManager.LoadScene("CreditScene");

    }

    // เพิ่มฟังก์ชันนี้สำหรับปุ่ม Back ในหน้า Credit ให้กลับมาหน้า Menu

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