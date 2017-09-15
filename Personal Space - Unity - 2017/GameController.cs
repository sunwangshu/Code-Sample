using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private int stage = 1; // 1 - start, 2 - die, 3 - win 
	public Player player;
	public GameObject win;
	public GameObject shame;
	public GameObject loneliness;
	public Text feelingText;
	public Button playAgain;
	public AudioSource bgm;

	void Start () {
		playAgain.onClick.AddListener(Restart);
	}
		
	void Awake() {
		GameObject currentBGM = GameObject.FindGameObjectWithTag ("BGM");
		if (currentBGM == null) {
			AudioSource spawned = Instantiate (bgm);
			spawned.Play ();
			DontDestroyOnLoad(spawned);
		}
	}
		
	// Update is called once per frame
	void Update () {
		switch(stage) {
		case 1:
			if (player.IsAlive ()) {
				feelingText.text = player.GetFeeling ().ToString ("F2");
				if (player.IsSuccess ()) {
					feelingText.text = "";
					win.SetActive (true);
					playAgain.gameObject.SetActive (true);
					stage = 3;
				}
			} else {
				feelingText.text = "";
				if (player.GetDeathReason () == 1) {
					shame.SetActive (true);
				} else if (player.GetDeathReason () == -1) {
					loneliness.SetActive (true);
				}
				playAgain.gameObject.SetActive (true);
				stage = 2;
			}
			break;
		case 2:
//			if (Button.)) {
//				Application.LoadLevel(Application.loadedLevel);
//			}
			break;
		case 3:
//			if (Input.GetKeyDown (KeyCode.Space)) {
//				Application.LoadLevel(Application.loadedLevel);
//			}
			break;
		}
		 
	}

	public bool InGame() {
		return stage == 1;
	}

	void Restart() {
		SceneManager.LoadScene("street");
	}
}
