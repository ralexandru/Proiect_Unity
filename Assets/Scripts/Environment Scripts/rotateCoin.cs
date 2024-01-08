using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCoin : MonoBehaviour
{
    public SpiderController spiderController;
    // Viteza de rotatie a monedei
    public float rotationSpeed = 30f;

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        // Roteste moneda in functie de rotationSpeed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Verifica daca exista o coliziune intre jucator si moneda
        if (other.CompareTag("Player"))
        {
            // Daca playerul a atins moneda, atunci aceasta va fi colectata prin apelarea functiei de mai jos
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        gameObject.SetActive(false);
        levelOptions2.coinsFound += 1;
        spiderController.IncreaseSpiderSpeed(1.05f);
    }
}
