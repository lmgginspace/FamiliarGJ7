using UnityEngine;
using System.Collections;

public class RuleTextController : MonoBehaviour
{
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float animationTime = 1.0f;

    private void Start()
    {
        GameManagerOne.Instance.OnRuleChanged += this.Animate;
    }

    private void OnDestroy()
    {
        GameManagerOne.Instance.OnRuleChanged -= this.Animate;
    }

    public void Animate()
    {
        this.StartCoroutine(this.AnimateCorroutine());
	}

    private IEnumerator AnimateCorroutine()
    {
        float time = 0.0f, inverseTotalTime = 1.0f / this.animationTime;
        while (time < 1.0f)
        {
            this.transform.localScale = Vector3.one * this.scaleCurve.Evaluate(time);

            time += Time.deltaTime * inverseTotalTime;
            yield return null;
        }
        this.transform.localScale = Vector3.one;
    }

}