using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooDoll : MonoBehaviour {

	[SerializeField] private Transform target;

	[SerializeField] private Needle[] needles;
	[SerializeField] private Needle[] largeNeedles;

	private List<Vector3> offsets = new List<Vector3>();
	private int needleIndex;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0) && needleIndex < needles.Length) {
			StabDoll();
		}
	}

	private void StabDoll() {
		Camera camera = Camera.main;

		Ray ray = camera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit)) {
			if (hit.collider.gameObject == gameObject) {
				Vector3 hitOffset = hit.point - transform.position;
				offsets.Add(hitOffset);
				MoveNeedle(hitOffset);
			}
		}
	}

	private void MoveNeedle(Vector3 offset) {
		needles[needleIndex].MoveTo(transform.position + (offset * 5), transform.position + offset);
		needles[needleIndex].OnTargetReached.AddListener((needle) => {
			needle.MoveTo(transform.position + (offset * 0.5f), transform.position + (offset * 0.5f));
			needle.OnTargetReached.RemoveAllListeners();
		});

		largeNeedles[needleIndex].MoveTo(target.position + (offset * 5), target.position + offset);
		largeNeedles[needleIndex].OnTargetReached.AddListener((needle) => {
			needle.MoveTo(target.position + (offset * 0.5f), target.position + (offset * 0.5f));
			needle.OnTargetReached.RemoveAllListeners();
			needle.OnTargetReached.AddListener((needle2) => needle2.SpawnBlood(offset));
		});

		needleIndex++;
	}
}
