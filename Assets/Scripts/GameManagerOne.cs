using UnityEngine;
using System.Collections;

public class GameManagerOne : MonoBehaviour {

    private static GameManagerOne instance = null;

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
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
