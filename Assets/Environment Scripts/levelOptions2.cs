using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelOptions2 : MonoBehaviour
{
    public static int coinsFound = 0;
    public int allCoinsNumber = 1;
    public GameObject winGameUI;
    public TextMeshProUGUI coinsFoundText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsFoundText.text = coinsFound.ToString();
        if (coinsFound == allCoinsNumber)
        {
            winGameUI.SetActive(true);
        }
    }
}
