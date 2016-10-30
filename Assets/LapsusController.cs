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

    private Image image;

    private void Start()
    {
        this.image = this.GetComponent<Image>();
        GameManagerOne.Instance.OnLapsusChanged += OnLapsusChanged;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManagerOne.Instance.OnLapsusChanged -= OnLapsusChanged;
    }

    private void OnLapsusChanged(bool currentLapsusStatus)
    {
        if (currentLapsusStatus)
            Show();
        else
            Hide();
    }

    public void Show()
    {
        if (this.image != null)
        {
            if (GameManagerOne.Instance.PlayerIsHomosexual)
                this.image.sprite = this.heteroSprite;
            else
                this.image.sprite = this.homoSprite;
        }

        gameObject.SetActive(true);

        StartCoroutine(this.ShowCorroutine());
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
