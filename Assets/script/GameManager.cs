using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameManager : MonoBehaviour

{

    public static GameManager Instance { get; private set; }

    [Header("Settings")]

    public int ammo = 10;        

    public int scoreToWin = 3;   

    private int currentScore = 0;

    [Header("UI References")]

    public Text scoreText;       

    public Text ammoText;        

    private void Awake()

    {

        if (Instance == null) Instance = this;

        UpdateUI();

    }

    public void UseAmmo()

    {

        ammo--;

        UpdateUI();

        if (ammo <= 0 && currentScore < scoreToWin)

        {

            Invoke("CheckLoseCondition", 3.0f);

        }

    }

    public void AddScore()

    {

        currentScore++;

        UpdateUI();

        if (currentScore >= scoreToWin)

        {

            // ระบบเช็คด่านเพื่อส่งไป Scene ถัดไป

            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "GameScene") 

            {

                SceneManager.LoadScene("Level2Scene"); // จบด่าน 1 ไปด่าน 2

            }

            else if (currentSceneName == "Level2Scene")

            {

                SceneManager.LoadScene("WinScene");    // จบด่าน 2 ไปหน้าชนะ

            }

        }

    }

    void UpdateUI()

    {

        if (scoreText != null) scoreText.text = "Score: " + currentScore + "/" + scoreToWin;

        if (ammoText != null) ammoText.text = "Carrots: " + ammo;

    }

    void CheckLoseCondition()

    {

        if (currentScore < scoreToWin)

        {

            SceneManager.LoadScene("LoseScene");

        }

    }

}