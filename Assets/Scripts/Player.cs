using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 300;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		// Movement
		float xdir = Input.GetAxisRaw("Horizontal");
		float ydir = Input.GetAxisRaw("Vertical");
		rb.velocity = new Vector2(speed * xdir * Time.deltaTime, speed *  ydir * Time.deltaTime);
	}
}
