using UnityEngine;
using System.Collections;

public class Item {

	public Transform transform;
	public int price;
	public string store;
	public string demo;
	public Item(Transform t, int price, string store) {
		this.transform = t;
		this.price = price;
		this.store = store;
	}
}
