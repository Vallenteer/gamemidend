using UnityEngine;
using System.Collections;

public class BackgroundBehavior : MonoBehaviour {

	public float Speed = 5f;
	void Update () {

		MeshRenderer mr = GetComponent<MeshRenderer>();

		Material mat = mr.material;

		Vector2 offset = mat.mainTextureOffset;

		if (offset.x >= 10)
			offset.x = 0;
		offset.x += Time.deltaTime / Speed; // beda sma BG1 itu dri speed aj .-.

		mat.mainTextureOffset = offset;

	}
}
