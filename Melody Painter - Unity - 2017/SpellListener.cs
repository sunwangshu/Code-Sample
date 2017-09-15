using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell{
	Land = 0,
	Grass = 1,
	Flower = 2,
	Tree = 3,
	Rock = 4,
	House = 5,
}

public class SpellListener : MonoBehaviour {

	// a 4-note list that stores current input, insert from first
	public float timeOut = 1f; // timeout in secs
	public NoteListenerGroup notes;

	private float lastInputTime; 

	private int[] noteList;
	private int spellIndex = -1;		// # of newly formed spell, -1 means null
	private int[][] spellList = new int[6][];
	//private string[] spellNames = new string[6] {"Land", "Grass", "Flower", "Tree", "Rock", "House"};

	private bool newNote = false;		// true if new note coming
	private bool newSpell = false;		// true if new spell is formed

	void Start () {
		noteList = new int[4] {0, 0, 0, 0};
		spellList [0] = new int[] { 1, 2, 1, 2 };			// land
		spellList [1] = new int[] { 1, 2, 2, 3 }; 			// grass
		spellList [2] = new int[] { 3, 4, 3, 4 }; 			// flower
		spellList [3] = new int[] { 1, 4, 4, 5 };			// tree
		spellList [4] = new int[] { 3, 1, 3, 1 };			// rock
		spellList [5] = new int[] { 5, 4, 5, 4 };			// house
	}

	void Update () {
		// if there is no note down over timeout time, reset input
		if (noteList [3] != 0) {
			if (!notes.IsNoteDown()) {
				if (Time.time > lastInputTime + timeOut) {
					ClearList ();
					newSpell = true;
				}
			}
		}
	}

	// --------------- public funcions --------------------

	public void AddNote (int noteIndex) {
		ShiftNote ();
		noteList [noteList.Length - 1] = noteIndex;
		newNote = true;
		lastInputTime = Time.time + 100f;

		// if four notes
		if (noteList [0] != 0) {
			CheckSpell ();
		}

	}

	public void AddReleaseTime () {
		lastInputTime = Time.time;
	}


	public bool IsNewNote() {
		return newNote;
	}

	public bool IsNewSpell() {
		return newSpell;
	}

	public string GetNoteList () {		// returns a string
		string info = "";
		for (int i = 0; i < noteList.Length; i++) {
			if (noteList [i] != 0) {
				info += noteList [i] + " ";
			}
		}
		return info;
	}

	public Spell GetSpell () {			// returns #
		return (Spell)spellIndex;
	}

	public void ReceiveNoteList () {
		newNote = false;
	}

	public void ReceiveSpell () {
		ClearList ();
		newSpell = false;
	}

		

	// --------------- private funcions --------------------

	private void ShiftNote () {	// shift left and add zero to end
		for (int i = 0; i < noteList.Length - 1; i++) {
			noteList [i] = noteList [i + 1];
		}
		noteList [noteList.Length - 1] = 0;
	}

	private void CheckSpell() {
		for (int i = 0; i < spellList.Length; i++) {
			if (ListEqual(noteList, spellList[i])) {
				spellIndex = i;
				break;
			}
		}

		if (spellIndex != -1) {
			newSpell = true;
			//print ("Spell: #" + spellIndex + " " + (Spell)spellIndex);
		}
	}

	private void ClearList() {
		// clear note list
		for (int i = 0; i < noteList.Length; i++) {
			noteList [i] = 0;
		}
		spellIndex = -1;
		newNote = true;
	}


	private bool ListEqual(int[] l1, int[] l2) {
		for (int i = 0; i < l1.Length; i++) {
			if (l1 [i] != l2 [i]) {
				return false;
			}
		}
		return true;
	}
		
}
