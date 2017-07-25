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
	public float CooldownMGC;
	public float CooldownULT;
	public float AtkPower;
	public int CurExp;
	public int TargetExp;

	public CharacterData () {
		charID = null;
		HP = 0;
		Namae = null;
		HP = 0;
		Level = 1;
		CooldownMGC = 0;
		CooldownULT = 0;
		AtkPower = 0;
		CurExp = 0;
		TargetExp = 0;
	}

	public CharacterData (string id, string name, int hp, int level, float cooldown1, float cooldown2, float atkpower) {
		charID = id;
		Namae = name;
		HP = hp;
		Level = level;
		CooldownMGC = cooldown1;
		CooldownULT = cooldown2;
		AtkPower = atkpower;
		TargetExp = CurrTargetExp (level);
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

	public int CurrTargetExp(int level) {
		float c1 = 5f;
		float c2 = 6f;
		return Mathf.CeilToInt(Mathf.Pow (c1, (1 + level / c2)));
	}

	public bool LevelUp() {
		if (Level == 15)
			return false;
		
		Level += 1;
		TargetExp = CurrTargetExp (Level);
		return true;
	}
}
