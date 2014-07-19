using UnityEngine;
using System.Collections;

public class targetScript : MonoBehaviour {
	public bool isTargetOnFloorTwo;

	void OnTriggerEnter(Collider other) {
		//determine which floor the user is on
		if (other.name == "secondFloor") {
			isTargetOnFloorTwo = true;
		}
		else if (other.name == "firstFloor") {
			isTargetOnFloorTwo = false;
		}
	}

}
