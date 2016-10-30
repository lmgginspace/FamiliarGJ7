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
            GameManagerOne.Instance.tenderCount = 0;
            GameManagerOne.Instance.countLevel = 0;
            GameManagerOne.Instance.score = 0;
            GameManagerOne.Instance.lifes = 3;
            GameManagerOne.Instance.activeColor = null;
            GameManagerOne.Instance.activeRule = null;
            GameManagerOne.Instance.lapsus = false;

            GameManagerOne.Instance.randomTender = Random.Range(6, 11);

        }
    }

    public void loadiScene(string scn)
    {
        if (scn=="reset")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(scn);
        }
        
    } 

    public void checkFlirt(profileClass profile, bool like)
    {
        // Comprobamos el género
        if ((profile.gender == GameManagerOne.Instance.playerLike && like && !GameManagerOne.Instance.lapsus)
            || (profile.gender != GameManagerOne.Instance.playerLike && !like && !GameManagerOne.Instance.lapsus)
            || (profile.gender != GameManagerOne.Instance.playerLike && like && GameManagerOne.Instance.lapsus)
            || (profile.gender == GameManagerOne.Instance.playerLike &&  !like && GameManagerOne.Instance.lapsus))
        {
            // Comprobamos la regla activa
            if(GameManagerOne.Instance.activeRule != null)
            {
                string profileColor = null;
                if(GameManagerOne.Instance.activeRule == "hair")
                {
                    profileColor = profile.hairNC;
                } else if(GameManagerOne.Instance.activeRule == "eye")
                {
                    profileColor = profile.eyeNC;
                } else if (GameManagerOne.Instance.activeRule == "skin")
                {
                    profileColor = profile.skinNC;
                }
                if ((profileColor == GameManagerOne.Instance.activeColor && !noColor) 
                    || (profileColor != GameManagerOne.Instance.activeColor && noColor))
                {
                    GameManagerOne.Instance.flirtSuccess("Que");
                }
                else
                {
                    // No es el color correcto, restamos una vida
                    GameManagerOne.Instance.flirtFailure("putaso");
                }
            } else
            {
                GameManagerOne.Instance.flirtSuccess("Que");
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
            GameManagerOne.Instance.StartCoroutine(GameManagerOne.Instance.gameOver());
        }
        else
        {
            GameManagerOne.Instance.lifes--;
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

    public IEnumerator gameOver()
    {
        GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>().text = "Score: " + score;
        float time = 0.0f, totalTime = 1.0f;
        while (time < totalTime)
        {
            time += Time.deltaTime;
            GameObject.FindGameObjectWithTag("gOScreen").GetComponent<CanvasGroup>().alpha = time;
            yield return null;
        }
    }
    
}
