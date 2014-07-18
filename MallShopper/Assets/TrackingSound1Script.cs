using UnityEngine;
using System.Collections;

public class TrackingSound1Script : MonoBehaviour {
	public float distanceFromTarget;
	public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		distanceFromTarget = Vector3.Distance(transform.position,target.position);
		Debug.Log ("current distance from target is: " + distanceFromTarget);
	}
}
