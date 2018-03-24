using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class notification : MonoBehaviour
{
	private ghostAI[] ghosts;
	private PlayerControl player;
	private int count = 368;
	private float timeCount = 0;
	private float t;
	private float sec;

	public static notification Instance;
	//实例游戏管理者单利模式
	void Awake ()
	{
		Instance = this;
	}
	//分数系统
	[SerializeField]
	Text tScore;
	int score = 0;
	//game score
	public int Score {
		get{ return score; }
		set {
			score = value;
			tScore.text = score.ToString ();
		}
	}
	//生命系统
	[SerializeField]
	Text tLife;
	int life = 0;
	//game score
	public int Life {
		get{ return life; }
		set {
			life = value;
			tLife.text = life.ToString ();
		}
	}
	//通知面板
	[SerializeField]
	GameObject noty;

	public void GameOver ()
	{
		noty.SetActive (true);
		Debug.Log ("game over");
	}
	//重新开始游戏
	public void Replay ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	//time
	[SerializeField]
	private Text timeInfo;

	private void time ()
	{
		timeCount += Time.deltaTime;
		timeInfo.text = ((int)timeCount).ToString ();
	}


	// Use this for initialization
	void Start ()
	{
		ghosts = GameObject.FindObjectsOfType<ghostAI> ();
		player = GameObject.FindObjectOfType<PlayerControl> ();
		this.Life = 3;
	}
	
	// 固定帧更新，通过计时来切换幽灵行为模式
	void FixedUpdate ()
	{
		
		time ();
		if (player.getAward ()) {
			sec += Time.deltaTime;
			Debug.Log ("奖励模式开启" + sec);
			if (sec < 10) {
				foreach (ghostAI g in ghosts) {
					g.setScared (true);
				}
			} else {
				Debug.Log ("奖励模式关闭" + sec);
				player.setAward (false);
				foreach (ghostAI g in ghosts) {
					g.setScared (false);
				}
			}
		}
		if (this.count < 1) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			this.count = 368;
		}
	}


}
