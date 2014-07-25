using UnityEngine;
using System.Collections.Generic;

public class GUI : MonoBehaviour {

	
	public GameObject floorText;
	public GameObject moneyText;
	public GameObject listText;
	public GameObject headerText;
	public GameObject background;
	public GameObject map1;
	public GameObject map2;
	
	public List<GameObject> items;
	
<<<<<<< HEAD
	private TrackingSound1Script tracker;
=======
	//private ItemTracker tracker;
>>>>>>> 6320046259525719882592a9b393fd8d58663657
	
	private int floor;
	private bool show;
	private bool display; //true = map, false = list
	
	private double moneySpent;
	private int listIndex;
	private int mapIndex;
	
	void Start() {
		//tracker = GetComponent<ItemTracker>();
	}
	
	void Update() {
		if(show) {
			if(Input.GetKeyDown("e")) {
				if(!display) {
					listIndex++;
					if(listIndex >= items.Count) listIndex = 0;
					//tracker.startTracking(items[listIndex].transform);
				}
			}
			if(Input.GetKeyDown("q")) {
				if(!display) {
					listIndex--;
					if(listIndex < 0) listIndex = items.Count-1;
					//tracker.startTracking(items[listIndex].transform);
				}
			}
			if(display) {
				if(floor == 1) {
					map2.SetActive(false);
					map1.SetActive(true);
				} else if(floor == 2) {
					map1.SetActive(false);
					map2.SetActive(true);
				}
				headerText.guiText.text = "Map";
			} else {
				listText.guiText.text = items[listIndex].name;
				headerText.guiText.text = "List";
			}
			floorText.guiText.text = "Floor " + floor.ToString();
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
			//tracker.startTracking(items[listIndex]);
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
		//tracker.mute();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "secondFloor") {
			floor = 2;
		}
		else if (other.name == "firstFloor") {
			floor = 1;
		}
	}
}
