using UnityEngine;
using System.Collections.Generic;

public class storeAlert : MonoBehaviour {

	public GameObject baseObject;
	public GameObject likeObject;
	public GameObject dealObject;

	private AudioSource baseSound;
	private AudioSource likeSound;
	private AudioSource dealSound;

	private storeData data;

	void Start() {
		baseSound = baseObject.GetComponent<AudioSource>();
		likeSound = likeObject.GetComponent<AudioSource>();
		dealSound = dealObject.GetComponent<AudioSource>();
	}

	void onTriggerEnter(Collider c) {
<<<<<<< HEAD
		Debug.Log("S9 TRIGGERED");
		if(c.transform.parent.gameObject.name=="StoreData" &&
			checkPosition(c.transform)) {
=======
		if(c.transform.parent.gameObject.name=="StoreData") {
>>>>>>> origin/master
			data = c.GetComponent<storeData>();
			//some minor calculations 
			scaleBase(data);
			scaleLike(data);
			scaleDeal(data);
			//has item
			if(data.all[0] == 1) {
				baseSound.PlayOneShot(baseSound.clip, 1.0f);
				likeSound.PlayOneShot(likeSound.clip,1.0f);
				dealSound.PlayOneShot(dealSound.clip, 1.0f);
				if (data.all[2] == 1) 
					likeSound.PlayOneShot(likeSound.clip, 1.0f);
			}
			//doesn't have item
			else {
				//in your demographic
				if(data.all[2] == 1) {
					//still plays a sound because capitalism 
					likeSound.PlayOneShot(likeSound.clip, 1.0f);
					dealSound.PlayOneShot(likeSound.clip, 1.0f);
				}
			}
		}
	}

<<<<<<< HEAD
	bool checkPosition(Transform t) {
		if(t.position.z > 0 && transform.position.z < t.position.z + 12)
			return true;
		else if(t.position.z < 0 && transform.position.z > t.position.z - 12)
			return true;
		else return false;
	}

=======
>>>>>>> origin/master
	void scaleBase(storeData d) {
		float volume = 0.0f;
		if (d.tShirt[0] == 1) volume += 0.25f;
		if (d.blouse[0] == 1) volume += 0.25f;
		if (d.jeans[0] == 1) volume += 0.25f;
		if (d.sneakers[0] == 1) volume += 0.25f;
		if (d.all[4] > 2) volume *= 0.75f;
		if (d.all[3] < 2) volume *= 0.75f;
		if (d.all[2] < 0) volume *= 0.75f;
		baseSound.volume = volume;
	}

	void scaleLike(storeData d) {
		if (d.all[4] < 3) likeSound.volume = 0.75f;
		else likeSound.volume = 1.0f;
	}

	void scaleDeal(storeData d) {
		dealSound.pitch = (float)(data.all[2] + (5 - data.all[3]))/10;
		dealSound.volume = (float)data.all[3]*2/10;
	}
}