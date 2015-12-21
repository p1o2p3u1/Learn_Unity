using UnityEngine;
using System.Collections;

// we want to make the cube more attractive
public class Rotator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		// make the transformation rate independent
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}
}
