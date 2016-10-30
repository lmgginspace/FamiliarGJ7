using Extensions.System.Colections;
using Extensions.UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerOne : MonoBehaviour {

    private static GameManagerOne instance = null;

    public string playerGender;
    public string playerLike;
    private string activeRule;
    private string activeColor;
    private bool lapsus;
    private bool noColor;
    public bool gameOverB=false;

    private int tenderCount;
    private int randomTender;
    private int countLevel;
    private int lifes;
    private int score;

    public List<string> colorEyeN;
    public List<string> colorHairN;
    public List<string> colorSkinN;

    public List<string> partFaceList;

    public AudioClip music;
    public AudioClip effectLike;
    public AudioClip effectNope;
    public AudioClip effectSexy;
    public AudioClip effectLapsus;
    public AudioClip effectFlirtOver;

    public static GameManagerOne Instance
    {
        get
        {
            return GameManagerOne.instance;
        }
    }

    public bool PlayerIsHomosexual
    {
        get { return GameManagerOne.Instance.playerGender == GameManagerOne.instance.playerLike; }
    }

    public event System.Action OnRuleChanged = delegate { };
    public event System.Action<bool> OnLapsusChanged = delegate { };
    public event System.Action<int> OnLifesChanged = delegate { };

    void Awake()
    {
        if (GameManagerOne.instance == null)
            GameManagerOne.instance = this;
        else if (GameManagerOne.instance != this)
            GameObject.Destroy(this.gameObject);
        // No destruir con los cambios de escena
        GameObject.DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        AudioManager.Instance.PlayMusic(music);
        AudioManager.Instance.MusicVolume = 0.5f;
        CountDown.OnTimerEnded += GameManagerOne.Instance.OnTimerEnded;
    }

    private void OnDestroy()
    {
        CountDown.OnTimerEnded -= GameManagerOne.Instance.OnTimerEnded;
    }

    private void OnTimerEnded()
    {
        GameManagerOne.Instance.flirtFailure("putita");
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name=="Play")
        {
            GameObject.FindGameObjectWithTag("scorePlayText").GetComponent<Text>().text = "Score: " + GameManagerOne.Instance.score;
        }
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
            GameManagerOne.Instance.gameOverB = false;

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
            AudioManager.Instance.PlaySoundEffect(effectSexy);
        }
        
    } 

    public void checkFlirt(profileClass profile, bool like)
    {
        // Comprobamos el género si es like
        if ((profile.gender == GameManagerOne.Instance.playerLike && like && !GameManagerOne.Instance.lapsus)
            || (profile.gender != GameManagerOne.Instance.playerLike && like && GameManagerOne.Instance.lapsus))
        {
            // Comprobamos la regla activa
            if (GameManagerOne.Instance.activeRule != null)
            {
                string profileColor = null;
                if (GameManagerOne.Instance.activeRule == "hair")
                {
                    profileColor = profile.hairNC;
                } else if (GameManagerOne.Instance.activeRule == "eye")
                {
                    profileColor = profile.eyeNC;
                } else if (GameManagerOne.Instance.activeRule == "skin")
                {
                    profileColor = profile.skinNC;
                }
                //Comprobamos color
                if ((profileColor == GameManagerOne.Instance.activeColor && !noColor && like)
                    || (profileColor != GameManagerOne.Instance.activeColor && noColor && like)
                    || (profileColor != GameManagerOne.Instance.activeColor && !noColor && !like)
                    || (profileColor == GameManagerOne.Instance.activeColor && noColor && !like))
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
        } // Si es nope a un género que no queremos
        else if ((profile.gender != GameManagerOne.Instance.playerLike && !like && !GameManagerOne.Instance.lapsus)
            || (profile.gender == GameManagerOne.Instance.playerLike && !like && GameManagerOne.Instance.lapsus))
        {
            GameManagerOne.Instance.flirtSuccess("Que");
        } // Si es nope a un género que queremos
        else if ((profile.gender == GameManagerOne.Instance.playerLike && !like && !GameManagerOne.Instance.lapsus)
            || (profile.gender != GameManagerOne.Instance.playerLike && !like && GameManagerOne.Instance.lapsus))
        {
            string profileColor = null;
            if (GameManagerOne.Instance.activeRule == "hair")
            {
                profileColor = profile.hairNC;
            }
            else if (GameManagerOne.Instance.activeRule == "eye")
            {
                profileColor = profile.eyeNC;
            }
            else if (GameManagerOne.Instance.activeRule == "skin")
            {
                profileColor = profile.skinNC;
            }
            //Comprobamos color
            if ((profileColor == GameManagerOne.Instance.activeColor && !noColor && like)
                || (profileColor != GameManagerOne.Instance.activeColor && noColor && like)
                || (profileColor != GameManagerOne.Instance.activeColor && !noColor && !like)
                || (profileColor == GameManagerOne.Instance.activeColor && noColor && !like))
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
            // No es el genero correcto, restamos una vida
            GameManagerOne.Instance.flirtFailure("putaso");
        }

        // Revisamos el cambio de regla
        GameManagerOne.Instance.tenderCount++;
        if (GameManagerOne.Instance.tenderCount >= GameManagerOne.Instance.randomTender)
        {
            GameManagerOne.Instance.countLevel++;
            GameManagerOne.Instance.randomRule();
            GameManagerOne.Instance.randomLevelN();
            GameManagerOne.Instance.tenderCount = 0;

            GameManagerOne.Instance.OnRuleChanged();
        }
    }

    public void flirtSuccess(string gender)
    {
        if (gender=="cake")
        {
            if (GameManagerOne.Instance.lifes <3)
            {
                GameManagerOne.Instance.lifes++;
                GameManagerOne.Instance.OnLifesChanged(GameManagerOne.Instance.lifes);
            }
        }
        GameManagerOne.Instance.score += 10;
        AudioManager.Instance.PlaySoundEffect(effectLike);
    }

    public void flirtFailure(string gender)
    {
        if (gender=="orc")
        {
            GameManagerOne.Instance.StartCoroutine(GameManagerOne.Instance.gameOver());
            GameManagerOne.Instance.lifes = 0;
            GameManagerOne.Instance.OnLifesChanged(GameManagerOne.Instance.lifes);
        }
        else
        {
            GameManagerOne.Instance.lifes--;
            GameManagerOne.Instance.OnLifesChanged(GameManagerOne.Instance.lifes);
            if (GameManagerOne.Instance.lifes <=0)
            {
                GameManagerOne.Instance.StartCoroutine(GameManagerOne.Instance.gameOver());            }
        }
        AudioManager.Instance.PlaySoundEffect(effectNope);
    }

    public void randomRule()
    {

        GameManagerOne.Instance.activeRule = GameManagerOne.Instance.partFaceList.RandomItem<string>();
        switch (GameManagerOne.Instance.activeRule)
        {
            case "eye":
                GameManagerOne.Instance.activeColor = colorEyeN.RandomItem<string>();
                GameManagerOne.Instance.noColor = RandomUtil.Chance(0.5f);
                break;
            case "hair":
                GameManagerOne.Instance.activeColor = colorHairN.RandomItem<string>();
                GameManagerOne.Instance.noColor = RandomUtil.Chance(0.5f);
                break;
            case "skin":
                GameManagerOne.Instance.activeColor = colorSkinN.RandomItem<string>();
                GameManagerOne.Instance.noColor = RandomUtil.Chance(0.5f);
                break;
        }
        GameManagerOne.Instance.ruleText(GameObject.FindGameObjectWithTag("ruleText").GetComponent<Text>());
    }

    public void randomLevelN()
    {
        bool previousLapsus = GameManagerOne.Instance.lapsus;

        switch (countLevel)
        {
            case 1:
                GameManagerOne.Instance.randomTender = Random.Range(5,9);
                break;
            case 2:
                GameManagerOne.Instance.randomTender = Random.Range(4, 7);
                break;
            case 3:
                GameManagerOne.Instance.randomTender = Random.Range(3, 5);
                GameManagerOne.Instance.lapsus = RandomUtil.Chance(0.5f);
                if(!previousLapsus && GameManagerOne.Instance.lapsus)
                {
                    AudioManager.Instance.PlaySoundEffect(effectLapsus);
                }
                break;
            default:
                GameManagerOne.Instance.randomTender = Random.Range(2, 4);
                GameManagerOne.Instance.lapsus = RandomUtil.Chance(0.5f);
                if (!previousLapsus && GameManagerOne.Instance.lapsus)
                {
                    AudioManager.Instance.PlaySoundEffect(effectLapsus);
                }
                break;
        }

        if (previousLapsus != GameManagerOne.Instance.lapsus)
            GameManagerOne.Instance.OnLapsusChanged(GameManagerOne.Instance.lapsus);
    }

    public void ruleText(Text t1)
    {
        if (t1 == null)
            return;

        if (activeRule!=null)
        {
            t1.text = GameManagerOne.Instance.noColor ? "<color='#FE272EFF'>NO</color> " : "<color='#3FEB34FF'>ONLY</color> ";
            t1.text += GameManagerOne.Instance.activeColor.ToUpper();

            switch (GameManagerOne.Instance.activeRule)
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
        AudioManager.Instance.PlaySoundEffect(effectFlirtOver);
        GameManagerOne.instance.gameOverB = true;
        GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>().text = "Score: " + score;
        float time = 0.0f, totalTime = 1.0f;
        while (time < totalTime)
        {
            time += Time.deltaTime;
            GameObject.FindGameObjectWithTag("gOScreen").GetComponent<CanvasGroup>().alpha = time;
            yield return null;
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
