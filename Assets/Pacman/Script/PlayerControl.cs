using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private int currentLife = 3;
	private int currentScore = 0;
	private bool isDead = false;
	private Vector3 dir = Vector3.zero;
	private bool isAward = false;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		//监听按键输入
		if (Input.GetKey (KeyCode.A)) {
			dir = Vector3.left;
			transform.rotation = Quaternion.LookRotation (Vector3.left);
		} else if (Input.GetKey (KeyCode.D)) {
			dir = Vector3.right;
			transform.rotation = Quaternion.LookRotation (Vector3.right);
		} else if (Input.GetKey (KeyCode.W)) {
			dir = Vector3.forward;
			transform.rotation = Quaternion.LookRotation (Vector3.forward);
		} else if (Input.GetKey (KeyCode.S)) {
			dir = Vector3.back;
			transform.rotation = Quaternion.LookRotation (Vector3.back);
		}
		if (dir != Vector3.zero) {
			transform.rotation = Quaternion.Lerp (
				transform.rotation,
				Quaternion.LookRotation (dir),
				0.5f
			);
		}
		Debug.Log ("life:" + this.currentLife);
		//检测死亡，判断是否停止角色控制并且播放死亡动画
		if (isDead) {
			notification.Instance.GameOver ();
			//Invoke ("GetComponent<Animator> ().SetBool (\"isStop\", true)", 10f);
			GetComponent<Animator> ().SetBool ("isStop", true);
			this.enabled = false;
		}
	}
	//碰撞检测，检测碰撞对象来增加分数或者死亡
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "bean") {
			notification.Instance.Score++;
			Destroy (other.gameObject);
			Debug.Log ("douzi1");
		} else if (other.gameObject.tag == "award") {
			this.currentScore += 20;
			setAward (true);
			Debug.Log ("奖励");
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "gost") {
			if (!isAward) {
				if (currentLife > 0) {
					currentLife--;
					notification.Instance.Life--;
					this.enabled = false;
					GetComponent<Animator> ().SetBool ("isStop", true);
					Invoke ("reSetPos", 1.5f);
				} else {
					isDead = true;
				}
			}
		}

	}
	//对象死亡时，重置游戏对象
	private void reSetPos ()
	{
		GetComponent<Animator> ().SetBool ("isStop", false);
		this.transform.position = new Vector3 (-1.7f, 0.4f, -22.0f);
		this.enabled = true;
	}
	//外部接口，获取游戏当前分数
	public int getScore ()
	{
		return this.currentScore;
	}
	//奖励模式是否开启
	public void setAward (bool a)
	{
		this.isAward = a;
	}

	public bool getAward ()
	{
		return this.isAward;
	}
}
