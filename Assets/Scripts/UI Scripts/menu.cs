using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public int level;
    public List<bool> unlockedLevels = new List<bool>();

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Levels")
        {
            unlockedLevels.AddRange(new bool[] { true, false, false, false });
            if (PlayerPrefs.GetInt("unlockedLevel", 0) != null)
            {

                for (int i = 0; i < PlayerPrefs.GetInt("unlockedLevel", 0); i++)
                {
                    unlockedLevels[i] = true;
                }
            }
            for (int i = 0; i < unlockedLevels.Count; i++)
            {
                if (unlockedLevels[i])
                {
                    GameObject.Find((i + 1).ToString()).GetComponent<Button>().GetComponent<Image>().color = Color.black;
                    GameObject.Find((i + 1).ToString()).GetComponent<Button>().transform.GetChild(0).gameObject.SetActive(true);
                    Debug.Log("Changed button style");
                }

            }
        }
    }


    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Scenes/Menu 1");
    }

    public void OpenLevels()
    {
        SceneManager.LoadScene("Scenes/Levels");
        Debug.Log("test");
    }
    public void OpenLevel()
    {
        SceneManager.LoadScene("Scenes/Levels/Level " + EventSystem.current.currentSelectedGameObject.name);
        Debug.Log("Scenes/Levels/Level " + EventSystem.current.currentSelectedGameObject.name);
        Debug.Log($"Button Name: {EventSystem.current.currentSelectedGameObject.name}, Button Text: {EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text}");


    }
    public void ResetProgress()
    {
        PlayerPrefs.SetInt("unlockedLevel", 1);
        PlayerPrefs.Save();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Levels/Level 1");
        Debug.Log("test");
    }

    public void MoreInfo()
    {
        SceneManager.LoadScene("Scenes/moreInfo");
    }
}
