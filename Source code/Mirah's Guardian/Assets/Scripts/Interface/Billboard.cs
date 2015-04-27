using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public GameObject m_Camera;

	void Start () {
		m_Camera = GameObject.Find ("Main Camera");
	}
	
	void Update () {
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
	}
}
