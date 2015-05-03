using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseTextFile : MonoBehaviour {
	[SerializeField] private TextAsset text;
	List <string> lines;

	void Start () {

	}

	void Update () {
	
	}

	public void ParseLines() {
		lines = new List<string>(text.text.Split(new string[] {"\r\n"},System.StringSplitOptions.None));
		GetComponent<LevelManager>().ReceiveListString(lines);
	}
}
