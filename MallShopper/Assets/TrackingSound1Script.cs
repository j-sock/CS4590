using UnityEngine;
using System.Collections;

public class TrackingSound1Script : MonoBehaviour {
	public AudioClip radarSoundLevel;
	public AudioClip radarSoundAbove;
	public AudioClip radarSoundBelow;

	//public GameObject targetWeAreLookingAt;

	public float timeWait;
	public float distanceFromTarget;
	public Transform[] targetArray;
	bool keepPlaying;
	bool isOnSecondFloor;
	public int currentTarget;

	public bool targetOnFloorTwo;
	//get the information from the targetScript
	private targetScript thisTarget;


	// Use this for initialization
	void Start () {
		currentTarget = 0;
		timeWait = 3;
		keepPlaying = true;
		StartCoroutine(SoundOut());
		//figure out which floor the target is on...
		//if targetOnFloorTwo is false, target is on first floor
		//if targetOnFloorTwo is true, target is on second floor

		//get the information from the target gameobject
		thisTarget = targetArray [currentTarget].GetComponent<targetScript>();
		targetOnFloorTwo = thisTarget.isTargetOnFloorTwo;
		Debug.Log ("target is on floor 2 = " + targetOnFloorTwo);
	}
	
	// Update is called once per frame
	void Update()
	{
				//get the distance between the user and the target item
				distanceFromTarget = Vector3.Distance (transform.position, targetArray[currentTarget].position);
				//Debug.Log ("distance from target = " + distanceFromTarget);

				//get the direction the user is facing based on where the object is.
		
				var relativePos = targetArray[currentTarget].position - transform.position;
		
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
		//if player presses "y", switch to the next targetItem
		if (Input.GetKeyUp(KeyCode.Y)){
			switchTarget();
		}
			
		}

	IEnumerator SoundOut()
	{
		while (keepPlaying) {
			
						//affect the Pitch of the Sound first
						//this line is here so that the sound doesn't change while it's playing, 
						//because that sounded horrible and offkey
						audio.pitch = 2.0f + (-distanceFromTarget / 200.0f);

						//determine if sound is above, below, or on the same level as the user
						//then actually play the sound
						//...sorry about all the if statements....
						if ((isOnSecondFloor == false) && (targetOnFloorTwo == false)) {
			//if the user is on the first floor and the target is on the first floor
								audio.PlayOneShot (radarSoundLevel);
						} else if ((isOnSecondFloor == true) && (targetOnFloorTwo == true)) {
				//if the user is on the second floor and the target is on the second floor
								audio.PlayOneShot (radarSoundLevel);
						} else if ((isOnSecondFloor == true) && (targetOnFloorTwo == false)) {
				//if the user is on the second floor and the target is on the first floor
								audio.PlayOneShot (radarSoundBelow);
						} else if ((isOnSecondFloor == false) && (targetOnFloorTwo == true)) {
				//if the user is on the first floor and the target is on the second floor
								audio.PlayOneShot (radarSoundAbove);
						}
						//Debug.Log("sound Played (same level)");
						yield return new WaitForSeconds (timeWait);
		
				}
			}


	void OnTriggerEnter(Collider other) {
		//determine which floor the user is on
		if (other.name == "secondFloor") {
			Debug.Log("Entered Second Floor");	
			isOnSecondFloor = true;
		}
		else if (other.name == "firstFloor") {
			Debug.Log("Entered First Floor");
			isOnSecondFloor = false;
		}
	}

	void switchTarget(){
		//move the target up one and get the new information from it about its location
			currentTarget = currentTarget + 1;
			if (currentTarget >= targetArray.Length){
				currentTarget = 0;
			}
				thisTarget = targetArray [currentTarget].GetComponent<targetScript>();
				targetOnFloorTwo = thisTarget.isTargetOnFloorTwo;
				Debug.Log ("CurrentTarget is " + currentTarget + ". This is the " + targetArray[currentTarget].gameObject.name + " and floorTwo is " + targetOnFloorTwo);
		}
}


	

