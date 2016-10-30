using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Extensions.System.Colections;
using Extensions.UnityEngine;
using UnityEngine.UI;

public class GameManagerOne : MonoBehaviour {

    private static GameManagerOne instance = null;

    public string playerGender;
    public string playerLike;
    private string activeRule;
    private string activeColor;
    private bool lapsus;
    private bool noColor;

    private int tenderCount;
    private int randomTender;
    private int countLevel;
    private int lifes;
    private int score;

    public List<string> colorEyeN;
    public List<string> colorHairN;
    public List<string> colorSkinN;

    public List<string> partFaceList;

    public static GameManagerOne Instance
    {
        get
        {
            return GameManagerOne.instance;
        }
    }

    void Awake()
    {
        if (GameManagerOne.instance == null)
            GameManagerOne.instance = this;
        else if (GameManagerOne.instance != this)
            GameObject.Destroy(this.gameObject);
        // No destruir con los cambios de escena
        GameObject.DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Play")
        {
            tenderCount = 0;
            countLevel = 0;
            score = 0;
            lifes = 3;
            activeColor = null;
            activeRule = null;
            lapsus = false;

            randomTender = Random.Range(6, 11);

        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void checkFlirt(profileClass profile, bool like)
    {
        // Comprobamos el género
        if ((profile.gender == playerLike && like && !lapsus)
            || (profile.gender != playerLike && !like && !lapsus)
            || (profile.gender != playerLike && like && lapsus)
            || (profile.gender == playerLike &&  !like && lapsus))
        {
            // Comprobamos la regla activa
            if(activeRule != null)
            {
                string profileColor = null;
                if(activeRule == "hair")
                {
                    profileColor = profile.hairNC;
                } else if(activeRule == "eye")
                {
                    profileColor = profile.eyeNC;
                } else if (activeRule == "skin")
                {
                    profileColor = profile.skinNC;
                }
                if ((profileColor == activeColor && !noColor) 
                    || (profileColor != activeColor && noColor))
                {
                    flirtSuccess("Que");
                }
                else
                {
                    // No es el color correcto, restamos una vida
                    flirtFailure("putaso");
                }
            } else
            {
                flirtSuccess("Que");
            }
        } else
        {
            // No es el genero correcto, restamos una vida
            flirtFailure("putaso");
        }
        // Revisamos el cambio de regla
        tenderCount++;
        if (tenderCount >= randomTender)
        {
            countLevel++;
            randomRule();
            randomLevelN();
            tenderCount = 0;
        }
    }

    public void flirtSuccess(string gender)
    {
        if (gender=="cake")
        {
            if (lifes<3)
            {
            lifes++;
            }
        }
        score += 10;
    }

    public void flirtFailure(string gender)
    {
        if (gender=="orc")
        {
            //gameOver
        }
        else
        {
            lifes--;
            if (lifes<=0)
            {
                //gameOver
            }
        }

    }

    public void randomRule()
    {
        
        activeRule =partFaceList.RandomItem<string>();
        switch (activeRule)
        {
            case "eye":
                activeColor = colorEyeN.RandomItem<string>();
                noColor = RandomUtil.Chance(0.5f);
                break;
            case "hair":
                activeColor = colorHairN.RandomItem<string>();
                noColor = RandomUtil.Chance(0.5f);
                break;
            case "skin":
                activeColor = colorSkinN.RandomItem<string>();
                noColor = RandomUtil.Chance(0.5f);
                break;
        }
        ruleText(GameObject.FindGameObjectWithTag("ruleText").GetComponent<Text>());
    }

    public void randomLevelN()
    {
        switch (countLevel)
        {
            case 1:
                randomTender = Random.Range(5,9);
                break;
            case 2:
                randomTender = Random.Range(4, 7);
                break;
            case 3:
                randomTender = Random.Range(3, 5);
                lapsus = RandomUtil.Chance(0.5f);
                break;
            default:
                randomTender = Random.Range(2, 4);
                lapsus = RandomUtil.Chance(0.5f);
                break;
        }
    }

    public void ruleText(Text t1)
    {
        if (t1 == null)
            return;

        if (activeRule!=null)
        {
            t1.text = noColor ? "NO " : "ONLY ";
            t1.text += activeColor.ToUpper();

            switch (activeRule)
            {
                case "eye":
                    t1.text += " EYES";
                    break;
                case "hair":
                    t1.text += " HAIR";
                    break;
                case "skin":
                    t1.text += " RACE";
                    break;
            }
        }
        else
        {
            t1.text = string.Empty;
        }
    }

    public void setPlayerGenderMale(bool sel)
    {
        if (sel)
        {
            GameManagerOne.Instance.playerGender = "male";
        }
    }

    public void setPlayerGenderFemale(bool sel)
    {
        if (sel)
        {
            GameManagerOne.Instance.playerGender = "female";
        }
    }

    public void setPlayerLikeFemale(bool sel)
    {
        if (sel)
        {
            GameManagerOne.Instance.playerLike = "female";
        }
    }

    public void setPlayerLikeMale(bool sel)
    {
        if (sel)
        {
            GameManagerOne.Instance.playerLike = "male";
        }
    }
}
