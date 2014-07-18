using UnityEngine;
using System.Collections;

public class TrackingSound1Script : MonoBehaviour {
	public AudioClip radarSoundLevel;
	public AudioClip radarSoundAbove;
	public AudioClip radarSoundBelow;

	public float timeWait;
	public float distanceFromTarget;
	public Transform target;
	bool keepPlaying;
	bool isOnSecondFloor;
	bool isOnFirstFloor;

	public bool targetOnFloorTwo;


	// Use this for initialization
	void Start () {
		timeWait = 3;
		keepPlaying = true;
		StartCoroutine(SoundOut());
		//figure out which floor the target is on...
		//if targetOnFloorTwo is false, target is on first floor
		//if targetOnFloorTwo is true, target is on second floor
		targetOnFloorTwo = true;
	}
	
	// Update is called once per frame
	void Update()
	{
				//get the distance between the user and the target item
				distanceFromTarget = Vector3.Distance (transform.position, target.position);
				//Debug.Log ("distance from target = " + distanceFromTarget);

				//get the direction the user is facing based on where the object is.
		
				var relativePos = target.position - transform.position;
		
				var forward = transform.forward;
				var angle = Vector3.Angle (relativePos, forward);
		if ((Vector3.Cross (forward, relativePos).y < 20) && (Vector3.Cross (forward, relativePos).y > -20)) {
			//Debug.Log ("target is in the center. Angle is: " + Vector3.Cross (forward, relativePos).y);
				} 
		else if ((Vector3.Cross (forward, relativePos).y < -20)){
			//Debug.Log ("target is on the left. Angle is: " + Vector3.Cross (forward, relativePos).y);
				}
		else if ((Vector3.Cross (forward, relativePos).y > 20)){
			//Debug.Log ("target is on the right. Angle is: " + Vector3.Cross (forward, relativePos).y);
		}

			
		}

	IEnumerator SoundOut()
	{
		while (keepPlaying){
			
			//affect the Pitch of the Sound first
			//this line is here so that the sound doesn't change while it's playing, 
			//because that sounded horrible and offkey
			audio.pitch = 2.0f + (-distanceFromTarget/200.0f);

			//determine if sound is above, below, or on the same level as the user
			//then actually play the sound
			//...sorry about all the if statements....
			if((isOnFirstFloor == true) && (targetOnFloorTwo == false))
			//if the user is on the first floor and the target is on the first floor
			{
				audio.PlayOneShot(radarSoundLevel);
			}
			else if((isOnSecondFloor == true) && (targetOnFloorTwo == true))
				//if the user is on the second floor and the target is on the second floor
			{
				audio.PlayOneShot(radarSoundLevel);
			}
			else if((isOnSecondFloor == true) && (targetOnFloorTwo == false))
				//if the user is on the second floor and the target is on the first floor
			{
				audio.PlayOneShot(radarSoundBelow);
			}
			else if((isOnFirstFloor == true) && (targetOnFloorTwo == true))
				//if the user is on the first floor and the target is on the second floor
			{
				audio.PlayOneShot(radarSoundAbove);
			}
			//Debug.Log("sound Played (same level)");
			yield return new WaitForSeconds(timeWait);
		}
	}


	void OnTriggerEnter(Collider other) {
		//determine which floor the user is on
		if (other.name == "secondFloor") {
			Debug.Log("Entered Second Floor");	
			isOnFirstFloor = false;
			isOnSecondFloor = true;
		}
		else if (other.name == "firstFloor") {
			Debug.Log("Entered First Floor");
			isOnFirstFloor = true;
			isOnSecondFloor = false;
		}
	}
}


	

