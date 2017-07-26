using UnityEngine;
using System.Collections;

public class HitColider : MonoBehaviour {
	public enum DamageType
	{
		NONE,
		ATK,
		MGC,
		ULT
	};

	public string punchName;
	public float damage;
	public DamageType damageType;

	public Fighter owner;

	public void UpdateDamage() {
		switch (damageType) {
		case DamageType.ATK:
			if (CharacterData.LoadedCharData.AtkPower > 0) {
				damage = CharacterData.LoadedCharData.AtkPower;
			} else {
				CharacterData.LoadedCharData.AtkPower = damage;
			}
			break;

		case DamageType.MGC:
			if (CharacterData.LoadedCharData.MgcPower > 0) {
				damage = CharacterData.LoadedCharData.MgcPower;
			} else {
				CharacterData.LoadedCharData.MgcPower = damage;
			}
			break;

		case DamageType.ULT:
			if (CharacterData.LoadedCharData.UltiPower > 0) {
				damage = CharacterData.LoadedCharData.UltiPower;
			} else {
				CharacterData.LoadedCharData.UltiPower = damage;
			}
			break;

		case DamageType.NONE:
			break;
		}
	}

	void OnTriggerEnter(Collider other){
		Fighter somebody = other.gameObject.GetComponent<Fighter> ();
		if (owner.attacking) {
			if (somebody != null && somebody != owner) {
				somebody.hurt (damage);
			}
		}
	}
}
