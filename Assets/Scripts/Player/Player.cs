using System.Collections.Generic;
using UnityEngine;

public class Player : StationObject {
	public float Health = 100;
	[SerializeField]
	Sprite[] spritesheet;
	[SerializeField]
	new Camera camera;
	[HideInInspector]
	public Inventory Inventory;

	SpriteRenderer spriteRenderer;
	int maskTile;
	Vector2 targetPos;
	float speed = 0.2f;
	[SerializeField]
	float Speed {
		get {return speed;}
		set {
			if (value < 0) {
				Speed = 0;
			} else {
				speed = value;
			}
		}
	}
	[SerializeField]
	float runSpeedBonus = 2.0f;
	bool isRunning = true;

	public static readonly string[] InventorySlotNames = {
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
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		Inventory = gameObject.GetComponent<Inventory>();
		Inventory.Init(InventorySlotNames);
		Inventory.ActiveSlotName = "HandRight";
		maskTile = LayerMask.GetMask("Tile");
		targetPos = transform.position;
	}

	/// <summary>
	/// Set move target if it is possible to move to it.
	/// </summary>
	/// <param name="xdir">Xdir.</param>
	/// <param name="ydir">Ydir.</param>
	void SetMoveTarget(float xdir, float ydir) {
		Collider2D result = Physics2D.OverlapCircle(
			                    new Vector2(transform.position.x + xdir,
			                                transform.position.y + ydir),
			                    0.0f,
			                    maskTile);
		if (result != null) {
			if (!result.gameObject.GetComponent<Tile>().HasObstacle) {
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
				SetMoveTarget(xdir, ydir);
			}
		} else {
			transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed);
		}
		RotatePlayer(xdir, ydir);

		// Mouse actions
		if (Input.GetButtonUp("PrimaryButton")) {
			Vector2 mousePos = Utility.RoundVector(camera.ScreenToWorldPoint(Input.mousePosition));
			Vector2 relativeMousePos = mousePos - (Vector2)transform.position;
			// Limit range to 1 tile
			bool pickable = (Mathf.Abs(relativeMousePos.x) <= 1) && (Mathf.Abs(relativeMousePos.y) <= 1);
			
			List<StationObject> tileContents = new List<StationObject>(0);
			Collider2D result = Physics2D.OverlapCircle(
				                    mousePos,
				                    0.0f,
				                    maskTile);

			if (result != null) {
				tileContents = result.gameObject.GetComponent<Tile>().Contents;
			}

			// Pick item up
			// TODO: Pick the topmost item first
			foreach (var stationObject in tileContents) {
				if (stationObject.IsPickable && Inventory.ActiveSlot.IsEmpty() && pickable) {
					Inventory.ActiveSlot.Item = ((DroppedItem)stationObject).Item;
					Destroy(stationObject.gameObject);
					break;
				}
			}
		}
	}

	void RotatePlayer(float xdir, float ydir) {
		if (ydir > 0) {
			spriteRenderer.sprite = spritesheet[1];
		} else if (ydir < 0) {
			spriteRenderer.sprite = spritesheet[3];
		} else if (xdir > 0) {
			spriteRenderer.sprite = spritesheet[0];
		} else if (xdir < 0) {
			spriteRenderer.sprite = spritesheet[2];
		}
	}

	public void SwapHands() {
		if (Inventory.ActiveSlotName == "HandRight") {
			Inventory.ActiveSlotName = "HandLeft";
		} else {
			Inventory.ActiveSlotName = "HandRight";
		}
	}

	public void ToggleRunning() {
		if (isRunning) {
			Speed /= runSpeedBonus;
		} else {
			Speed *= runSpeedBonus;
		}
		isRunning = !isRunning;
	}

	public void DropItem() {
		if (!Inventory.ActiveSlot.IsEmpty()) {
			Instantiate(ItemDatabase.Instance.GetDroppedPrefab(Inventory.ActiveSlot.Item),
			            Utility.RoundVector(transform.position),
			            Quaternion.identity);
			Inventory.ActiveSlot.Empty();
		}
	}
}
