using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollide : MonoBehaviour {

	private void OnCollisionEnter(Collision collision) {
		ServiceLocator.AudioManager.PlayRandomLocal(collision.contacts[0].point, "DiceHit");
	}

}
