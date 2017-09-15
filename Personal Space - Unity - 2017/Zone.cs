using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Zone : MonoBehaviour {

	public float transparency = 0.0f;
	public float fadeSpeed = 0.8f;
	private int fade = 0;	// 0 stay, 1 fade in, -1 fade out
	public Color color;
	[SerializeField]
	UnityEvent onPlayerEnter = new UnityEvent();

	void Start() {
		Color newColor = color;
		newColor.a = transparency;
		Renderer renderer = this.GetComponent<Renderer> ();
		renderer.material.color = newColor;
	}

	void Update () {
		
		if (fade > 0) {
			// print ("fadeIn");
			if (transparency < 1.0f) {
				transparency += fadeSpeed * Time.deltaTime;
			} else if (transparency > 1.0f) {
				transparency = 1.0f;
			} else {
				fade = 0;
			}
			Color newColor = color;
			newColor.a = transparency;
			Renderer renderer = this.GetComponent<Renderer> ();
			renderer.material.color = newColor;

		} else if (fade < 0) {
			// print ("fadeOut");
			if (transparency > 0.0f) {
				transparency -= fadeSpeed * Time.deltaTime;
			} else if (transparency < 0.0f) {
				transparency = 0.0f;
			} else {
				fade = 0;
			}
			Color newColor = color;
			newColor.a = transparency;
			Renderer renderer = this.GetComponent<Renderer> ();
			renderer.material.color = newColor;

		} else {
		}
	}

//	void OnTriggerStay(Collider coll) {
//		if (coll.gameObject.tag == "Player") {
//			Vector3 offset = (coll.transform.position - transform.position)/100f * -1;
//			NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent> ();
//			agent.Move (offset);
//		}
//	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			setFade (1);
			onPlayerEnter.Invoke ();
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			setFade (-1);
		}
	}

	public void setFade (int _fade) {
		fade = _fade;
	}
}
