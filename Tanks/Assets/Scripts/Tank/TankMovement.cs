using UnityEngine;

/*
 * 1. Get the input
 * 2. Setup the audio
 * 3. Setup forward/backward movement
 * 4. Setup turning
 * */
public class TankMovement : MonoBehaviour
{
	// identify which tank belongs to which player, this is set by the tank manager
    public int m_PlayerNumber = 1;         
    // speed of the tank
	public float m_Speed = 12f;            
    // degrees to turn
	public float m_TurnSpeed = 180f;  
	// tank engine audio source
    public AudioSource m_MovementAudio;    
    // two audio clip for drive or stop
	// audio to play when the tank isn't moving 
	public AudioClip m_EngineIdling;    
	// audio to play when the tank is moving
    public AudioClip m_EngineDriving; 
	// pitch: 坠落
	// The amount by which the pitch of the engine noises can vary.
    public float m_PitchRange = 0.2f;

	// horizontal or vertical
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    // reference used to move the tank
	private Rigidbody m_Rigidbody;       
	// the current movement input value
    private float m_MovementInputValue;
	// the current turn input value
    private float m_TurnInputValue;
	// The pitch of the audio source at the start of the scene.
    private float m_OriginalPitch;         

	// when the scene very first starts
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

	// called when the script is turned on it will be called after awake before any updates happen
    private void OnEnable ()
    {
		// isKinematic means isMoving, so when the game begin it's not moving
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
		// when the tank turned off, it should stop moving
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }
    
	// update is called every rendered frame
    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);
		EngineAudio ();
	}


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
		if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f) {
			// if the audio source is playing the driving clip
			if (m_MovementAudio.clip == m_EngineDriving) {
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		} else {
			if (m_MovementAudio.clip == m_EngineIdling) {
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}
	}

	// fixed update is called every physics step
    private void FixedUpdate()
    {
        // Move and turn the tank.
		Move();
		Turn ();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		// make this into a rotation in the y axis
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

	}
}