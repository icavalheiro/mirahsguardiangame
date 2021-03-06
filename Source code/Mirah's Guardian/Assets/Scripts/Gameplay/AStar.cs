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
			color = Color.blue;
		}
	}

	private Node _startNode;
	private Node _endNode;

	public Action<List<Vector2>> onPathProcessed;

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
		bool __debugProcess = false;
		float __secondsToWait = 0f;
		yield return null;

		//set up variables
		bool __pathFound = false;
		{
			List<Node> __lockedNodes = new List<Node>();
			List<Node> __forcedNodes = new List<Node>();
			_startNode.step = __lockedNodes.Count;
			__lockedNodes.Add(_startNode);

			Node __currentNode = _startNode;

			//start algorithm
			while(__pathFound == false && __currentNode != null)
			{
				__currentNode.color = Color.red;

				if(__debugProcess)
					yield return new WaitForSeconds(__secondsToWait);

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
						if(__currentNode.parent != null)
							if(neighbour.step < __currentNode.parent.step)
								if(neighbour.parent != __currentNode)
									__currentNode.parent = neighbour;
					}
					else if (__forcedNodes.Contains(neighbour) == false)
					{
						neighbour.color = Color.yellow;
						float __distance = Vector2.Distance(neighbour.position, _endNode.position);
						if(__distance < __lowestDistance)
						{
							__lowestDistance = __distance;
							__closestNode = neighbour;
						}

						if(__debugProcess)
							yield return new WaitForSeconds(__secondsToWait);
					}
				}

				Action __setUpNextNode = () =>
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
				};

				if(__closestNode == null)
				{
					int __counter = 0;
					while(__forcedNodes.Contains(__currentNode.neighbours[__counter]))
						__counter ++;

					if(__counter < __currentNode.neighbours.Count)
					{
						__closestNode = __currentNode.neighbours[__counter];
						__setUpNextNode();
					}
					else
					{
						__currentNode.color = Color.white;
						__currentNode.step = int.MaxValue;
						__currentNode = __currentNode.parent;
					}
				}
				else
				{
					__setUpNextNode();
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

				if(__debugProcess)
					yield return new WaitForSeconds(__secondsToWait);

				__pathToFollow.Add(__currentNode.position);
				__currentNode = __currentNode.parent;
			}

			__pathToFollow.Reverse();

			if(onPathProcessed != null)
				onPathProcessed(__pathToFollow);
		}
	}
}
