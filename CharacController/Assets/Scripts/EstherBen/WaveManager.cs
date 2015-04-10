using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour 
{

	public List<GameObject> _whoDoIFollow;
	public GameObject _spawner;
	public GameObject _target;

	// Use this for initialization
	void Start () 
	{
		_whoDoIFollow.Add(_target);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
