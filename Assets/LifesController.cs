using UnityEngine;
using System.Collections;

public class LifesController : MonoBehaviour {

    private void Start()
    {
        GameManagerOne.Instance.OnLifesChanged += this.SetActiveHearts;
    }

    private void OnDestroy()
    {
        GameManagerOne.Instance.OnLifesChanged -= this.SetActiveHearts;
    }

    public void SetActiveHearts(int count)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(i < count);
        }
    }

}
