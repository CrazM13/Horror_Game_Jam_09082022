using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Needle : MonoBehaviour {

	[SerializeField] private GameObject bloodParticles;

	private Vector3 startPosition;
	private Vector3 targetPosition;
	private Vector3 lookAtPosition;
	private float movementPercentage = -1;

	public UnityEvent<Needle> OnTargetReached { get; private set; } = new UnityEvent<Needle>();

	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		if (movementPercentage >= 0) {
			movementPercentage += Clock.DeltaTime;
			transform.position = Vector3.Lerp(startPosition, targetPosition, movementPercentage);
			transform.LookAt(lookAtPosition);
			if (movementPercentage >= 1) {
				transform.position = targetPosition;
				movementPercentage = -1;
				OnTargetReached.Invoke(this);
			}
		}
	}

	public void MoveTo(Vector3 position, Vector3 lookAt) {
		targetPosition = position;
		lookAtPosition = lookAt;
		movementPercentage = 0;
		startPosition = transform.position;
	}

	public void SpawnBlood(Vector3 normal) {
		if (!bloodParticles) return;

		GameObject blood = Instantiate(bloodParticles, transform.position, Quaternion.LookRotation(normal, Vector3.up));
		Destroy(blood, 10f);
	}
}
