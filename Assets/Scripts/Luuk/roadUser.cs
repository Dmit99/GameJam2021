using System.Collections;
using UnityEngine;

public class roadUser : MonoBehaviour
{
    protected bool drive;
    protected bool accident;
    protected bool stoplightstop;
    protected int laneNumber;

    protected float speed;

    protected string roadusername;

    /// <summary>
    /// This constructor should not be called!
    /// </summary>
    /// <param name="drive"></param>
    /// <param name="accident"></param>
    /// <param name="stoplightstop"></param>
    /// <param name="speed"></param>
    /// <param name="roadusername"></param>
    public void GenerateRoadUser(bool drive, bool accident, bool stoplightstop, int speed, int laneNumber,string roadusername)
    {
        this.laneNumber = laneNumber;
        this.drive = drive;
        this.accident = accident;
        this.stoplightstop = stoplightstop;
        this.speed = speed;
        this.roadusername = roadusername;    
    }

    private void Start()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.236f);
    }

    private void Update()
    {
        if (!accident)
        {
            Moving();
        }
    }

    public void Moving()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LaneCollider")
        {
            GameManager.instance.RoadUserSavePass();
            StartCoroutine(Die());
        }
        
        if(collision.gameObject.tag == "RoadUser")
        {
            roadUser userscript = collision.gameObject.GetComponent<roadUser>();
            if (userscript.laneNumber != laneNumber)
            {
                Debug.Log("Accident!!!!");
                accident = true;
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        GameManager.instance.SetCurrentActiveBikers();
        GameManager.instance.CurrentBikersListChecker();
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }

    protected float CalculateSpriteSpeed(float currentspeed)
    {
        float value = 1 / currentspeed;
        return value;
    }

    public bool GetAccident()
    {
        return accident;
    }

    public bool GetDriversState()
    {
        return drive;
    }

    public bool GetStopforstoplightState()
    {
        return stoplightstop;
    }

    public float Getspeed()
    {
        return speed;
    }

    public string Getname()
    {
        return roadusername;
    }
}
