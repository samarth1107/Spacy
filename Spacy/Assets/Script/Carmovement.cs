using UnityEngine;
using System.Collections;

public class Carmovement : MonoBehaviour {
	
	public AudioSource car_collection;
	void Awake () 
	{
		if ((int)transform.position.z % 2 == 0)
			transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
		else
			transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);		

		car_collection = GetComponent<AudioSource>();

	}
}
