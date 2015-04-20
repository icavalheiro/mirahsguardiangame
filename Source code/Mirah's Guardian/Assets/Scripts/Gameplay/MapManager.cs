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

	public bool debugIaPath = true;
	public Map map;
	public List<RespawFlag> toRespaw = new List<RespawFlag>();

	public event Action onSpawningFinished;

	private List<AStar.Node> _pathableNodes = new List<AStar.Node>();
	
	void OnDrawGizmos()
	{
		if(debugIaPath == false)
			return;

		foreach(var node in _pathableNodes)
		{
			Gizmos.color = node.color;
			Gizmos.DrawSphere(node.nodeTransform.position + Vector3.up, 0.1f);
			
			if(node.parent != null)
				Gizmos.DrawLine(node.nodeTransform.position + Vector3.up, node.parent.nodeTransform.position + Vector3.up);
		}
	}

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


		//get pathable nodes
		List<TileObject> __iaWalkableTiles = map.GetIaWalkableTiles();
		foreach(TileObject tile in __iaWalkableTiles)
		{
			AStar.Node __node = new AStar.Node();
			__node.position = new Vector2(tile.transform.position.x, tile.transform.position.z);
			__node.nodeTransform = tile.transform;
			_pathableNodes.Add(__node);
		}
		
		
		//set up neighbours
		foreach(var node in _pathableNodes)
		{
			foreach(var possibleNeighbour in _pathableNodes)
			{
				if(possibleNeighbour == node)
					continue;
				
				//check to see if the possible neightbour is indeed a neighbour
				if((possibleNeighbour.position.x + 1 == node.position.x || possibleNeighbour.position.x - 1 == node.position.x || possibleNeighbour.position.x == node.position.x) &&
				   (possibleNeighbour.position.y + 1 == node.position.y || possibleNeighbour.position.y - 1 == node.position.y || possibleNeighbour.position.y == node.position.y))
					node.neighbours.Add(possibleNeighbour);
			}
		}

		if(onSpawningFinished != null)
			onSpawningFinished();
	}

	public List<AStar.Node> GetPathableNodes()
	{
		return _pathableNodes;
	}

	private string GetFlagName(Flags p_flag)
	{
		return p_flag.ToString().ToLower() + ".png";
	}
}