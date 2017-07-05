using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class CharacterData {
	//lets test this fucking thing
	//ssave
	//load with by object summoned by image target
	//debug log the loaded char data

	public static CharacterData LoadedCharData;
	public string charID;
	public int HP;
	public int MP;

	public CharacterData () {
		charID = null;
		HP = 0;
		MP = 0;
	}

	public CharacterData (string id, int hp, int mp) {
		charID = id;
		HP = hp;
		MP = mp;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void SaveCharacterData(string char_id, int HP, int MP) {
		//save (more like set) the entire database into 
		CharacterData cd = new CharacterData();
		cd.charID = char_id;
		cd.HP = HP;
		cd.MP = MP;

		string charData = JsonUtility.ToJson (cd);
		//Debug.Log (charData);
		PlayerPrefs.SetString (char_id, charData);
	}

	public static void SaveCharacterData(CharacterData cd) {
		string charData = JsonUtility.ToJson (cd);
		Debug.Log (charData);
		PlayerPrefs.SetString (cd.charID, charData);
	}

	public static void SaveCharacterData(CharacterData[] cd) {

		for (int i = 0; i < cd.Length; i++) {
			SaveCharacterData (cd [i]);
			/*
			string charData = JsonUtility.ToJson (cd[i]);
			//Debug.Log (charData);
			PlayerPrefs.SetString (cd[i].charID, charData);
			*/
		}
	}

	public static int LoadCharacterData(string charID) {
		//load from pref
		string charData = PlayerPrefs.GetString(charID);
		Debug.Log (charData);
		LoadedCharData = JsonUtility.FromJson<CharacterData> (charData);
		return 1;
	}


}
