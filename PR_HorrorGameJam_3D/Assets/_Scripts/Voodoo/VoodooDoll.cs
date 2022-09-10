using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooDoll : MonoBehaviour {

	[SerializeField] private Transform target;

	[SerializeField] private Needle[] needles;
	[SerializeField] private Needle[] largeNeedles;

	private List<Vector3> offsets = new List<Vector3>();
	private int needleIndex;

	private bool IsEnabled { get; set; } = false;

	// Start is called before the first frame update
	void Start() {
		ServiceLocator.GameManager.OnChangedState.AddListener(OnChangedState);
	}

	// Update is called once per frame
	void Update() {
		if (!IsEnabled) return;

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
				ServiceLocator.GameManager.CurrentState = GameStateManager.GameStates.VOODOO_PAIN;
				IsEnabled = false;
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

	private void OnChangedState(GameStateManager gameManager) {
		IsEnabled = gameManager.CurrentState == GameStateManager.GameStates.VOODOO_SELECT;
	}
}
