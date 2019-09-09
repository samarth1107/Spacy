using UnityEngine;
using System.Collections;

public class Cameramovement : MonoBehaviour {

	
	private float zrotation = 0.0f;
	private float xrotation = 0.0f;

	private Quaternion calibration; 

	void Start()
	{
		calibration = new Quaternion(PlayerPrefs.GetFloat("quadx"), PlayerPrefs.GetFloat("quady"), 
		                             PlayerPrefs.GetFloat("quadz"), PlayerPrefs.GetFloat("quadw"));
	}

	Vector3 FixAcceleration (Vector3 acceleration)
	{		
		Vector3 fixedAcceleration = calibration * acceleration;
		return (fixedAcceleration);
	}

	void Update()
	{
		Vector3 dir; 
		Vector3 fixeddir; 
		Vector3 cam_pos; 

        calibration = new Quaternion(PlayerPrefs.GetFloat("quadx"), PlayerPrefs.GetFloat("quady"),
                                     PlayerPrefs.GetFloat("quadz"), PlayerPrefs.GetFloat("quadw"));

        cam_pos = transform.position;
		cam_pos.y = (Mathf.Cos(Time.time * 0.1f) * 90.0f) - 10.0f;
		transform.position = cam_pos;	

		dir = new Vector3(0.0f, 0.0f, 0.0f); 
		dir.x += Input.GetAxis("Horizontal");
		dir.y += Input.GetAxis ("Vertical");	
		dir += Input.acceleration;
		fixeddir = FixAcceleration(dir); 
		fixeddir.x = Mathf.Clamp(fixeddir.x * 3.0f, -1.0f, 1.0f);
		fixeddir.y = Mathf.Clamp(fixeddir.y * 3.0f, -1.0f, 1.0f);      

        if (PlayerPrefs.GetInt("revCon") == 1)
            fixeddir.y = -fixeddir.y;

        zrotation = Mathf.Lerp(zrotation, -fixeddir.x / 10.0f, 0.5f);
		xrotation = Mathf.Lerp(xrotation, -fixeddir.y / 10.0f, 0.5f);
		transform.rotation = new Quaternion(xrotation, transform.rotation.y, zrotation, transform.rotation.w);
	}
}
