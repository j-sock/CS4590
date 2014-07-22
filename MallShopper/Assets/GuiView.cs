using UnityEngine;
using System.Collections;

public class GuiView : MonoBehaviour {

	public GameObject background;
	public GameObject money;
	public GameObject header;
	public GameObject text;
	public GameObject scroll;
	public GameObject topFloor;
	public GameObject bottomFloor;

	private int timer;

	enum Screen { 
		None, 
		Money, 
		Option1, 
		Option2,
		Option3,
		List1, 
		List2, 
		Map1, 
		Map2,
		Sounds
	}

	private int state;
	private int prevState;

	private ArrayList myItems;
	private string[] categories = new string[] { 
		"Accessories",
		"Bath and Beauty",
		"Children's Apparel",
		"Electronics",
		"Entertainment",
		"Kitchen and Home",
		"Men's Apparel",
		"Shoes",
		"Sports and Fitness",
		"Women's Apparel"
	};
	private string[] stores = new string[] { 
		"Aberchombie & Fitch",
		"American Eagle",
		"Banana Republic",
		"Claire's",
		"Hot Topic",
		"J.Crew",
		"Macy's",
		"Payless ShoeSource",
		"Yankee Candle"
	};

	private int scrollPos;
	private int prevScrollPos;

	void Start() {
		timer = 0;
		state = (int)Screen.None;
		prevState = (int)Screen.Money;
		myItems = new ArrayList();
		scrollPos = 0;
		prevScrollPos = 0;
		scroll.guiText.text = "+\nll\n+";
	}

	void Update() {
		timer++;
		if (Input.GetKeyDown("f"))
			toggleScreen();
		if (Input.GetKeyDown("e"))
			flipRight();
		else if (Input.GetKeyDown("q"))
			flipLeft();
		if (Input.GetKeyDown("z")) 
			scrollDown();
		else if (Input.GetKeyDown("x"))
			scrollUp();
	}

	private void scrollDown() {
		if (state == (int)Screen.List1 &&
			scrollPos <= myItems.Count-1) {
				scrollPos++;
				writeList1();
			}
		else if (state == (int)Screen.List2 &&
			scrollPos < categories.Length-1) {
				scrollPos++;
				writeList2();
			}
		else if (state == (int)Screen.Map2 &&
			scrollPos < stores.Length-1) {
				scrollPos++; 
				writeList3();
			}
	}

	private void scrollUp() {
		if (state == (int)Screen.List1 &&
			scrollPos > 0) {
				scrollPos--;
				writeList1();
			}
		else if (state == (int)Screen.List2 &&
			scrollPos > 0) {
				scrollPos--;
				writeList2();
			}
		else if (state == (int)Screen.Map2 &&
			scrollPos > 0) {
				scrollPos--; 
				writeList3();
			}
	}

	private void flipRight() {
		//menu
		if (state == (int)Screen.Money) {
			text.guiText.text = "List";
			prevState = state;
			state = (int)Screen.Option1;
			scrollPos = 0;
		}
		else if (state == (int)Screen.Option1) {
			text.guiText.text = "Map";
			prevState = state;
			state = (int)Screen.Option2;
		}
		else if (state == (int)Screen.Option2) {
			text.guiText.text = "Sounds";
			prevState = state;
			state = (int)Screen.Option3;
		}
		//list
		else if (state == (int)Screen.List1) {
			writeList2();
			prevState = state;
			state = (int)Screen.List2;
			scrollPos = 0;
		}
		//map
		else if (state == (int)Screen.Map1) {
			bottomFloor.SetActive(false);
			topFloor.SetActive(false);
			writeList3();
			prevState = state;
			state = (int)Screen.Map2;
			scrollPos = 0;
		}
	}

	private void flipLeft() {
		//menu
		if (state == (int)Screen.Option1) {
			text.guiText.text = "";
			prevState = state;
			state = (int)Screen.Money;
		}
		else if (state == (int)Screen.Option2) {
			text.guiText.text = "List";
			prevState = state;
			state = (int)Screen.Option1;
		}
		else if (state == (int)Screen.Option3) {
			text.guiText.text = "Map";
			header.guiText.text = "";
			prevState = state;
			state = (int)Screen.Option2;
		}
		//list
		else if (state == (int)Screen.List1) {
			text.guiText.text = "";
			header.guiText.text = "";
			showAll();
			prevState = state;
			state = (int)Screen.Money;
			scrollPos = 0;
		}
		else if (state == (int)Screen.List2) {
			header.guiText.text = "Your List";
			writeList1();
			prevState = state;
			state = (int)Screen.List1;
			scrollPos = 0;
		}
		//map
		else if (state == (int)Screen.Map2) {
			showMap();
			text.guiText.text = "";
			prevState = state;
			state = (int)Screen.Map1;
		}
		else if (state == (int)Screen.Map1) {
			text.guiText.text = "";
			header.guiText.text = "";
			showAll();
			prevState = state;
			state = (int)Screen.Money;
			scrollPos = 0;
		}
	}

	private void writeList1() {
		header.guiText.text = "Your List";
		scroll.SetActive(true);
		if (myItems.Count == 0)
			text.guiText.text = "Add Items";
		else {
			string t = "";
			int start = scrollPos - 1;
			for(int i = start; i < start + 3; i++)
				if (i == myItems.Count)
					t += "Add Items";
				else if (i < myItems.Count && i >= 0)
					t += myItems[i] + "\n";
				else t += "\n";
			text.guiText.text = t;
		}
	}

	private void writeList2() {
		header.guiText.text = "Add Items";
		scroll.SetActive(true);
		string t = "";
		int start = scrollPos - 1;
		for(int i = start; i < start + 3; i++)
			if (i < categories.Length && i >= 0)
				t += categories[i] + "\n";
			else t += "\n";
		text.guiText.text = t;
	}

	private void writeList3() {
		header.guiText.text = "Directory";
		scroll.SetActive(true);
		string t = "";
		int start = scrollPos - 1;
		for(int i = start; i < start + 3; i++)
			if (i < stores.Length && i >= 0)
				t += stores[i] + "\n";
			else t += "\n";
		text.guiText.text = t;
	}

	private void showMap() {
		//CHANGE ME ONCE THE MAP IS PUT BACK TOGETHER
		if (transform.position.y < 0) {
			header.guiText.text = "Floor 1";
			bottomFloor.SetActive(true);
			topFloor.SetActive(false);
		} else {
			header.guiText.text = "Floor 2";
			topFloor.SetActive(true);
			bottomFloor.SetActive(false);
		}
		text.guiText.text = "";
	}

	private void toggleScreen() {
		//screen off
		if (state == (int)Screen.None) {
			if (prevState == (int)Screen.Money) {
				header.guiText.text = "";
				text.guiText.text = "";
				showAll();
			}
			else showAll();
			state = prevState;
			prevState = (int)Screen.None;
		} else { //screen on
			if (timer < 30) { //double tap turns off
				showNothing();
				prevState = state;
				state = (int)Screen.None;
			} else { //single tap selects
				//go to list screen
				if (state == (int)Screen.Option1) {
					writeList1();
					showAll();
					prevState = state;
					state = (int)Screen.List1;
					scrollPos = 0;
					prevScrollPos = 0;
				} 
				//go to map screen
				else if (state == (int)Screen.Option2) {
					showMap();
					prevState = state;
					state = (int)Screen.Map1;
				}
				//go to sound options
				else if (state == (int)Screen.Option3) {
					header.guiText.text = "Sound Options";
					text.guiText.text = "Not Done :)";
				}
				//select an item to remove from list
				else if (state == (int)Screen.List1) {
					if (scrollPos < myItems.Count) {
						myItems.RemoveAt(scrollPos);
						Debug.Log("Removed item at "+scrollPos);
						scrollPos--;
						writeList1();
					}
					else if (scrollPos == myItems.Count) {
						scrollPos = 0;
						writeList2();
						prevState = state;
						state = (int)Screen.List2;						
					}
				}
				//select an item to put in list
				else if (state == (int)Screen.List2) {
					if (!myItems.Contains(categories[scrollPos])) {
						myItems.Add(categories[scrollPos]);
						header.guiText.text = "Your List";
						scrollPos = 0;
						writeList1();
						prevState = state;
						state = (int)Screen.List1;
					}
				}
			}
			timer = 0;
		}
	}

	private void showNothing() {
		background.SetActive(false);
		money.SetActive(false);
		header.SetActive(false);
		text.SetActive(false);
		scroll.SetActive(false);
		topFloor.SetActive(false);
		bottomFloor.SetActive(false);
	}

	private void showAll() {
		background.SetActive(true);
		money.SetActive(true);
		header.SetActive(true);
		text.SetActive(true);
		scroll.SetActive(false);
		topFloor.SetActive(false);
		bottomFloor.SetActive(false);
	}

}