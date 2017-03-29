public class ItemSlot {
	Item item;
	public Item Item {
		get {return item;}
		set {
			if (value != null && value.Size <= this.size) {
				item = value;
			}
		}
	}

	int size = 1;

	public bool IsEmpty() {
		return Item == null;
	}

	/// <summary>
	/// Remove the item from this slot.
	/// </summary>
	public void Empty() {
		item = null;
	}
}
