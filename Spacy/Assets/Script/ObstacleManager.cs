using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{	

	public GameObject player;

    public GameObject[] buildingList;

	public GameObject car;
    public float carSpeed = 10f;


	private GameObject[] buildingIns; 
	private GameObject[] carIns; 
	private int Building_Number = 18; 
	private int Car_Number = 72; 
	private int shift = 0; 

	int[] GetRandomArray(int length)
	{
		int[] style = new int[length];
        int	i = -1; 

		while (++i < length)
			style[i] = i;

        i = -1;
        while (++i < length)
        {
            int random = (int)Mathf.Floor(Random.Range(0.0f, length - 0.01f));
            int tmp = style[random];
            style[random] = style[i];
            style[i] = tmp;
        }
		return (style);
	}

	void SpawnBuilding()
	{
        buildingIns = new GameObject[Building_Number]; 
		int[] style; 
		int i = 0;
		int	j = 0; 
		int	x = 0; 

		Vector3 pos = new Vector3(-125.0f, 0.0f, 150.0f);
		while (i < Building_Number)
		{
			j = i - 1;
			x = 0;
			shift++;
			style = GetRandomArray(6); 
			while (++j < i + 6)
			{
                buildingIns[j] = Instantiate(buildingList[style[j % 6]], pos, Quaternion.identity) as GameObject;
                buildingIns[j].transform.eulerAngles = new Vector3((Random.value > 0.5f ? 180f : 0f), (Random.value > 0.5f ? 180f : 0f), 0.0f);
				pos.x += 50.0f;
				x++;
			}
			pos.x = -125.0f;
			pos.x += (shift % 2 == 0 ? 0.0f : -25.0f); 
			pos.z += 75.0f;
			i += 6;
		}
	}

	void SpawnCar()
	{
		carIns = new GameObject[Car_Number];
		int	i = 0;
		int j = 0;
		Vector3 pos = new Vector3(-200.0f, 0.0f, 187.5f);

		while (i < Car_Number)
		{
			j = i - 1;
            int[] carY = GetRandomArray(24);

            while (++j < i + 24)
            {
                Vector3 carPos = pos;
                carPos.y += (carY[j % 24] - 12) * 5f;
                carIns[j] = Instantiate(car, carPos, Quaternion.identity) as GameObject; 
                pos.x += 15.0f;               
			}
			pos.z += 75.0f; 
			pos.x = -200.0f;
			i += 24;
		}
	}

	void ManageBuilding()
	{
		int i = 0; 
		int	j = 0; 
        int[] style; 
		Vector3 newCoord = player.transform.position; 
		newCoord.x = (int)newCoord.x - (((int)newCoord.x % 50) + 125.0f);
		newCoord.z = Mathf.Floor(newCoord.z) + 175.0f;
		newCoord.y = Mathf.Floor(newCoord.y);
		while (i < Building_Number)
		{
			if (buildingIns[i].transform.position.z < player.transform.position.z - 50.0f)
			{
				j = -1;
				newCoord.x += (shift % 2 == 0 ? 0.0f : -25.0f); 
				shift++;
				style = GetRandomArray(6);
				while (++j < 6)
				{
                    buildingIns[i + style[j]].transform.position = newCoord;
					if (Random.value > 0.5f)
                        buildingIns[i + style[j]].transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					else
                        buildingIns[i + style[j]].transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					newCoord.x += 50.0f;
				}
				return ;
			}
			i += 6;
		}
	}

	void ManageCar()
	{
		int	i = 0;
		int	j = 0;
		Vector3 newCoord = player.transform.position;
		Vector3 pos;

		newCoord.x = (int)newCoord.x - (((int)newCoord.x % 50) + 200.0f);
		newCoord.z = Mathf.Floor(newCoord.z) + 175.0f;
		newCoord.y = Mathf.Floor(newCoord.y);
		while (i < Car_Number)
		{
			j = i - 1;

            int[] carY = GetRandomArray(24);
            while (++j < i + 24)
			{
				pos = carIns[j].transform.position;
				if ((int)j % 2 == 0)
					pos.x += carSpeed * Time.deltaTime;
				else
					pos.x -= carSpeed * Time.deltaTime;
                carIns[j].transform.position = pos;
			}
			if (carIns[i].transform.position.z < player.transform.position.z - 50.0f)
			{
				j = i - 1;
				while (++j < i + 24)
				{
                    Vector3 carPos = newCoord;
                    carPos.y += (carY[j % 24] - 12) * 5f;
                    carIns[j].transform.position = carPos;
					newCoord.x += 15.0f;
				}
			}
			i += 24;
		}
		
	}

	void Awake()
	{
		Time.timeScale = 1.0f; 
		RenderSettings.fogEndDistance = 0.0f; 
		SpawnBuilding();
		SpawnCar();
	}
	
	void Update()
	{
		float fog = RenderSettings.fogEndDistance;
		if (fog < 150.0f) 
		{
			fog += 150.0f * Time.deltaTime;
			RenderSettings.fogEndDistance = fog; 
		}
		if ((int)Time.frameCount % 2 == 0)
			ManageBuilding();
		else
			ManageCar();
	}
}