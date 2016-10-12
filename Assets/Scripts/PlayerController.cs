using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	// before drawing frame
	//void Update () {
		
	//}

	// before physics calculations
	void FixedUpdate () 
	{
		// support desktop and mobile
		bool mobileDevice = true;
#if UNITY_EDITOR
		mobileDevice = false;
#endif

		float moveHorizontal = 0, moveVertical = 0;

		if (!mobileDevice) {
			
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");

		} else {

			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);

				if (touch.phase == TouchPhase.Moved) {

					moveHorizontal = touch.deltaPosition.x;
					moveVertical = touch.deltaPosition.y;

					// this is too fast and too far, all we really want is direction
					if (moveHorizontal > 1) 
						moveHorizontal = 1;
					if (moveHorizontal < -1)
						moveHorizontal = -1;

					if (moveVertical > 1) 
						moveVertical = 1;
					if (moveVertical < -1)
						moveVertical = -1;

					speed = 10;
				}
			}
		}

		// no movement, lets exist
		if ((moveHorizontal == 0) && (moveVertical == 0)) {
			return;
		}

		Debug.Log ("Detected touch movement of (" + moveHorizontal + ", " + moveVertical + ")");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick up")) 
		{
			other.gameObject.SetActive(false);
			count++;
			SetCountText (); 
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 10) 
		{
			winText.text = "You win";
		}
	}
}
