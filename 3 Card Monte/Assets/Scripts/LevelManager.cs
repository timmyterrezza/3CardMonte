using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	private List<string> textList;
	private bool readyToProcess = false;

	private int round = 0;

	private int roundMoves;
	private float startingSpeed;

	void Start () {

	}
	

	void Update () {
		if (readyToProcess) {
			if (round < textList.Count) {
				ProcessString(textList[round]);
			}
			readyToProcess = false;
		}
	}

	void ProcessString(string line) {
		char[] delim = {','};
		string[] stringArray = line.Split(delim, System.StringSplitOptions.None);
		roundMoves = int.Parse(stringArray[0]);
		startingSpeed = float.Parse(stringArray[1]);
		GetComponent<LevelFlowManager>().SetRound(roundMoves, startingSpeed);
	}

	public void ReceiveListString(List<string>text) {
		round = 0;
		textList = text;
		readyToProcess = true;
	}

	public void TriggerProcess() {
		readyToProcess = true;
		round += 1;
	}
}
