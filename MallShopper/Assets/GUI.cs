using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

public class GUI : MonoBehaviour {

	private ItemTracker tracker;
	
	public TextAsset jsonString;
	
	public GameObject floorText;
	public GameObject moneyText;
	public GameObject listText;
	public GameObject headerText;
	public GameObject background;
	public GameObject map1;
	public GameObject map2;
	
	public string demo;
	public float priceCap;
		
	public List<GameObject> categories;
	private GameObject currentCategory;
	private Item currentItem;
	private int listIndex;
	
	private bool show;
	private bool display; //true = map, false = list	
	private double moneySpent;
	
	void Start() {
		tracker = GetComponent<ItemTracker>();
		currentCategory = categories[0];
		currentItem = pickItem(currentCategory);
	}
	
	void Update() {
		if(show) {
			if(Input.GetKeyDown("e")) {
				scroll (1);
			}
			if(Input.GetKeyDown("q")) {
				scroll(0);
			}
			if(display) {
				if(getFloor(transform) == 1) {
					map2.SetActive(false);
					map1.SetActive(true);
				} else {
					map1.SetActive(false);
					map2.SetActive(true);
				}
				headerText.guiText.text = "Map";
			} else {
				if(currentCategory != null) listText.guiText.text = currentCategory.name;
				headerText.guiText.text = "List";
			}
			floorText.guiText.text = "Floor " + getFloor(transform).ToString();
			moneyText.guiText.text = "$" + moneySpent.ToString();
		}
		if(Input.GetKeyDown("f")) {
			show = !show;
			showUI(show, display);
		}
		if(Input.GetKeyDown("z")) {
			display = !display;
			hide();
			showUI(show, display);
		}
	}
	
	void scroll(int s) {
		if(display) return;
		switch(s) {
		case 0:
			listIndex--;
			if(listIndex < 0) listIndex = categories.Count-1;
			currentCategory = categories[listIndex];
			currentItem = pickItem(currentCategory);
			tracker.startTracking(currentItem);	
			break;
		case 1:
			listIndex++;
			if(listIndex >= categories.Count) listIndex = 0;
			currentCategory = categories[listIndex];
			currentItem = pickItem(currentCategory);					
			tracker.startTracking(currentItem);
			break;
		}
	}
	
	void showUI(bool show, bool type) {
		if(!show) {
			hide();
			return;
		}
		if(type) {
			map1.SetActive(true);
			map2.SetActive(true);
		} else {
			listText.SetActive(true);
			tracker.startTracking(currentItem);
		}
		floorText.SetActive(true);
		moneyText.SetActive(true);
		headerText.SetActive(true);
		background.SetActive(true);
	}
	
	void hide() {
		floorText.SetActive(false);
		moneyText.SetActive(false);
		listText.SetActive(false);
		headerText.SetActive(false);
		background.SetActive(false);
		map1.SetActive(false);
		map2.SetActive(false);
		tracker.mute();
	}
	
	public void buyItem(Item i) {
		categories.Remove(currentCategory);
		moneySpent += i.price;
		i.transform.gameObject.SetActive(false);
		scroll(1);
	}
	
	Item pickItem(GameObject category) {
		var json = JSONNode.Parse(jsonString.text);		
		string store = "";
		int min = int.MaxValue;
		for(int i=0; i<2; i++) {
			int p = int.Parse(json[category.name][i]["price"]);
			string d = json[category.name][i]["demo"];
			if(d.Equals(demo) && p < min && p <= priceCap) {
				min = p;
				store = json[category.name][i]["store"];
			}
		}
		Transform t = category.transform.Find(store);
		return new Item(t, min, store);
	}
	
	int getFloor(Transform t) {
		if(t.position.y < 6) return 1;
		return 2;
	}
}
