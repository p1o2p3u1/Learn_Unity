using UnityEngine;
using System.Collections;
// to display our score text, using the following namespace
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// the speed of the player
	public float speed;

	// a reference to our player, which is a rigidbody
	private Rigidbody rb; 

	// the number score of our player
	private int score;

	// a reference to our UI text
	public Text textScore;
	public Text winText;

	// this function will be called on the first frame, which means the init function.
	void Start(){
		rb = GetComponent<Rigidbody> ();
		score = 0;
		SetScoreText ();
		winText.text = "";
	}

	// call before any physics calculation
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		rb.AddForce (movement * speed);
	}


	// when out player in collision with other game object
	void OnTriggerEnter(Collider other){
		// make sure that we are not touch other things.
		if(other.gameObject.CompareTag("PickUp")){
			score += 1;
			other.gameObject.SetActive (false);
			SetScoreText ();
		}
	}

	void SetScoreText(){
		textScore.text = "Score: " + score.ToString ();
		if (score >= 12) {
			winText.text = "You Win!";
		}
	}
}
