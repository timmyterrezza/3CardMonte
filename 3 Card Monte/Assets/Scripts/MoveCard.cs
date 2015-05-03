using UnityEngine;
using System.Collections;

public class MoveCard : MonoBehaviour {
	[SerializeField] private Vector3 startPosition;
	[SerializeField] private Vector3 endPosition;
	[SerializeField] private float speed;
	[SerializeField] private float yBend;
	private float startTime;
	private float journeyLength;
	private bool shouldMove;

	private float fracJourney;
	private float distCovered;

	public bool finished;

	void Start () {
		startTime = Time.time;
		shouldMove = false;
		journeyLength = Vector3.Distance(startPosition, endPosition);
		finished = false;
	}

	void Update () {
		distCovered = (Time.time - startTime) * speed;
		fracJourney = distCovered / journeyLength;

		if (fracJourney >= 1f && shouldMove) {
			shouldMove = false;
			transform.localPosition = endPosition;
			finished = true;
		}

		if (shouldMove) {
			Move(fracJourney);
		}
	}

	void Move(float fractionJourney) {
		Vector3 change = endPosition - startPosition;
		Vector3 currentPosition = startPosition;
		currentPosition.x += change.x * fractionJourney;
		currentPosition.z += change.z * fractionJourney + Mathf.Sin (fractionJourney * Mathf.PI) * yBend;
		transform.position = currentPosition;
	}

	public void SetJourney(Vector3 end, float newSpeed, float newYBend) {
		startTime = Time.time;
		startPosition = transform.position;
		endPosition = end;
		speed = newSpeed;
		yBend = newYBend;
		journeyLength = Vector3.Distance(startPosition, end);
		shouldMove = true;
		finished = false;
	}

}
