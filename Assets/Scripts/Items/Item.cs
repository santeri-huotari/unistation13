using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Generic Item")]
public class Item : ScriptableObject {
	public ItemID type;
	public Sprite sprite;
	public int stackSize;
	public int size;
}

public enum ItemID {
	Rods
}
