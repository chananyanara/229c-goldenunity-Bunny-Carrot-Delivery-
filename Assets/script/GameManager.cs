using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameManager : MonoBehaviour

{

    public static GameManager Instance { get; private set; }

    [Header("Settings")]

    public int ammo = 10;        // จำนวนแครอทที่มี

    public int scoreToWin = 3;   // คะแนนที่ต้องได้

    private int currentScore = 0;

    [Header("UI References")]

    public Text scoreText;       // ลาก Text คะแนนมาใส่

    public Text ammoText;        // ลาก Text จำนวนแครอทมาใส่

    private void Awake()

    {

        if (Instance == null) Instance = this;

        UpdateUI();

    }

    // เรียกใช้ตอนกดยิง

    public void UseAmmo()

    {

        ammo--;

        UpdateUI();

        if (ammo <= 0 && currentScore < scoreToWin)

        {

            // ถ้าแครอทหมด ให้รอ 3 วินาทีเผื่อลูกสุดท้ายกำลังบิน แล้วค่อยแพ้

            Invoke("CheckLoseCondition", 3.0f);

        }

    }

    // เรียกใช้ตอนแครอทลงตะกร้า

    public void AddScore()

    {

        currentScore++;

        UpdateUI();

        if (currentScore >= scoreToWin)

        {

            SceneManager.LoadScene("WinScene"); // ชื่อ Scene ต้องตรงกับที่ตั้งไว้

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

            SceneManager.LoadScene("LoseScene"); // ชื่อ Scene ต้องตรงกับที่ตั้งไว้

        }

    }

}