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

        GameManagerOne.Instance.OnRuleChanged += Instance_OnRuleChanged;
	}

    private void OnDestroy()
    {
        GameManagerOne.Instance.OnRuleChanged -= Instance_OnRuleChanged;
    }

    private void Instance_OnRuleChanged()
    {
        this.PauseForSeconds(1.0f);
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

    public void PauseForSeconds(float seconds)
    {
        this.StartCoroutine(this.PauseCorroutine(seconds));
    }

    private IEnumerator PauseCorroutine(float seconds)
    {
        this.paused = true;
        float time = 0.0f;
        while (time < seconds)
        {
            time += Time.deltaTime;
            yield return null;
        }
        this.paused = false;
    }

}
