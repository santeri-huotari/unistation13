using System.Collections.Generic;
using UnityEngine;

public class Player : StationObject {
	public float health = 100;
	public Sprite[] spritesheet;
	new public Camera camera;
	public Inventory inventory;

	SpriteRenderer sr;
	int maskTile;
	Vector2 targetPos;
	float _speed = 0.2f;
	[SerializeField]
	float speed {
		get {return _speed;}
		set {
			if (value < 0) {
				speed = 0;
			} else {
				_speed = value;
			}
		}
	}
	[SerializeField]
	float runSpeedBonus = 2.0f;
	bool running = true;

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

	void Start() {
		sr = gameObject.GetComponent<SpriteRenderer>();
		inventory = gameObject.GetComponent<Inventory>();
		inventory.init(inventorySlotNames);
		inventory.activeSlotName = "HandRight";
		maskTile = LayerMask.GetMask("Tile");
		targetPos = transform.position;
	}

	// Set move target if it is possible to move to it.
	void setMoveTarget(float xdir, float ydir) {
		Collider2D result = Physics2D.OverlapCircle(
			                    new Vector2(transform.position.x + xdir,
			                                transform.position.y + ydir),
			                    0.0f,
			                    maskTile);
		if (result != null) {
			if (!result.gameObject.GetComponent<Tile>().hasObstacle) {
				targetPos.x += xdir;
				targetPos.y += ydir;
			}
		} else {
			targetPos.x += xdir;
			targetPos.y += ydir;
		}
	}

	void Update() {
		// Movement
		float xdir = Input.GetAxisRaw("Horizontal");
		float ydir = Input.GetAxisRaw("Vertical");
		if ((Vector2)transform.position == targetPos) {
			if (xdir != 0 || ydir != 0) {
				setMoveTarget(xdir, ydir);
			}
		} else {
			transform.position = Vector2.MoveTowards(transform.position, targetPos, speed);
		}
		rotatePlayer(xdir, ydir);

		// Mouse actions
		if (Input.GetButtonUp("PrimaryButton")) {
			Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
			mousePos.x = Mathf.Round(mousePos.x);
			mousePos.y = Mathf.Round(mousePos.y);
			Vector2 relativeMousePos = mousePos - (Vector2)transform.position;
			// Limit range to 1 tile
			bool pickable = (Mathf.Abs(relativeMousePos.x) <= 1) && (Mathf.Abs(relativeMousePos.y) <= 1);
			
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

	void rotatePlayer(float xdir, float ydir) {
		if (ydir > 0) {
			sr.sprite = spritesheet[1];
		} else if (ydir < 0) {
			sr.sprite = spritesheet[3];
		} else if (xdir > 0) {
			sr.sprite = spritesheet[0];
		} else if (xdir < 0) {
			sr.sprite = spritesheet[2];
		}
	}

	void swapHands() {
		if (inventory.activeSlotName == "HandRight") {
			inventory.activeSlotName = "HandLeft";
		} else {
			inventory.activeSlotName = "HandRight";
		}
	}

	public void toggleRunning() {
		if (running) {
			speed /= runSpeedBonus;
		} else {
			speed *= runSpeedBonus;
		}
		running = !running;
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
