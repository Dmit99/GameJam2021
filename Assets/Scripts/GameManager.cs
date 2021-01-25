using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject roadUser;

    [Header("Global Information")]
    public TextMeshProUGUI timer;
    public GameObject[] roadUserRow;
    public List<GameObject> roadUsersInScene;
    private Transform[] spawnLocation;

    [Header("Sprites")]
    [Tooltip("Biker sprites must be 4 sprites!")]public Sprite[] bikerSprites;
    public Sprite[] scooterSprites;

    [Header("BikerInfo")]
    public int MaxAmountOfRoadUsersPerLane = 5;
    private int currentRoadUsersActive;

    private readonly float timerAmount = 60;
    private float actualTimer;
    private int laneToAdd = 1;
    private int mistakes;
    private bool addingLane;
    private bool spawnedRoadUser;
    private bool generatingRoadUser;

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

        mistakes = 0;
        actualTimer = timerAmount;
        addingLane = false;
        spawnedRoadUser = false;
        generatingRoadUser = false;

        roadUsersInScene = new List<GameObject>();

        if(bikerSprites.Length != 4)
        {
            Debug.Log("bikersprites is or to long or its to short. bikersprites must be 4! \nCurrent value is: " + bikerSprites.Length);
        }
    }

    void Start()
    {
        GetSpawnLocations();

        for (int i = 0; i < MaxAmountOfRoadUsersPerLane; i++)
        {
            if (spawnLocation[0] != null && !spawnedRoadUser && currentRoadUsersActive < MaxAmountOfRoadUsersPerLane)
            {
                SpawnBiker(0);
            }
        }
    }

    void Update()
    {
        Timer(timerAmount);
        MistakesMade();


        if (actualTimer < 40 && actualTimer > 39 && !addingLane)
        {
            StartCoroutine(AddLane(laneToAdd));
        }

        if (actualTimer < 30 && actualTimer > 19 && !addingLane)
        {
            StartCoroutine(AddLane(laneToAdd));
        }
        
        if (actualTimer < 20 && actualTimer > 19 && !addingLane)
        {
            StartCoroutine(AddLane(laneToAdd));
        }

        if (currentRoadUsersActive < MaxAmountOfRoadUsersPerLane && !generatingRoadUser)
        {
            GenerateSpawnLocationForBiker();
        }
    }

    //Stopt de applicatie als je 3 kruisjes behaald hebt.
    //Moet nog veranderd worden naar andere scene.
    public void MistakesMade()
    {
        if(mistakes >= 3)
        {
            Application.Quit();
        }
    }

    public void CurrentBikersListChecker()
    {
        for (int i = 0; i < roadUsersInScene.Count; i++)
        {
            if(roadUsersInScene[i] == null)
            {
                roadUsersInScene.Remove(roadUsersInScene[i]);
            }
        }
    }

    private void GenerateSpawnLocationForBiker()
    {
        generatingRoadUser = true;
        int randomNumber = Random.Range(0, roadUserRow.Length);

        if (roadUserRow[randomNumber].activeSelf && !spawnedRoadUser && currentRoadUsersActive < MaxAmountOfRoadUsersPerLane)
        {
            SpawnBiker(randomNumber);
        }
        else GenerateSpawnLocationForBiker();

        generatingRoadUser = false;
    }

    private void SpawnBiker(int rowNumber)
    {
        spawnedRoadUser = true;
        int isScooter = Random.Range(0, 2);

        GameObject user = Instantiate(roadUser, spawnLocation[rowNumber].transform.position, spawnLocation[rowNumber].transform.rotation);

        ///0 means false, so its not a scooter.
        ///1 means true, so it is.
        if (isScooter == 0)
        {
            Biker bikerScript = user.AddComponent<Biker>();
            bikerScript.GenerateBiker(bikerImage001: bikerSprites[0], bikerImage002: bikerSprites[1], bikerImage003: bikerSprites[2], bikerImage004: bikerSprites[3],drive: true, accident: false, stoplightstop: false, speed: Random.Range(1, 10), laneNumber: rowNumber, roadusername: "Biker_" + currentRoadUsersActive + 1);
        }
        else if (isScooter == 1)
        {
            bool waitingForTrafficLights = (Random.value > 0.5f);

            Scooter scooterScript = user.AddComponent<Scooter>();
            scooterScript.GenerateScooter(scooterImage: scooterSprites[0], stoplightGo: waitingForTrafficLights, drive: true, accident: false, stoplightstop: waitingForTrafficLights, speed: Random.Range(1, 10), laneNumber: rowNumber, roadusername: "Biker_" + currentRoadUsersActive + 1);
        }

        roadUsersInScene.Add(user);
        currentRoadUsersActive++;
        spawnedRoadUser = false;
    }

    public int GetCurrentActiveBikers()
    {
        return currentRoadUsersActive;
    }

    public void SetCurrentActiveBikers()
    {
        currentRoadUsersActive--;
    }

    public void RoadUserCrashed()
    {
        mistakes++;
        UI_CrossSystem.instance.PlayerMistakesMade(mistakes);
    }

    private void GetSpawnLocations()
    {
        spawnLocation = new Transform[roadUserRow.Length];

        for (int i = 0; i < roadUserRow.Length; i++)
        {
            if (roadUserRow[i] == roadUserRow[0])
            {
                roadUserRow[i].SetActive(true);

                foreach (Transform spawnLocation in roadUserRow[i].transform)
                {
                    if (spawnLocation.name == "BicycleSpawnLocation")
                    {
                        this.spawnLocation[i] = spawnLocation;
                    }
                }

            }
            else
            {
                foreach (Transform spawnLocation in roadUserRow[i].transform)
                {
                    if (spawnLocation.name == "BicycleSpawnLocation")
                    {
                        this.spawnLocation[i] = spawnLocation;
                    }
                }

                roadUserRow[i].SetActive(false);
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
        roadUserRow[laneNumber].SetActive(true);
        yield return new WaitForSeconds(1);
        laneToAdd++;
        addingLane = false;
    }
}
