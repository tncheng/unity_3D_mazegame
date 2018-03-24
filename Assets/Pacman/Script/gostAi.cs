using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gostAi : MonoBehaviour
{
	public GameObject enemy;
	public float moveSpeed;
	public float catchRange;
	private NavMeshAgent nav;
	private float rotateSpeed;
	private float dist;

	void Start ()
	{
		nav = GetComponent<NavMeshAgent> ();
	}

	void Update ()
	{
		dist = Vector3.Distance (enemy.transform.position, transform.position);
		if (dist < catchRange) {
			nav.SetDestination (enemy.transform.position);
		} else {
			Vector3 dir = enemy.transform.position - transform.position;
			Quaternion wantedROtation = Quaternion.LookRotation (dir);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedROtation, rotateSpeed * Time.deltaTime);
		}


	}
}
