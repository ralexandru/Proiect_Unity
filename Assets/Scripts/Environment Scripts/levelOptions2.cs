using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelOptions2 : MonoBehaviour
{
    public static int coinsFound = 0;
    public int allCoinsNumber = 11;
    public GameObject winGameUI;
    public TextMeshProUGUI coinsFoundText;
    public GameObject coinPrefab;
    public int numberOfCoins = 10;
    public float spawnRadius = 20f;
    public float maxSpawnHeightDifference = 5f;
    private GameObject[] waterAreas;
    public Camera playerCamera;
    public Camera spiderCamera;
    public float switchDuration = 5f;
    public TextMeshProUGUI startMessage;
    private bool isSwitching = true;
    private float switchTimer = 0f;

    void Start()
    {
        Time.timeScale = 1f;
        waterAreas = GameObject.FindGameObjectsWithTag("Water");
        EnableSpiderCamera();
        DisablePlayerCamera();
        SpawnCoins();
    }

    void Update()
    {
        coinsFoundText.text = coinsFound.ToString();
        if (coinsFound == allCoinsNumber)
        {
            winGameUI.SetActive(true);
        }
        if (isSwitching)
        {
            switchTimer += Time.deltaTime;

            if (switchTimer >= switchDuration)
            {
                EnablePlayerCamera();
                DisableSpiderCamera();
                isSwitching = false;
            }
        }
    }
    void EnableSpiderCamera()
    {
        spiderCamera.enabled = true;
        startMessage.enabled = true;
    }

    void DisableSpiderCamera()
    {
        startMessage.enabled = false;
        spiderCamera.enabled = false;
    }

    void EnablePlayerCamera()
    {
        playerCamera.enabled = true;
    }

    void DisablePlayerCamera()
    {
        playerCamera.enabled = false;
    }

    void SpawnCoins()
    {
        Terrain terrain = Terrain.activeTerrain;

        if (terrain == null)
        {
            Debug.LogError("No active terrain found. Make sure you have a terrain in your scene.");
            return;
        }

        TerrainCollider terrainCollider = terrain.GetComponent<TerrainCollider>();

        if (terrainCollider == null)
        {
            Debug.LogError("No terrain collider found. Make sure your terrain has a collider.");
            return;
        }

        // Calculeaza un punct central in cadrul terenului/hartii
        Vector3 centralPoint = terrain.transform.position + new Vector3(terrain.terrainData.size.x / 2f, 0f, terrain.terrainData.size.z / 2f);

        for (int i = 0; i < numberOfCoins; i++)
        {
            // Genereaza un punct random bazat pe punctul central al hartii
            float randomX = Random.Range(centralPoint.x - spawnRadius, centralPoint.x + spawnRadius);
            float randomZ = Random.Range(centralPoint.z - spawnRadius, centralPoint.z + spawnRadius);

            // Verifica daca punctul generat se afla pe apa
            if (IsPositionOutsideWaterAreas(new Vector3(randomX, terrain.transform.position.y, randomZ)))
            {
                // Folosesc raycast pentru a determina distanta pana la pamant
                Ray ray = new Ray(new Vector3(randomX, terrain.transform.position.y + terrain.terrainData.size.y + 10f, randomZ), Vector3.down);
                RaycastHit hit;

                if (terrainCollider.Raycast(ray, out hit, Mathf.Infinity))
                {
                   
                    Vector3 randomPosition = new Vector3(randomX, hit.point.y+2f, randomZ);

                    // Instantiez moneda intr-o pozitie aleatoare
                    GameObject coin = Instantiate(coinPrefab, randomPosition, Quaternion.identity);
                }
                else
                {
                   Debug.LogWarning("Failed to spawn coin. Raycast did not hit the terrain.");
                }
            }
            else
            {
                Debug.Log("Position is inside a water area. Skipping coin spawn.");
            }
        }
    }

    bool IsPositionOutsideWaterAreas(Vector3 position)
    {
        foreach (GameObject waterArea in waterAreas)
        {
            Collider waterCollider = waterArea.GetComponent<Collider>();

            if (waterCollider != null && waterCollider.bounds.Contains(position))
            {
                return false; // Pozitia generata este in apa
            }
        }

        return true; // Pozitia generata nu este in apa
    }
}
