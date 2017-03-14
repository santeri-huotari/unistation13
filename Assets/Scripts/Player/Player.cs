using System.Collections.Generic;
using UnityEngine;

public class Player : StationObject {
	public float health = 100;
	public float speed = 300;
	public Sprite[] spritesheet;
	new public Camera camera;
	public Inventory inventory;
	

	Rigidbody2D rb;
	SpriteRenderer sr;
	int maskTile;

	public static string[] inventorySlotNames = {
		"HandLeft",
		"HandRight",
		"PocketLeft",
		"PocketRight",
		"Backpack",
		"Belt",
		"Id",
		"SuitStorage",
		"Shoes",
		"InternalClothing",
		"ExternalClothing",
		"Gloves",
		"Neck",
		"Mask",
		"Ears",
		"Eyes",
		"Head",
	};

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		sr = gameObject.GetComponent<SpriteRenderer>();
		inventory = gameObject.GetComponent<Inventory>();
		inventory.init(inventorySlotNames);
		inventory.activeSlotName = "HandRight";
		maskTile = LayerMask.GetMask("Tile");
	}
	
	// Update is called once per frame
	void Update () {// Movement
		float xdir = Input.GetAxisRaw("Horizontal");
		float ydir = Input.GetAxisRaw("Vertical");
		rb.velocity = new Vector2(speed * xdir * Time.deltaTime, speed * ydir * Time.deltaTime);

		// Rotate player
		if (ydir > 0)
			sr.sprite = spritesheet[1];
		else if (ydir < 0)
			sr.sprite = spritesheet[3];
		else if (xdir > 0)
			sr.sprite = spritesheet[0];
		else if (xdir < 0)
			sr.sprite = spritesheet[2];


		// Mouse actions
		if (Input.GetButtonUp("PrimaryButton")) {
			Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 relativeMousePos = mousePos - (Vector2)transform.position;
			float x = Mathf.Round(relativeMousePos.x);
			float y = Mathf.Round(relativeMousePos.y);
			// Limit range to 1 tile
			bool pickable = (Mathf.Abs(x) <= 1) && (Mathf.Abs(y) <= 1);
			
			List<StationObject> tileContents = new List<StationObject>(0);
			Collider2D result = Physics2D.OverlapCircle(
				                    mousePos,
				                    0.0f,
				                    maskTile);

			if (result != null) {
				tileContents = result.gameObject.GetComponent<Tile>().contents;
			}

			// Pick item up
			// TODO: Pick the topmost item first
			foreach (var stationObject in tileContents) {
				if (stationObject.isPickable && inventory.activeSlot.isEmpty() && pickable) {
					inventory.activeSlot.item = ((DroppedItem)stationObject).item;
					Destroy(stationObject.gameObject);
					break;
				}
			}
		}
	}

	void swapHands() {
		if (inventory.activeSlotName == "HandRight") {
			inventory.activeSlotName = "HandLeft";
		} else {
			inventory.activeSlotName = "HandRight";
		}
	}

	public void dropItem() {
		if (!inventory.activeSlot.isEmpty()) {
			Instantiate(ItemDatabase.instance.getDroppedPrefab(inventory.activeSlot.item),
			            transform.position,
			            Quaternion.identity);
			inventory.activeSlot.empty();
		}
	}
}
