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

    private float actualTimer;
    private int laneToAdd = 1;
    private bool addingLane;
    private bool spawnedBiker;

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

        bikers = new List<GameObject>();
    }


    void Start()
    {
        GetSpawnLocations();
    }

    void Update()
    {
        Timer(timerAmount);

        if (actualTimer < 40 && actualTimer > 39 && !addingLane)
        {
           StartCoroutine(AddLane(laneToAdd));
        }
        if(actualTimer < 20 && actualTimer > 19 && !addingLane)
        {
            StartCoroutine(AddLane(laneToAdd));
        }
       
    }

    private void SpawnBiker()
    {
        if (spawnLocation[0] != null && !spawnedBiker)
        {
            spawnedBiker = true;
            GameObject biker001 = Instantiate(fietser, spawnLocation[0].transform.position, spawnLocation[0].transform.rotation);
            Bicycle bikerScript = biker001.AddComponent<Bicycle>();
            bikerScript.InstantiateBiker(spawnedRight: false);

            bikers.Add(biker001);
        }
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
