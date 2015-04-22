using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class AStar
{
	public class Node
	{
		public Vector2 position;
		public List<Node> neighbours = new List<Node>();
		public Transform nodeTransform;
		public Node parent;
		public int step = -1;

		public Color color;

		public Node()
		{
			//color = new Color(UnityEngine.Random.Range(0f,1.0001f), UnityEngine.Random.Range(0f, 1.00001f), UnityEngine.Random.Range(0f,1.0001f)); 
			color = Color.blue;
		}
	}

	private List<Vector2> _pathToFollow = new List<Vector2>();
	private Node _startNode;
	private Node _endNode;
	private int _lastPointDelivered = -1;

	public Action onPathProcessed;

	public string tagName = "";

	public bool isEndOfPath { get { return _lastPointDelivered >= (_pathToFollow.Count -1); } }

	//dont forget to set up the node neigbours while creating the node list to use here
	public AStar(Vector2 p_initial, Vector2 p_final, List<Node> p_nodes)
	{

		if(p_nodes.Count < 2)
		{
			Debug.LogError("Something wrong with the node list");
			return;
		}

		//detect first and final node
		//also sets up the nodes
		Node __endNode = null;
		Node __startNode = null;
		{
			float __currentDistanceStart = Mathf.Infinity;
			float __currentDistanceEnd = Mathf.Infinity;

			foreach(var node in p_nodes)
			{
				float __distanceStart = Vector2.Distance(node.position, p_initial);
				float __distanceFinal = Vector2.Distance(node.position, p_final);

				if(__distanceStart < __currentDistanceStart)
				{
					__currentDistanceStart = __distanceStart;
					__startNode = node;
				}

				if(__distanceFinal < __currentDistanceEnd)
				{
					__currentDistanceEnd = __distanceFinal;
					__endNode = node;
				}

				//set up node for algorithm
				node.parent = null;
				node.step = -1;
				node.color = Color.blue;
			}
		}

		_endNode = __endNode;
		_startNode = __startNode;
		_endNode.color = Color.white;
		_startNode.color = Color.black;

	}

	public IEnumerator ProcessPath()
	{
		//float __secondsToWait = 0f;
		yield return null;

		//set up variables
		bool __pathFound = false;
		{
			List<Node> __lockedNodes = new List<Node>();
			_startNode.step = __lockedNodes.Count;
			__lockedNodes.Add(_startNode);

			Node __currentNode = _startNode;

			//start algorithm
			while(__pathFound == false && __currentNode != null)
			{
				__currentNode.color = Color.red;

				//yield return new WaitForSeconds(__secondsToWait);

				Node __closestNode = null;
				float __lowestDistance = Mathf.Infinity;
				foreach(var neighbour in __currentNode.neighbours)
				{
					if(neighbour == _endNode)
					{
						_endNode.parent = __currentNode;
						_endNode.step = __lockedNodes.Count;
						__lockedNodes.Add(_endNode);
						__pathFound = true;
						__currentNode.neighbours.ForEach(x =>
						                                 {
							if(__lockedNodes.Contains(x) == false)
								x.color = Color.blue;
						});
						break;
					}
					
					if(__lockedNodes.Contains(neighbour))
					{
						if(neighbour.step < __currentNode.parent.step)
							__currentNode.parent = neighbour;
					}
					else
					{
						neighbour.color = Color.yellow;
						float __distance = Vector2.Distance(neighbour.position, _endNode.position);
						if(__distance < __lowestDistance)
						{
							__lowestDistance = __distance;
							__closestNode = neighbour;
						}

						//yield return new WaitForSeconds(__secondsToWait);
					}
				}
				
				if(__closestNode == null)
				{
					__currentNode = __currentNode.parent;
				}
				else
				{
					__closestNode.parent = __currentNode;
					__closestNode.step = __lockedNodes.Count;
					__lockedNodes.Add(__closestNode);
					__currentNode.color = Color.white;
					__currentNode.neighbours.ForEach(x =>
					{
						if(__lockedNodes.Contains(x) == false)
							x.color = Color.blue;
					});
					__currentNode = __closestNode;
				}
			}
		}

		if(__pathFound)
		{
			List<Vector2> __pathToFollow = new List<Vector2>();
			Node __currentNode = _endNode;
			while(__currentNode.parent != null)
			{
				__currentNode.color = Color.black;
				//yield return new WaitForSeconds(__secondsToWait);
				__pathToFollow.Add(__currentNode.position);
				__currentNode = __currentNode.parent;
			}

			this._pathToFollow = __pathToFollow;

			_pathToFollow.Reverse();

			if(onPathProcessed != null)
				onPathProcessed();
		}
	}

	public Vector2 GetNextPoint()
	{
		_lastPointDelivered ++;

		if(isEndOfPath)
			_lastPointDelivered = _pathToFollow.Count -1;

		return _pathToFollow[_lastPointDelivered];
	}
}
