using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager2 : MonoBehaviour {
	
	public GameObject SettingsMenu; 
	public GameObject HighScore; 
    public GameObject ToggleObject;

	private bool GameLaunched = false; 

	Text hscoretext;

	private Quaternion calibrationQuaternion; 

	void AccCalibration()
	{

		Vector3 accelerationSnapshot = Input.acceleration;

		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
		PlayerPrefs.SetFloat("quadx", calibrationQuaternion.x);
		PlayerPrefs.SetFloat("quady", calibrationQuaternion.y);
		PlayerPrefs.SetFloat("quadz", calibrationQuaternion.z);
		PlayerPrefs.SetFloat("quadw", calibrationQuaternion.w);
	}

	void Start()
	{
		Time.timeScale = 1.0f; 
		hscoretext = HighScore.GetComponent<Text>();
		hscoretext.text = "High score : " + PlayerPrefs.GetInt("HighScore");
		AccCalibration();

        ToggleObject.GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("revCon") == 1 ? true : false;
	}

    public void ReversedControls()
    {
        PlayerPrefs.SetInt("revCon", (ToggleObject.GetComponent<Toggle>().isOn == true ? 1 : 0));
    }

	public void LaunchGame()
	{
		GameLaunched = true;
	}

	public void ShowParam()
	{
		SettingsMenu.SetActive(!SettingsMenu.activeSelf);
	}

	public void SetAcc()
	{
		AccCalibration();
	}

	void Update()
	{

		if (GameLaunched)
		{
              SceneManager.LoadScene("Level_2");
			
		}
	}
}
