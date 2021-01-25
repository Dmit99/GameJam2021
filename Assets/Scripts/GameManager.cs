using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject roadUser;

    [Header("Global Information")]
    private TextMeshProUGUI timer;
    [SerializeField]  private GameObject[] roadUserRow = new GameObject[4];
    public List<GameObject> roadUsersInScene;
    private Transform[] spawnLocation = new Transform[4];

    [Header("Sprites")]
    [Tooltip("Biker sprites must be 4 sprites!")]public Sprite[] bikerSprites;
    public Sprite[] scooterSprites;

    [Header("BikerInfo")]
    public int MaxAmountOfRoadUsersPerLane = 5;
    private int currentRoadUsersActive;

    [Header("Audio")]
    public AudioClip roadUserCrash;

    private readonly float timerAmount = 60;
    private float actualTimer;
    private int laneToAdd = 1;
    private float mistakes;
    private bool addingLane;
    private bool spawnedRoadUser;
    private bool generatingRoadUser;
    private bool started = false;
    private AudioSource audios;
    private bool howToPlayInfo = false;
    private Canvas instructioncanvas;

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
        howToPlayInfo = false;

        roadUsersInScene = new List<GameObject>();

        if (instructioncanvas == null)
        {
            instructioncanvas = GameObject.Find("HowTOPlayCanvas").GetComponent<Canvas>();
            instructioncanvas.enabled = howToPlayInfo;
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if(instructioncanvas == null)
            {
                instructioncanvas = GameObject.Find("HowTOPlayCanvas").GetComponent<Canvas>();
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainGame"))
        {

            if (!started && roadUserRow !=null)
            {
                started = true;

                if (audios == null)
                {
                    audios = GameObject.Find("Canvas").GetComponent<AudioSource>();
                }

                mistakes = 0;
                actualTimer = timerAmount;
                addingLane = false;
                spawnedRoadUser = false;
                generatingRoadUser = false;

                roadUsersInScene = new List<GameObject>();

                if (bikerSprites.Length != 4)
                {
                    Debug.Log("bikersprites is or to long or its to short. bikersprites must be 4! \nCurrent value is: " + bikerSprites.Length);
                }

                roadUserRow[0] = GameObject.Find("BicycleRow001");
                roadUserRow[1] = GameObject.Find("BicycleRow002");
                roadUserRow[2] = GameObject.Find("BicycleRow003");
                roadUserRow[3] = GameObject.Find("BicycleRow004");

                GetSpawnLocations();

                for (int i = 0; i < MaxAmountOfRoadUsersPerLane; i++)
                {
                    if (spawnLocation[0] != null && !spawnedRoadUser && currentRoadUsersActive < MaxAmountOfRoadUsersPerLane)
                    {
                        SpawnBiker(0);
                    }
                }
            }

            if (timer == null)
            {
                timer = GameObject.FindWithTag("Timer").GetComponent<TextMeshProUGUI>();
            }

            Timer(timerAmount);

            UI_CrossSystem.instance.PlayerMistakesMade((int)mistakes);
            MistakesMade();

            if (actualTimer < 45 && actualTimer > 44 && !addingLane)
            {
                StartCoroutine(AddLane(laneToAdd));
            }

            if (actualTimer < 35 && actualTimer > 34 && !addingLane)
            {
                StartCoroutine(AddLane(laneToAdd));
            }

            if (actualTimer < 30 && actualTimer > 29 && !addingLane)
            {
                StartCoroutine(AddLane(laneToAdd));
            }

            if (currentRoadUsersActive < MaxAmountOfRoadUsersPerLane && !generatingRoadUser)
            {
                GenerateSpawnLocationForBiker();
            }
        }
    }

    ///Changes the scene when you've made 3 mistakes.
    public void MistakesMade()
    {
        if(mistakes >= 3)
        {
            SceneManager.LoadScene(3);
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
            int scooterSpriteNumber = Random.Range(0, scooterSprites.Length+1);
            user.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            bool waitingForTrafficLights = (Random.value > 0.5f);

            Scooter scooterScript = user.AddComponent<Scooter>();

            if (waitingForTrafficLights)
            {
                scooterScript.GenerateScooter(scooterImage: scooterSprites[0], stoplightGo: waitingForTrafficLights, drive: true, accident: false, stoplightstop: waitingForTrafficLights, speed: Random.Range(1, 10), laneNumber: rowNumber, roadusername: "Biker_" + currentRoadUsersActive + 1);
            }
            else if (!waitingForTrafficLights)
            {
                scooterScript.GenerateScooter(scooterImage: scooterSprites[1], stoplightGo: waitingForTrafficLights, drive: true, accident: false, stoplightstop: waitingForTrafficLights, speed: Random.Range(1, 10), laneNumber: rowNumber, roadusername: "Biker_" + currentRoadUsersActive + 1);
            }
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
        mistakes -= 0.5f;
        audios.PlayOneShot(roadUserCrash);
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
            SceneManager.LoadScene(2);
        }
    }

    public void ChangeTheScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        if(sceneNumber == 1)
        {
            started = false;
        }
    }

    public void HowToPlay()
    {
        howToPlayInfo = !howToPlayInfo;
        if (howToPlayInfo)
        {
            instructioncanvas.enabled = true;
        }

        if (!howToPlayInfo)
        {
            instructioncanvas.enabled = false;
        }
    }

    public void CloseApplication()
    {
        Application.Quit();
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
