using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour 
{

	public string couleur;
	public GameObject _creep;
	public GameObject target;

	// Use this for initialization
	void Start () 
	{
		target = _creep.GetComponent<CreepBehaviour>()._ennemyCurrent;
	}
	
	// Update is called once per frame
	void Update () 
	{
	


	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "RedCreep" && couleur == "BlueCreep")
		{
			if(_creep != null)
			{
				_creep.GetComponent<CreepBehaviour>()._list.Remove(this.gameObject);
				Destroy(this.gameObject);
			}

		}

		if(col.gameObject.tag == "BlueCreep" && couleur == "RedCreep")
		{
			if(_creep != null)
			{
				_creep.GetComponent<CreepBehaviour>()._list.Remove(this.gameObject);
				Destroy(this.gameObject);
			}
		}
			
	}
}
