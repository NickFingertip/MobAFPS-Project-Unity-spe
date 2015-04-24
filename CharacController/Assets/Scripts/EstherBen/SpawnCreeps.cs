using UnityEngine;
using System.Collections;

public class SpawnCreeps : MonoBehaviour 
{

	public bool _Blue;


	//variables de spawning
	public GameObject _creepPrefab;
	public float _spawnRate = 1;
	float _nextSpawn = 0;
	int _creepCount;

	//nombre de creeps par wave
	public int _CreepWave = 10;

	//Creep qui spawn
	public GameObject _creep;

	private GameObject _waveManager;

	private int test = 0;

	//====================================================================================================================
	void Start () 
	{
		if(_creepPrefab.tag == "RedCreep")
		{
			_waveManager = GameObject.Find ("WaveManagerRed");
		}
		if(_creepPrefab.tag == "BlueCreep")
		{
			_waveManager = GameObject.Find ("WaveManagerBlue");
		}
	}
	//====================================================================================================================

	//====================================================================================================================
	void Update () 
	{
		//spawn de creep
		if (Time.time > _nextSpawn && _creepCount < _CreepWave) 
		{
			_nextSpawn = Time.time + _spawnRate;
			if (_Blue)
			{
				_creep = PhotonNetwork.Instantiate("CreepBlue", transform.position, transform.rotation, 0) as GameObject;
			}
			else
			{
				_creep = PhotonNetwork.Instantiate("CreepRed", transform.position, transform.rotation, 0) as GameObject;
			}
			_waveManager.GetComponent<WaveManager>()._whoDoIFollow.Add(_creep);
			_creepCount++;
		}

		if(_creepCount > 9 && test == 0)
		{
			test++;
			StartCoroutine(MyMethod());
		}

	}
	//====================================================================================================================

	IEnumerator MyMethod() 
	{
		yield return new WaitForSeconds(20);
		_creepCount = 0;
		test = 0;
	}
}
