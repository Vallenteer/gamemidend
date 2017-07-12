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
	public string Namae;
	public int HP;
	public int Level;
	public float CooldownSkill1;
	public float CooldownSkill2;
	public float AtkPower;


	public CharacterData () {
		charID = null;
		HP = 0;
		Namae = null;
		HP = 0;
		Level = 1;
		CooldownSkill1 = 0;
		CooldownSkill2 = 0;
		AtkPower = 0;
	}

	public CharacterData (string id, string name, int hp, int level, float cooldown1, float cooldown2, float atkpower) {
		charID = id;
		Namae = name;
		HP = hp;
		Level = level;
		CooldownSkill1 = cooldown1;
		CooldownSkill2 = cooldown2;
		AtkPower = atkpower;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	public static void SaveCharacterData(string char_id, int HP, int MP) {
		//save (more like set) the entire database into 
		CharacterData cd = new CharacterData();
		cd.charID = char_id;
		cd.HP = HP;

		string charData = JsonUtility.ToJson (cd);
		//Debug.Log (charData);
		PlayerPrefs.SetString (char_id, charData);
	}*/

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
