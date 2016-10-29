using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rulesClass : MonoBehaviour {

    public List<string> colorEyeN;
    public List<string> colorHairN;
    public List<string> colorSkinN;

    private string wFace;
    private string color;

    private bool switchLike=false;

    private int tenderCount=0;
    private int randomTender;
    private int countLevel;


    private string playerG;
    private string playerL;

    private bool checkRN;


    // Use this for initialization
    void Start () {
        playerG=GameManagerOne.Instance.playerGender;
        playerL = GameManagerOne.Instance.playerLike;
        randomTender = Random.Range(5, 10);
    }
	
	// Update is called once per frame
	void Update () {

        

	}

   public void rules(int rInt,string colorRule,string colorProfile)
    {
        bool checkR=false;

        switch (rInt)
        {
           case 0:
                if (colorRule.Equals(colorProfile))
                {
                    checkR = true;
                }
                     break;
            case 1:
                if (colorRule!=colorProfile)
                {
                    checkR = true;
                }
                    break;
        }
        checkRN = checkR;
    }




    public bool checkRule(string selectColor,string genProfile)
    {
        bool chR = false;
        tenderCount++;
        if (switchLike)
        {
            if (playerL!=genProfile)
            {
                chR = true;
            }
        }
        else
        {
            if (playerL==genProfile)
            {
                chR = true;
            }
        }

        chR = checkRN;

        if (genProfile.Equals("orc"))
        {
            chR = true;
        }
        else if (genProfile.Equals("cake"))
        {
            chR = true;
        }
        return chR;
    }

    public void reset()
    {
        tenderCount = 0;
        countLevel = 0;
    }
    
}    

