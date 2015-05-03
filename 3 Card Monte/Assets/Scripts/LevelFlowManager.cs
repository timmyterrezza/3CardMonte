using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelFlowManager : MonoBehaviour {
	private LevelManager levelManager;

	private bool moveRound = false;
	private int moves;
	private float startingSpeed;

	private GameObject[] cards;

	private int section = 1;

	private float rotationDelay;
	private bool needsRotationDelay;

	private bool needsMoveDelay;
	private bool cardsMoving = false;

	private int firstMovingCard;
	private int secondMovingCard;
	private Vector3 firstDestination;
	private Vector3 secondDestination;

	private float yBend;

	private float currentMoves = 0;
	private bool nextMove;

	private Ray ray;

	private GameObject mainCamera;
	private RaycastHit hit;

	private bool guessedCorrect;

	public int score = 0;

	private bool gameover = false;

	private GameObject startButton;

	private GameObject inputField;
	private GameObject inputText;

	private HighScoreManager highscores;

	private void SubmitName(string argument) {
		highscores.addEntry(argument,score);
		inputField.GetComponent<InputField>().text = "";
		inputField.SetActive(false);
		inputText.SetActive(false);
		gameover = false;
	}

    void Start () {
		cards = GameObject.FindGameObjectsWithTag("card");
		mainCamera = GameObject.Find ("Main Camera");
		levelManager = GetComponent<LevelManager>();
		startButton = GameObject.Find ("Start Button");
		inputField = GameObject.Find ("Name");
		inputText = GameObject.Find ("Directions");

		InputField input = inputField.GetComponent<InputField>();
		InputField.SubmitEvent subEvent = new InputField.SubmitEvent();
		subEvent.AddListener(SubmitName);
		input.onEndEdit = subEvent;
		inputField.SetActive(false);
		inputText.SetActive(false);
        
		highscores = GetComponent<HighScoreManager>();
    }
    
    void Update () {
		if (moveRound) {
			if (section == 1) {
				for (int i = 0; i < cards.Length; i++) {
					cards[i].GetComponent<RotateCard>().TriggerRotation();
				}
				section += 1;
				needsRotationDelay = false;
			}
			else if (section == 2) {
				//If previous rotation was done, sets delay between rotations
				if (cards[0].GetComponent<RotateCard>().GetRotationAmount() >= 180 && needsRotationDelay == false) {
					needsRotationDelay = true;
					rotationDelay = Time.time;
				}
				//If delay is done
				if (needsRotationDelay && Time.time - rotationDelay > 1) {
					for (int i = 0; i < cards.Length; i++) {
						cards[i].GetComponent<RotateCard>().TriggerRotation();
					}
					section += 1;
					needsMoveDelay = false;
				}
			}
			else if (section == 3) {
				//If previous rotation was done
				if (cards[0].GetComponent<RotateCard>().GetRotationAmount() >= 180 && needsMoveDelay == false) {
					nextMove = false;
					CalculateMoveIndex();
					needsMoveDelay = true;
					cardsMoving = false;
				}
				//If move is done
				if (needsMoveDelay && cardsMoving == false) {
					cards[firstMovingCard].GetComponent<MoveCard>().SetJourney(cards[secondMovingCard].transform.position,startingSpeed,yBend);
					cards[secondMovingCard].GetComponent<MoveCard>().SetJourney(cards[firstMovingCard].transform.position,startingSpeed,yBend * -1);
					cardsMoving = true;
				}
				if (cardsMoving && cards[firstMovingCard].GetComponent<MoveCard>().finished == true) {
					nextMove = true;
					currentMoves += 1;
				}
				//Check for multiple moves
				if (nextMove && currentMoves < moves) {
					needsMoveDelay = false;
				}
				else if (nextMove && currentMoves >= moves){
					section += 1;
				}
			}
			else if (section == 4) {
				//Wait for user input
				if (Input.GetMouseButtonDown(0)) {
					ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit)) {
						if (hit.transform.name == "Queen") {
							score += 100;
							section += 1;
						}
						else if (hit.transform.name == "Four" || hit.transform.name == "Nine") {
							gameover = true;
							moveRound = false;
						}
					}
				}
			}
			else if (section == 5) {
				levelManager.TriggerProcess();
			}
		}
		else {
			if (gameover) {
				inputText.SetActive(true);
				inputField.SetActive(true);
			}
			else {
				score = 0;
				startButton.SetActive(true);
			}
		}
	}

	public void SetRound(int newMoves, float newStartingSpeed) {
		moves = newMoves;
		startingSpeed = newStartingSpeed;
		moveRound = true;
		section = 1;
		cardsMoving = false;
		currentMoves = 0;
	}

	public void AdvanceSection() {
		section += 1;
	}

	private void CalculateMoveIndex() {
		float randomNumber = Random.Range(0f,1f);
		if (randomNumber < 0.33) {
			firstMovingCard = 0;
			secondMovingCard = 1;
			yBend = 0.5f;
		}
		else if (randomNumber < 0.66) {
			firstMovingCard = 0;
			secondMovingCard = 2;
			yBend = 1f;
		}
		else {
			firstMovingCard = 1;
			secondMovingCard = 2;
			yBend = 0.5f;
		}
	}
}
