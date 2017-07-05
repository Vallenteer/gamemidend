using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCharSet : MonoBehaviour {
	public CharacterData[] CharacterDatas;

	void Awake() {
		if (StaticVars.isFirstDataInitialized)
			return;
		//input character manually
		CharacterDatas = new CharacterData[2];

		/*
		CharacterDatas [0].charID = "001";
		CharacterDatas [0].HP = 100;
		CharacterDatas [0].MP = 200;
		*/

		/// 
		CharacterDatas [0] = new CharacterData ("001", 100, 200);
		CharacterDatas [1] = new CharacterData ("002", 200, 400);
		CharacterData.SaveCharacterData (CharacterDatas);

	}

}
