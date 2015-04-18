using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapManager : MonoBehaviour 
{
	public enum Flags
	{
		BLUE,
		CYAN,
		YELLOW,
		WHITE,
		BLACK,
		GREEN,
		ORANGE,
		PINK,
		RED
	}

	[System.Serializable]
	public class RespawFlag
	{
		public Flags flag;
		public GameObject respaw;
	}

	public Map map;
	public List<RespawFlag> toRespaw = new List<RespawFlag>();

	public event Action onSpawningFinished;

	void Start()
	{
		//change flags for the gameObject they represent
		toRespaw.ForEach(x => 
		{
			var __flagOfThisColorInScreen = map.GetTileObjectsByImageName(GetFlagName(x.flag));
			while(__flagOfThisColorInScreen.Count > 0)
			{
				var __flagObj = __flagOfThisColorInScreen[0];
				__flagOfThisColorInScreen.Remove(__flagObj);

				Vector3 __positionToSpawn = __flagObj.transform.position;
				Destroy(__flagObj.gameObject);

				GameObject.Instantiate(x.respaw, __positionToSpawn, x.respaw.transform.rotation);
			}
		});

		if(onSpawningFinished != null)
			onSpawningFinished();
	}

	private string GetFlagName(Flags p_flag)
	{
		return p_flag.ToString().ToLower() + ".png";
	}
}
