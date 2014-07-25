using UnityEngine;
using System.Collections;

public class ItemTracker : MonoBehaviour {
	
	private GUI gui;
	private Item current;
	
	
	public AudioClip chime;
	public AudioClip buySound;
	public AudioClip floor1;
	public AudioClip floor2;
	
	public GameObject s1f1;
	public GameObject s1f2;
	public GameObject s2f1;
	public GameObject s2f2;
	
	private float lastDistance;
	
	void Start () {
		gui = GetComponent<GUI>();
		audio.clip = chime;
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
		if(lastDistance == distance) return;
		lastDistance = distance;
		int dFloor = getFloor(current.transform) - getFloor(transform);
		Vector3 dirToTarget = transform.position - target.position;
		dirToTarget.y = 0;
		dirToTarget = dirToTarget / -distance;
		Vector3 facing = transform.forward / transform.forward.magnitude;
		float angle = Vector3.Angle(facing, dirToTarget);
		audio.volume = .75f - angle / 180;
		
		if(Vector3.Distance(current.transform.position, transform.position) < 5f) {
		audio.PlayOneShot(buySound, 1f);
			gui.buyItem(current);
		}
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
		AudioClip clip = null;
		current = item;
		if(getFloor(item.transform) == 1) {
			clip = floor1;
		} else {
			clip = floor2;
		}
		audio.volume = 1f;
		if(getFloor(transform) != getFloor(item.transform))
			audio.PlayOneShot(clip, 1f);
	}
	
	public void mute() {
		audio.volume = 0;
	} 
}
