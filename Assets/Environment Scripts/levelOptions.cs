using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelOptions : MonoBehaviour
{
    public static string[] coduri = new string[] { "0000" };
    public int level;
    public GameObject audio;
    public float countdownTime = 10.0f;
    public int nextLevel = 2;
    public TextMeshProUGUI countdownText;
    public GameObject gameOverUI;
    public GameObject portal;
    public GameObject nextLevelUI;
    public List<PortalPositions> portalPositions = new List<PortalPositions>
    {
        new PortalPositions(1237.99f, -718.82f, 309.11f),
        new PortalPositions(1272.78f, -718.82f, 318.79f),
        new PortalPositions(1272.93f, -718.82f, 330.96f),
        new PortalPositions(1249.45f, -718.82f, 329.91f),
        new PortalPositions(1240.92f, -718.82f, 329.91f),
        new PortalPositions(1216.42f, -718.82f, 335.26f),
        new PortalPositions(1225.69f, -718.82f, 316.5f)
    };
    public void HideInitialMessage()
    {
        GameObject.Find("FirstMessage").SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        SpawnPortalAtRandomPosition();
        UpdateCountdownText();
    }
    void SpawnPortalAtRandomPosition()
    {
        if (portalPositions.Count > 0)
        {
            // Generate a random index
            int randomIndex = Random.Range(0, portalPositions.Count);

            // Retrieve the PortalPositions object at the random index
            PortalPositions randomPosition = portalPositions[randomIndex];

            // Use the retrieved position to spawn the portal
            portal.transform.position = new Vector3(randomPosition.positionX, randomPosition.positionY, randomPosition.positionZ);
            //Instantiate(portal, new Vector3(randomPosition.positionX, randomPosition.positionY, randomPosition.positionZ), Quaternion.identity);
        }
        else
        {
            Debug.LogError("No portal positions available!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            UpdateCountdownText();
        }
        else
        {

            GameOver();
            // Countdown has reached zero, handle the event (e.g., end the game)
            Debug.Log("Countdown reached zero!");
        }
    }

    public void ShowNextLevelUI()
    {
        nextLevelUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void UpdateCountdownText()
    {
        // Format the countdown time as mm:ss
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("unlockedLevel", level+1);
        PlayerPrefs.Save();
    }
    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level "+nextLevel);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level 1");
    }

    public void MuteUnmuteAudio()
    {
        audio.SetActive(!audio.activeInHierarchy);
    }

}

public class PortalPositions
{
    public float positionX { get; set; }
    public float positionY { get; set; }
    public float positionZ { get; set; }

    public PortalPositions(float positionX, float positionY, float positionZ)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.positionZ = positionZ;
    }
}