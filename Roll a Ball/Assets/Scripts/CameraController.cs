using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// associated game object
	public GameObject player;

	// the difference between the game object and the camera
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		// yeah this is the absolute difference
		offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame, unlike Update it is guaranteed to run after all items
	// have been processed in Update.
	void LateUpdate () {
		// calculate the camera's position after the player moves.
		transform.position = player.transform.position + offset;
	}
}
