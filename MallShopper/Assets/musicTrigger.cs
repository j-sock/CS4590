using UnityEngine;
using System.Collections;

public class musicTrigger : MonoBehaviour {

	public AudioClip thisMusic;
	
	void Start()
	{
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "User")
		{
			audio.clip = thisMusic;
			audio.volume = 0.8f;
			audio.Play();

		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "User")
		{
			audio.Pause();
	
		}
}
}