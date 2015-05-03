using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	private int score;
	private LevelFlowManager levelManager;
	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelFlowManager>();
	}
	
	// Update is called once per frame
	void Update () {
		score = levelManager.score;
		GetComponent<Text>().text = "Score: " + score;
	}
}
