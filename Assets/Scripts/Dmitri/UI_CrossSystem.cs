using UnityEngine;
using UnityEngine.UI;

public class UI_CrossSystem : MonoBehaviour
{
    private int mistakesMade = 0;
    private int numOfCrosses = 5;

    public Image[] cross;
    public Sprite fullCross;
    public Sprite emptyCross;

    public static UI_CrossSystem instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerMistakesMade(int currentMistakes)
    {
        mistakesMade = currentMistakes -1;

        for (int i = 0; i < cross.Length; i++)
        {
            if (i > mistakesMade)
            {
                cross[i].sprite = emptyCross;
            }
            else
            {
                cross[i].sprite = fullCross;
            }

            if (i < numOfCrosses)
            {
                cross[i].enabled = true;
            }
            else
            {
                cross[i].enabled = false;
            }
        }
    }
}
