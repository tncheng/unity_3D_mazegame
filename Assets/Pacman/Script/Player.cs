using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private CharacterController character;
	private Vector3 moveV;
	private float g = 5f;

	void Start ()
	{
		character = GetComponent<CharacterController> ();

	}

	void Update ()
	{
		Move ();
		
	}

	private void Move ()
	{
		if (character.isGrounded) {
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			Debug.Log ("h:" + h);
			Debug.Log ("v:" + v);
			moveV = new Vector3 (h, 0, v);
		}
		moveV += Vector3.down * g * Time.deltaTime;
		character.Move (moveV * Time.deltaTime);

	}
}
