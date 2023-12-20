using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public List<bool> unlockedLevels = new List<bool>();
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("Scenes/Menu");
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
}
