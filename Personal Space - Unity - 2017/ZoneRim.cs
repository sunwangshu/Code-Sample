using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRim : MonoBehaviour {

	public Zone parent;

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			parent.setFade (1);
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			parent.setFade (-1);
		}
	}
}
