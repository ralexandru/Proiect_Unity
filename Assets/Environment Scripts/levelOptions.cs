using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelOptions : MonoBehaviour
{
    public static string[] coduri = new string[] { "0000" };
    public int level;
    public GameObject audio;
    public void HideInitialMessage()
    {
        GameObject.Find("FirstMessage").SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("unlockedLevel", level+1);
        PlayerPrefs.Save();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void MuteUnmuteAudio()
    {
        audio.SetActive(!audio.activeInHierarchy);
    }

}
