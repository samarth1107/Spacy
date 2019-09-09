using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float counter = 10.0f;

	void Awake()
	{
		Destroy(this.gameObject, counter);
	}
}
