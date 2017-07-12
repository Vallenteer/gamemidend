using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCharSet : MonoBehaviour {
	public CharacterData[] CharacterDatas;
	CharacterData[] EnemyDatas;

	void Awake() {
		if (StaticVars.isFirstDataInitialized)
			return;
		//input character manually
		CharacterDatas = new CharacterData[3];
		EnemyDatas = new CharacterData[1];

		/// 
		CharacterDatas [0] = new CharacterData ("001", "Ryu", 100, 1, 5f, 10f, 1f);
		CharacterDatas [1] = new CharacterData ("002", "Ken", 200, 1, 6f, 10f, 1f);
		CharacterDatas [2] = new CharacterData ("003", "Boar", 200, 1, 6f, 10f, 1f);
		CharacterData.SaveCharacterData (CharacterDatas);

		///
		EnemyDatas [0] = new CharacterData ("E001", "Enemy1", 100, 1, 5f, 10f, 1f);
		CharacterData.SaveCharacterData (EnemyDatas);
	}

}
