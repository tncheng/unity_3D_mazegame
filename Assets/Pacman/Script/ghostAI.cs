using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghostAI : MonoBehaviour
{
	private enum ghostType
	{
		ghostBlue,
		ghostRed,
		ghostYellow,
		ghostPink
	}

	[SerializeField]
	private ghostType ghost = ghostType.ghostBlue;
	[SerializeField]
	private GameObject enemy;
	[SerializeField]
	private Transform home;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float catchRange;
	//追捕范围
	[SerializeField]
	private float rotateSpeed;
	[SerializeField]
	private float patrolTime;
	//to lookover the ower area
	[SerializeField]
	private float huntTime;
	//to catch pacman
	[SerializeField]
	private float hideTime;
	//to avoid the pacman for huntting
	private NavMeshAgent nav;
	private float dist;
	private bool isScare = false;
	private PlayerControl player;
	private bool isBeCatch = false;
	[SerializeField]
	private Transform wps;
	private Transform[] ar_wp;
	private Transform currentDest;
	private bool isInit;
	// Use this for initialization
	void Start ()
	{
		Init ();
	}

	//初始化游戏对象
	private void Init ()
	{
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindObjectOfType<PlayerControl> ();
		nav.SetDestination (this.transform.position);
		isInit = true;
		ar_wp = wps.GetComponentsInChildren<Transform> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//根据枚举类型来判断幽灵是哪一个
		switch (ghost) {
		case ghostType.ghostBlue:
			behaveB ();
			break;
		case ghostType.ghostRed:
			behaveR ();
			break;
		case ghostType.ghostPink:
			behaveP ();
			break;
		case ghostType.ghostYellow:
			behaveY ();
			break;
		}
	}
	//蓝色幽灵行为
	private void behaveB ()
	{
		//Debug.Log ("isScared:" + isScare);
		//Debug.Log ("isBeCatched:" + isBeCatch);
		if (this.isScare) {
			Debug.Log ("受惊模式，远离pacman");
			if (this.isBeCatch) {
				Debug.Log ("蓝色我在回家:");
				nav.speed = 10f;
				nav.SetDestination (home.position);
			}
		} else {
			dist = Vector3.Distance (enemy.transform.position, this.transform.position);
			if (dist < catchRange) {
				huntPac ();
			} else {
				patrol ();
			}
		}
	}
	//红色幽灵的行为
	private void behaveR ()
	{
		//enemy.transform.position
		if (this.isScare) {
			Debug.Log (ghost + "受惊模式");
			if (this.isBeCatch) {
				nav.speed = 8f;
				nav.SetDestination (home.position);
			}
		} else {
			dist = Vector3.Distance (enemy.transform.position, this.transform.position);
			if (dist < catchRange) {
				huntPac ();
			} else {
				patrol ();
			}
		}
	}
	//紫色幽灵行为
	private void behaveP ()
	{
		if (this.isScare) {
			Debug.Log (ghost + "受惊模式");
			if (this.isBeCatch) {
				nav.speed = 8f;
				nav.SetDestination (home.position);
			}
		} else {
			dist = Vector3.Distance (enemy.transform.position, this.transform.position);
			if (dist < catchRange) {
				huntPac ();
			} else {
				patrol ();
			}
		}
		
	}
	//黄色幽灵行为
	private void behaveY ()
	{
		if (this.isScare) {
			Debug.Log (ghost + "受惊模式");
			if (this.isBeCatch) {
				nav.speed = 8f;
				nav.SetDestination (home.position);
			}
		} else {
			patrol ();
		}
	}
	//狩猎模式
	private void huntPac ()
	{
		nav.speed = 7f;
		Vector3 dir = enemy.transform.position - transform.position;
		Quaternion wantedRotation = Quaternion.LookRotation (dir);
		transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, rotateSpeed * Time.deltaTime);
		nav.SetDestination (enemy.transform.position);
	}
	//巡逻模式
	private void patrol ()
	{
		nav.speed = 5f;
		if (isInit) {
			currentDest = ar_wp [Random.Range (0, ar_wp.Length)];

			isInit = false;
		} else {
			if (Mathf.Abs (nav.remainingDistance) < 1f) {
				currentDest = ar_wp [Random.Range (0, ar_wp.Length)];
			}
		}
		nav.SetDestination (currentDest.position);
	}
	//受惊模式
	public void setScared (bool s)
	{
		this.isScare = s;
		if (!s) {
			this.isBeCatch = false;
		}
	}
	//碰撞检测代码
	void OnCollisionEnter (Collision other)
	{
		if (this.isScare) {
			if (other.gameObject.tag == "Player") {
				this.isBeCatch = true;
				Debug.Log ("i am go back");
			}
		}
	}
}
