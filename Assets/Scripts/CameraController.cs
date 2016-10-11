using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	// LateUpdate is called once her frame; but guaranteed to run after Update() has been processed
	void LateUpdate () {
		// follow our player with the camera
		// place this in LateUpdate () so player is updated before camera position
		transform.position = player.transform.position + offset;
	}
}
