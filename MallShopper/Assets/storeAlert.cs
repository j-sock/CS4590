using UnityEngine;
using System.Collections.Generic;

public class storeAlert : MonoBehaviour {

	public AudioClip baseSound;
	public AudioClip dealSound;

	private storeData data;

	void Start() {
		
	}

	void onTriggerEnter(Collider c) {
		if(c.transform.parent.gameObject.name=="DataObjects") {
			data = c.GetComponent<storeData>();

		}
	}
}