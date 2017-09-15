using UnityEngine;

public class NoteListenerGroup : MonoBehaviour {

	public NoteListener prefab;
	public AudioClip[] noteSounds = new AudioClip[7];

	// define five keys: 
	// right hand #0 - #4
	// so that right hand controls C3 - G3
	// in Keyboard it's {1 2 3 4 5}

	//C3 D3(#) E3 F3(#) G3
	// |  |     |  |     |
	// 1  2     3  4     5

	// also define two keys.
	// one flying key, one restart key.
	// G2 - fly (in PlayerController.cs), #55
	// C4 - reset (in PlayerController.cs), #72

	private int[] noteNums;
	public NoteListener[] notes;
	public KeyCode[] keys = new KeyCode[5] {KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L,KeyCode.Semicolon};

	void Start () {
		notes = new NoteListener[5];
		noteNums = new int[5] {60, 62, 64, 65, 67};
		for (int i = 0; i < noteNums.Length; i++) {
			notes[i] = Instantiate<NoteListener> (prefab,transform);
			notes[i].midiNumber = noteNums [i];
			notes[i].noteIndex = i + 1;
			notes[i].key = keys [i];
			notes[i].clipNum = i;
			notes[i].ChangeNoteSound (noteSounds [notes[i].clipNum]);
		}
	}

	public bool IsNoteDown () {
		bool isNoteDown = false;
		foreach (NoteListener note in notes) {
			if (note.IsNoteDown ()) {
				isNoteDown = true;
			}
		}
		return isNoteDown;
	}
}
