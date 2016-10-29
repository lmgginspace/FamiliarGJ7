using UnityEngine;
using System.Collections;

public class ProfilePhoto : MonoBehaviour {

    float offsetX;
    float initialX;
    float initialY;

    bool draggable;

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
        CountDown countdown = FindObjectOfType(typeof(CountDown)) as CountDown;
        countdown.restart();
        transform.position = new Vector3(initialX, initialY, 0.0f);
        // TODO: Llamar renueva profile
    }
}
