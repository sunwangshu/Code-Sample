using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleIndicators : MonoBehaviour {

	public Text noteList;	// inputed notes
	public Text currentSpell;	// current (last) executed spell

	public SpellListener spellListener;		// listens if a meaningful spell is formed
	public PlayerController playerController;

	void Update () {
		if (spellListener.IsNewNote()) {
			noteList.text = "NoteList: " + spellListener.GetNoteList();
			spellListener.GetSpell();

			spellListener.ReceiveNoteList();
		}
		if (spellListener.IsNewSpell ()) {
			currentSpell.text = "Current Spell: #" + spellListener.GetSpell ();
			spellListener.ReceiveSpell ();
		}
	}	
}
