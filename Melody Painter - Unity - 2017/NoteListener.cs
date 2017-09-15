using UnityEngine;
using MidiJack;

public class NoteListener : MonoBehaviour {

	public int midiNumber;
	public int noteIndex;	// 1 ~ 5, 5 keys for right hand
	public int clipNum;		// index of notesound	
	public KeyCode key;		// keyboard simulation
	private AudioSource voice;
	public SpellListener spellListener;
	private bool isNoteDown = false;	 // true if corresponding note is press down

	void Start() {
		spellListener = (SpellListener) FindObjectOfType(typeof(SpellListener));
	}

	void Update () {
		if (MidiMaster.GetKeyDown (midiNumber) || Input.GetKeyDown (key)) {
			voice.PlayOneShot (voice.clip, 0.65f);
			isNoteDown = true;
			spellListener.AddNote (noteIndex);
		}
		if (MidiMaster.GetKeyUp (midiNumber) || Input.GetKeyUp (key)) {
			isNoteDown = false;
			spellListener.AddReleaseTime ();
		}
	}

	public bool IsNoteDown() {
		return isNoteDown;
	}

	public void ChangeNoteSound(AudioClip newClip) {
		voice = GetComponent<AudioSource> ();
		voice.clip = newClip;
	}
}
