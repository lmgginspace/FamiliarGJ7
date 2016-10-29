using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Extensions.System.Colections;

public class ProfilePhoto : MonoBehaviour {

    [HideInInspector] float offsetX;
    [HideInInspector] float initialX;
    [HideInInspector] float initialY;

   [HideInInspector] bool draggable;

    [HideInInspector] string colorF;
    [HideInInspector] string partF;

    public List<GameObject> listProf;

    

    // Use this for initialization
    void Start () {
        initialX = transform.position.x;
        initialY = transform.position.y;
        draggable = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        offsetX = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y, 21f)).x;
    }

    void OnMouseDrag()
    {
        if (draggable)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y, 21f));
            transform.position = new Vector3(transform.position.x + offsetX, initialY, 0.0f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
    }

    void OnMouseUp()
    {
        transform.position = new Vector3(initialX, initialY, 0.0f);
        draggable = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        draggable = false;
        bool bolAccepted = other.gameObject.name == "Accept" ? true : false;
        GameManagerOne.Instance.checkFlirt(GameObject.FindGameObjectWithTag("profile").GetComponent<profileClass>(), bolAccepted);
        CountDown countdown = FindObjectOfType(typeof(CountDown)) as CountDown;
        countdown.restart();
        Destroy(GameObject.FindGameObjectWithTag("profile"));
        transform.position = new Vector3(initialX, initialY, 0.0f);
        GameObject prof1 = Instantiate(listProf.RandomItem<GameObject>(),transform.position,Quaternion.identity) as GameObject;
        prof1.transform.SetParent(this.transform, false);
        // TODO: Llamar renueva profile
    }
}
