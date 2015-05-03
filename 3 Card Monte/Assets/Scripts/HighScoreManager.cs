using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour {
	public Dictionary<string, List<int>> highscores;
	// Use this for initialization
	void Start () {
		highscores = new Dictionary<string, List<int>>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addEntry(string name, int score) {
		if (!highscores.ContainsKey(name) ){
			highscores[name] = new List<int>();
		}
		highscores[name].Add(score);
		ShowEntries();
	}

	public void ShowEntries() {
		foreach(KeyValuePair<string, List<int>> pair in highscores){
			foreach(int value in pair.Value){
				Debug.Log("Key: "+ pair.Key + "Value: " + value);
			}
		}
	}
}
