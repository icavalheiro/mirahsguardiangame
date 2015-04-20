using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour 
{
	private static GameplayManager _singleton;

	public MapManager mapManager;
	public Camera mainCamera;

	public Mirah mirah;
	public Guardian guardian;
	public List<Enemy> enemies = new List<Enemy>();

	void Awake()
	{
		if(_singleton != null)
			Destroy(_singleton.gameObject);
		_singleton = this;

		mapManager.onSpawningFinished += () =>  
		{
			Vector3 __currentCameraPosition = mainCamera.transform.position;
			mainCamera.transform.parent = guardian.transform;
			mainCamera.transform.localPosition = __currentCameraPosition;
		};
	}
	
	void Start()
	{
		var __iaWalkableNodes =  mapManager.GetPathableNodes();
		//set path to mirah
		Vector2 __startMirahsPosition = new Vector2(mirah.transform.position.x, mirah.transform.position.z);
		Vector2 __finalMirahsPosition = mapManager.map.GetTileObjectsByImageName("cyan.png")[0].position;
		AStar __mirahsPath = new AStar(__startMirahsPosition, __finalMirahsPosition, __iaWalkableNodes);

		__mirahsPath.onPathProcessed += () =>
		{			
			mirah.SetPathToFollow(__mirahsPath);
		};
		StartCoroutine(__mirahsPath.ProcessPath());
	}

	public static void RegisterCharacter(Character p_toRegister)
	{
		if(_singleton == null)
		{
			Debug.LogError("GameplayManager singleton was null!");
			return;
		}

		switch(p_toRegister.type)
		{
		case Character.Type.ENEMY:
			Enemy __enemy = p_toRegister.GetComponent<Enemy>();
			if(__enemy == null)
				return;

			if(_singleton.enemies.Contains(__enemy) == false)
				_singleton.enemies.Add(__enemy);

			break;

		case Character.Type.GUARDIAN:
			Guardian __guardian = p_toRegister.GetComponent<Guardian>();
			if(__guardian == null)
				return;

			_singleton.guardian = __guardian;
			break;

		case Character.Type.MIRAH:
			Mirah __mirah = p_toRegister.GetComponent<Mirah>();
			if(__mirah == null)
				return;

			_singleton.mirah = __mirah;
			break;
		}

	}
}
