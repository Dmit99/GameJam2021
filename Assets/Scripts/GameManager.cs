using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject fietser;

    [Header("Global Information")]
    private float timerAmount = 60;
    public TextMeshProUGUI timer;

    [Header("Sprites")]
    public GameObject[] bicycleRow;
    public List<GameObject> bikers;
    private Transform[] spawnLocation;

    [Header("BikerInfo")]
    public int MaxAmountOfBikersPerLane = 5;
    private int currentBikersActive;

    private float actualTimer;
    private int laneToAdd = 1;
    private bool addingLane;
    private bool spawnedBiker;
    private bool generatingBiker;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }

        actualTimer = timerAmount;
        addingLane = false;
        spawnedBiker = false;
        generatingBiker = false;

        bikers = new List<GameObject>();
    }

    void Start()
    {
        GetSpawnLocations();

        for (int i = 0; i < MaxAmountOfBikersPerLane; i++)
        {
            if (spawnLocation[0] != null && !spawnedBiker && currentBikersActive < MaxAmountOfBikersPerLane)
            {
                SpawnBiker(0);
            }
        }
    }

    void Update()
    {
        Timer(timerAmount);

        if (actualTimer < 40 && actualTimer > 39 && !addingLane)
        {
            StartCoroutine(AddLane(laneToAdd));
        }
        if (actualTimer < 20 && actualTimer > 19 && !addingLane)
        {
            StartCoroutine(AddLane(laneToAdd));
        }

        if (currentBikersActive < MaxAmountOfBikersPerLane && !generatingBiker)
        {
            GenerateSpawnLocationForBiker();
        }
    }

    public void CurrentBikersListChecker()
    {
        for (int i = 0; i < bikers.Count; i++)
        {
            if(bikers[i] == null)
            {
                bikers.Remove(bikers[i]);
            }
        }
    }

    private void GenerateSpawnLocationForBiker()
    {
        generatingBiker = true;
        int randomNumber = Random.Range(0, bicycleRow.Length);

        if (bicycleRow[randomNumber].activeSelf && !spawnedBiker && currentBikersActive < MaxAmountOfBikersPerLane)
        {
            SpawnBiker(randomNumber);
        }
        else GenerateSpawnLocationForBiker();

        generatingBiker = false;
    }

    private void SpawnBiker(int rowNumber)
    {
        spawnedBiker = true;
        GameObject biker = Instantiate(fietser, spawnLocation[rowNumber].transform.position, spawnLocation[rowNumber].transform.rotation);
        Bicycle bikerScript = biker.AddComponent<Bicycle>();
        bikerScript.InstantiateBiker("Biker_" + currentBikersActive +1, Random.Range(1, 10));
        bikers.Add(biker);
        currentBikersActive++;

        spawnedBiker = false;
    }

    public int GetCurrentActiveBikers()
    {
        return currentBikersActive;
    }

    public void SetCurrentActiveBikers()
    {
        currentBikersActive--;
    }

    private void GetSpawnLocations()
    {
        spawnLocation = new Transform[bicycleRow.Length];

        for (int i = 0; i < bicycleRow.Length; i++)
        {
            if (bicycleRow[i] == bicycleRow[0])
            {
                bicycleRow[i].SetActive(true);

                foreach (Transform spawnLocation in bicycleRow[i].transform)
                {
                    if (spawnLocation.name == "BicycleSpawnLocation")
                    {
                        this.spawnLocation[i] = spawnLocation;
                    }
                }

            }
            else
            {
                foreach (Transform spawnLocation in bicycleRow[i].transform)
                {
                    if (spawnLocation.name == "BicycleSpawnLocation")
                    {
                        this.spawnLocation[i] = spawnLocation;
                    }
                }

                bicycleRow[i].SetActive(false);
            }
        }
    }

    private void Timer(float startTime)
    {
        actualTimer -= 1 * Time.deltaTime;
        timer.text = "Time: " + actualTimer.ToString("F2");

        if(actualTimer <= 0)
        {
            Debug.Log("Time done!");
            actualTimer = startTime;
        }
    }

    IEnumerator AddLane(int laneNumber)
    {
        addingLane = true;
        bicycleRow[laneNumber].SetActive(true);
        yield return new WaitForSeconds(1);
        laneToAdd++;
        addingLane = false;
    }
}
