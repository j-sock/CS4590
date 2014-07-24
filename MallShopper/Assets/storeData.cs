using UnityEngine;
using System.Collections.Generic;

public class storeData : MonoBehaviour {
	
	//element 0 is if the item is at the store
	//element 1 is your demographic rating
	//element 2 is the item's percent sale
	//element 3 is the price of the item

	public double[] tShirt = new double[4];
	public double[] blouse = new double[4];
	public double[] jeans = new double[4];
	public double[] sneakers = new double[4];

	//element 0 is if an item is at the store
	//element 1 is your demographic rating (-1 to 1)
	//element 2 is the sale rating (0 - none, 3 - 50, 5 - 100)
	//element 3 is the price rating (1 - $10 or less, 5 - $100+)
	public double[] all = new double[4];

}