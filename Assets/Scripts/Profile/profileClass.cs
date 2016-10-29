using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Extensions.System.Colections;



public class profileClass : MonoBehaviour {

    public string gender;

    [SerializeField] private SpriteRenderer head;
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private SpriteRenderer nose;
    [SerializeField] private SpriteRenderer mouth;
    public SpriteRenderer backGround;

    [SerializeField] private List<SpriteRenderer> eyes;
    [SerializeField] private List<SpriteRenderer> iris;
    [SerializeField] private List<SpriteRenderer> eyebrows;

    [SerializeField] private List<SpriteRenderer> skinAll;
    [SerializeField] private List<SpriteRenderer> hairAll;
   
    [Header("Colores")]
    [SerializeField] private List<colorName> eyesColor;
    [SerializeField] private List<colorName> skinColor;
    [SerializeField] private List<colorName> hairColor;

    [SerializeField] private List<Sprite> headS;
    [SerializeField] private List<Sprite> hairS;
    [SerializeField] private List<Sprite> noseS;
    [SerializeField] private List<Sprite> mouthS;
    [SerializeField] private List<Sprite> eyesS;
    [SerializeField] private List<Sprite> eyebrowsS;
    

    [HideInInspector] public string hairNC;
    [HideInInspector] public string skinNC;
    [HideInInspector] public string eyeNC;

    void Awake()
    {
        randomP();

    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void randomP()
    {
        #region Colorchange

        colorName eyeC = eyesColor.RandomItem<colorName>();

        eyeNC = eyeC.nameColor;

        foreach (var item in iris)
        {
            item.color = eyeC.cSolid;
        }

        colorName skinC = skinColor.RandomItem<colorName>();

        skinNC = skinC.nameColor;

        foreach (var item in skinAll)
        {
            item.color = skinC.cSolid;
        }

        colorName hairC = hairColor.RandomItem<colorName>();

        hairNC = hairC.nameColor;

        foreach (var item in hairAll)
        {
            item.color = hairC.cSolid;
        }
        #endregion

        #region RandomSprites

        head.sprite = headS.RandomItem<Sprite>();

        hair.sprite = hairS.RandomItem<Sprite>();

        nose.sprite = noseS.RandomItem<Sprite>();

        mouth.sprite = mouthS.RandomItem<Sprite>();

        Sprite eyeCS = eyesS.RandomItem<Sprite>();

        foreach (var item in eyes)
        {
            item.sprite = eyeCS;
        }

        Sprite eyebrowsCS = eyebrowsS.RandomItem<Sprite>();

        foreach (var item in eyebrows)
        {
            item.sprite = eyebrowsCS;
        }

        #endregion
    }

}
