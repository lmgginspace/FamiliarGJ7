using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CountDown : MonoBehaviour {

    public Image currentTimeBar;
    public Text timeText;

    private float currentTime;
    public float maxTime;
    private bool paused;

	// Use this for initialization
	void Start () {
        currentTime = maxTime;
        paused = false;
        timeText.text = maxTime.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                currentTime = 0;
                paused = true;
                // TODO: Llamar GameOver
            }
            float ratio = currentTime / maxTime;

            currentTimeBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
            timeText.text = (currentTime * 100).ToString("0");
        }
	}

    public void restart()
    {
        maxTime -= maxTime * 0.1f * maxTime * 0.2f;
        currentTime = maxTime;
        paused = false;
        timeText.text = maxTime.ToString();
    }
}
