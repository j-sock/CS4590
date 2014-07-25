using UnityEngine;
using System.Collections;

public class ItemTracker : MonoBehaviour {
	
	private Item current;
	public AudioClip ambient;
	
	public GameObject s1f1;
	public GameObject s1f2;
	public GameObject s2f1;
	public GameObject s2f2;
	
	void Start () {
		 audio.clip = ambient;
		 audio.volume = 0;
		 audio.loop = true;
		 audio.Play();
	}	
	
	void Update () {
		if(current == null) return;
		Transform target = null;
		if(getFloor(transform) == getFloor(current.transform)){
			target = current.transform;
		} else {
			target = pickStairs().transform;
		}
		
		float distance = Vector3.Distance(transform.position, target.position);
		if(Vector3.Distance(current.transform.position, transform.position) < 2f) {
			//buy
			mute();
		}
		float vertDistance = target.position.y - transform.position.y;
		audio.pitch = vertDistance / 20 + 1;
	}
	
	GameObject pickStairs() {
		int floor = getFloor(transform);
		float s1Dist = Vector3.Distance(transform.position, s1f1.transform.position);
		float s2Dist = Vector3.Distance(transform.position, s2f1.transform.position);
		if(floor == 2) {
			if(s1Dist > s2Dist)	return s2f1;
			else return s1f1;			
		} else {
			if(s1Dist > s2Dist)	return s2f2;
			else return s1f2;	
		}
		return null;
	}
	
	int getFloor(Transform t) {
		if(t.position.y < 6) return 1;
		return 2;
	}
	
	public void startTracking(Item item) {
		current = item;
		audio.volume = .5f;
	}
	
	public void mute() {
		audio.volume = 0;
	} 
}
