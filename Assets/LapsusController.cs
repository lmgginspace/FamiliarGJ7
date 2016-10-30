using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LapsusController : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve showCurve;
    [SerializeField]
    private AnimationCurve hideCurve;

    [SerializeField]
    private float showTime;
    [SerializeField]
    private float hideTime;

    [SerializeField]
    private Sprite homoSprite;

    [SerializeField]
    private Sprite heteroSprite;

    private void Start()
    {
        GameManagerOne.Instance.OnLapsusChanged += this.OnLapsusChanged;
        this.gameObject.SetActive(false);
    }

    private void OnLapsusChanged(bool currentLapsusStatus)
    {
        if (currentLapsusStatus)
            this.Show();
        else
            this.Hide();
    }

    public void Show()
    {
        Image sr = this.GetComponent<Image>();
        if (sr != null)
        {
            if (GameManagerOne.Instance.PlayerIsHomosexual)
                sr.sprite = this.heteroSprite;
            else
                sr.sprite = this.homoSprite;
        }

        this.gameObject.SetActive(true);

        this.StartCoroutine(this.ShowCorroutine());
	}
	
	public void Hide()
    {
        this.StartCoroutine(this.HideCorroutine());
    }

    private IEnumerator ShowCorroutine()
    {
        float time = 0.0f, inverseTotalTime = 1.0f / this.showTime;
        while (time < 1.0f)
        {
            this.transform.localScale = Vector3.one * this.showCurve.Evaluate(time);

            time += Time.deltaTime * inverseTotalTime;
            yield return null;
        }
        this.transform.localScale = Vector3.one;
    }

    private IEnumerator HideCorroutine()
    {
        float time = 0.0f, inverseTotalTime = 1.0f / this.hideTime;
        while (time < 1.0f)
        {
            this.transform.localScale = Vector3.one * this.hideCurve.Evaluate(time);

            time += Time.deltaTime * inverseTotalTime;
            yield return null;
        }
        this.gameObject.SetActive(false);
    }

}
