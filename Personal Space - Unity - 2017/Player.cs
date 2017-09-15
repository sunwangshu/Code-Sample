using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
	
	private bool isAlive = true;
	private bool success = false;

	[SerializeField]
	UnityEvent onSuccess = new UnityEvent();
	[SerializeField]
	UnityEvent onDeath = new UnityEvent();

	void SetSuccess(bool value) {
		if (success == false && value == true && IsAlive()) {
			onSuccess.Invoke ();
			voice.Win ();
		}
		success = value;
	}

	void SetIsAlive(bool value) {
		if (isAlive == true && value == false) {
			onDeath.Invoke ();
			voice.Lose ();
		}
		isAlive = value;	
	}

	[SerializeField] private float shamingSpeed = 0.1f;		// speed of getting ashamed
	[SerializeField] private float freezingSpeed = 0.1f;		// speed of getting freezed
	[SerializeField] private float collisionShame = 0.2f;
	[SerializeField] private float neutralSpeed;		// average of shaming and freezing speed;

	private int temperature = -1; // 0 - neutral, 1 - hot, -1 cold	
	private float feelingMax = 1.0f;
	private float feelingMin = 0.0f;
	private float feelingNeutral = 0.5f;
	private float feeling = 0.5f;
	private int deathReason = 0;	// 1 - shame to death, -1 - die of loneliness

	[SerializeField] private Color CSafe = new Color(0.66F, 0.70F, 0.00F, 1F);
	[SerializeField] private Color CCold = new Color(0.36F, 0.60F, 0.75F, 1F);
	[SerializeField] private Color CHot = new Color(0.75F, 0.29F, 0.00F, 1F);
	[SerializeField] private Color CFeeling = new Color(0.66F, 0.70F, 0.00F, 1F);

	public Renderer rend;
	public Renderer ATFRend;

	// voice
	[SerializeField] private PlayerVoice voice;

	void Start () {
		neutralSpeed = (shamingSpeed + freezingSpeed) / 2;
	}

	void Update () {

		if (isAlive) {
			// if alive, change feelings according to temperature, and see if still alive
			if (feeling >= feelingMax) {
				feeling = feelingMax;
				deathReason = 1;
				SetIsAlive (false);		// die if too close
			} else if (feeling <= feelingMin) {
				feeling = feelingMin;
				deathReason = -1;
				SetIsAlive (false);		// die if too distant
			} else {
				if (!success) {
					if (temperature > 0) {		// shaming
						feeling += Time.deltaTime * shamingSpeed;
					} else if (temperature < 0) {		// cooling
						feeling -= Time.deltaTime * freezingSpeed;
					} else {	// temperature = 0;
						if (Mathf.Abs (feeling - feelingNeutral) < neutralSpeed / 10f) {
							feeling = feelingNeutral;
						} else {
							if (feeling < feelingNeutral) {				// de-cooling, using neutral speed
								feeling += Time.deltaTime * neutralSpeed;
							} else if (feeling > feelingNeutral) {		// de-shaming, using shaming speed
								feeling -= Time.deltaTime * neutralSpeed;
							}
						}
					}
				}
			}

			// change color of sprite and ATField according to feeling
			if (feeling == feelingNeutral) {
				CFeeling = CSafe;
			} else if (feeling > feelingNeutral) {
				CFeeling = Color.Lerp(CSafe,CHot,(feeling - feelingNeutral) * 2.0f);
			} else {
				CFeeling = Color.Lerp(CSafe,CCold,(feelingNeutral - feeling) * 2.0f);
			}
				
			rend.material.color = CFeeling;
			ATFRend.material.color = CFeeling;

		}

		// print ("Feeling: " + feeling + " Temperature: " + temperature +  " isAlive: " + isAlive);
	}

	void OnTriggerStay(Collider coll) {
		if (coll.gameObject.tag == "Alert Zone") {
			temperature = 1;
		}
		if (coll.gameObject.tag == "Comfort Zone") {
			temperature = 0;
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Pedestrain") {
			feeling += collisionShame;
		}

		if (coll.gameObject.tag == "Comfort Zone") {
			temperature = 0;
		}
		if (coll.gameObject.tag == "Alert Zone") {
			temperature = 1;
		}

		if (coll.gameObject.tag == "Finish") {
			SetSuccess(true);
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Alert Zone") {
			temperature = 0;
		}
		if (coll.gameObject.tag == "Comfort Zone") {
			temperature = -1;
		}
	}

	public bool IsAlive() {
		return isAlive;
	}

	public bool IsSuccess() {
		return success;
	}

	public int GetDeathReason() {
		if (isAlive) {
			return 0;
		} else {
			return deathReason;
		}
	}

	public float GetFeeling() {
		return (feeling - 0.5f) * 2;	// ranging from -1 to 1
	}
}


