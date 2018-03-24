using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//镜头跟随
public class CameraFlow : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	Vector3 offset;

	void Start ()
	{
		offset = transform.position - target.position;
	}

	void Update ()
	{
		transform.position = target.position + offset;
	}


}
