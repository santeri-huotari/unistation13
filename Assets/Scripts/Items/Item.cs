using UnityEngine;

[CreateAssetMenu(menuName = "Items/Generic Item")]
public class Item : ScriptableObject {
	public ItemID Type;
	public Sprite Sprite;
	public int StackSize;
	public int Size;
}

public enum ItemID {
	Rods
}
