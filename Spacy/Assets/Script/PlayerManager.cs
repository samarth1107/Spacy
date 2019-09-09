using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	public float speed = 20.0f;
    public int ingame = 1;
	public GameObject PauseMenuobj;
	public GameObject GameOverMenu;
	public GameObject HighScoreObj;
	public GameObject ScoreObj;
	Text HscoreText;
	Text ScoreText;
	private float zrotation = 0.0f;
	private float xrotation = 0.0f;
	private int score = 0; 
	private bool alive = true; 
	private bool Paused = false;
	private bool GameLost = false; 
	private Quaternion calibration;
    bool revCon = false;


	void Start()
	{	
		calibration = new Quaternion(PlayerPrefs.GetFloat("quadx"), PlayerPrefs.GetFloat("quady"), PlayerPrefs.GetFloat("quadz"), PlayerPrefs.GetFloat("quadw"));

		HscoreText = HighScoreObj.GetComponent<Text>();
		ScoreText = ScoreObj.GetComponent<Text>();
		HscoreText.text = "High score : " + PlayerPrefs.GetInt("HighScore");
		ScoreText.text = "Score : " + score;

        revCon = PlayerPrefs.GetInt("revCon") == 1 ? true : false;
				
		
		

	}

	Vector3 FixAcceleration (Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibration * acceleration;
		return (fixedAcceleration);

	}

	void Update()
	{	

		

		Vector3 dir; // Where will the camera will point.
		Vector3 fixeddir; // Camera direction calibrated.
		Vector3 position; // Position of the player.

		if (!alive && !GameLost) 
		{
			GameLost = true; 

			Time.timeScale = 0.0f; 

			GameOverMenu.SetActive(true); 

			if (score > PlayerPrefs.GetInt("HighScore")) 
				PlayerPrefs.SetInt("HighScore", score);
		}

		if (!Paused && !GameLost) 
		{
			dir = new Vector3(0.0f, 0.0f, 0.0f); 
			position = transform.position; 

			speed += 0.25f * Time.deltaTime; 

			dir.x += Input.GetAxis("Horizontal");
			dir.y += Input.GetAxis ("Vertical");

			dir += Input.acceleration; 
			fixeddir = FixAcceleration(dir); 

			fixeddir.x = Mathf.Clamp(fixeddir.x * 3.0f, -1.5f, 1.5f);
			fixeddir.y = Mathf.Clamp(fixeddir.y * 3.0f, -1.5f, 1.5f);

            if (revCon)
                fixeddir.y = -fixeddir.y;

            zrotation = Mathf.Lerp(zrotation, -fixeddir.x / 10.0f, Time.deltaTime * 3f);
			xrotation = Mathf.Lerp(xrotation, -fixeddir.y / 10.0f, Time.deltaTime * 3f);

            position.x += fixeddir.x * speed * Time.deltaTime; 
			position.z += speed * Time.deltaTime; 
			position.y += fixeddir.y * speed * 0.66f * Time.deltaTime; 

			transform.position = position;
			transform.rotation = new Quaternion(xrotation, transform.rotation.y, zrotation, transform.rotation.w);
			
			ScoreText.text = "Score : " + score; 
			RenderSettings.ambientIntensity = 0.5f - ((float)score / 10000.0f); 
		}

        if (score>5 && ingame == 1)
        {
            SceneManager.LoadScene("Level_2");
            ingame = 2;
        }

        if (score>10 && ingame == 2)
        {
            SceneManager.LoadScene("Level_3");
            ingame = 3;
        }

	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		alive = false;		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="car")
		{
			score++;
			Destroy(other.gameObject);
  		}
	}

	public void PauseMenu()
	{
		if (!GameLost)
		{
			if (PauseMenuobj.activeSelf == true)
			{
				PauseMenuobj.SetActive(false);
				Paused = false;
				Time.timeScale = 1.0f;
			}
			else
			{
				PauseMenuobj.SetActive(true);
				Paused = true;
				Time.timeScale = 0.0f;
			}
		}
	}

	public void RetryButton()
	{
        SceneManager.LoadScene("Flaw");
    }

	public void MainMenuButton()
	{
        SceneManager.LoadScene("MainMenu");
	}
	
}
