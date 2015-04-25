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
		mirah.onReachPortal += () =>  EndGameSuccessfuly();
		mirah.onDead += () => EndGameWithFail();
		mirah.InformMapNodes (mapManager.GetPathableNodes ());
		mirah.InformEndPortalPosition (mapManager.GetPositionForEndPortal ());
		mirah.WalkToTheEndPortal ();

		guardian.onDead += () => EndGameWithFail ();
		guardian.onMirahCalled += () =>
		{
			if(Vector3.Distance(guardian.transform.position, mirah.transform.position) < 6)
				mirah.RunToPosition(guardian.Get2DPosition());
		};
	}

	private void EndGameSuccessfuly()
	{
		Debug.Log("Level completed");
	}

	private void EndGameWithFail()
	{
		Debug.Log ("Level failed!");
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
