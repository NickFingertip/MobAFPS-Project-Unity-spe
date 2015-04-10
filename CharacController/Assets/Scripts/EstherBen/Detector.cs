using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//IMPORTANT
//SORT ENNEMIES BY NEARIEST IN THE UPDATING ENNEMIES POUR EQUILIBRER LE JEU
// REGARDER SORT LIST
//ENNEMI LE PLUS PROCHE AU CREEP
//IMPORTANT

public class Detector : MonoBehaviour 
{

	//liste des ennemis détectés
	public List<GameObject> _myEnnemiesUpdating;
	public List<GameObject> _myEnnemiesCurrent;

	//variables de l'animator
	Animator _animator;


	//====================================================================================================================
	void Start () 
	{
		_animator = GetComponent<Animator> ();
	}
	//====================================================================================================================

	//====================================================================================================================
	void Update () 
	{
		//A chaque frame on appelle la detection d'ennemis
		DetectionUpdate ();

		//Si le creep trouve des ennemis activer le comportement attacking
		if (_myEnnemiesCurrent.Count != 0) 
		{
		
			_animator.SetBool("TargetFound", true);
		}
		//Si le creep ne trouve pas des ennemis activer le comportement walking
		if (_myEnnemiesCurrent.Count == 0) 
		{
			
			_animator.SetBool("TargetFound", false);
		}

		//Ceci nous permet d'update la liste myEnnemiesCurrent qui va affecter creepBehaviour
		//Si la liste myEnnemiesCurrent est différente a _myEnnemiesUpdating
		//myEnnemiesCurrent va etre supprimee et remplacee par la liste _myEnnemiesUpdating
		if (_myEnnemiesCurrent.Count != 0 && _myEnnemiesCurrent != _myEnnemiesUpdating)
		{
			_myEnnemiesCurrent.Clear ();
		}

		if (_myEnnemiesCurrent != _myEnnemiesUpdating) 
		{
			_myEnnemiesCurrent.AddRange (_myEnnemiesUpdating);
		}

	}
	//====================================================================================================================

	//====================================================================================================================
	void DetectionUpdate()
	{
		_myEnnemiesUpdating.Clear ();

		//Si l'ennemi se trouve dans ma range le mettre dans la liste _myEnnemiesUpdating
		Collider[] hitObjects = Physics.OverlapSphere(transform.position, 2);
		if (this.tag == "RedCreep") 
		{
			foreach(Collider hit in hitObjects)
			{
				if(hit.tag == "BlueCreep")
				{
					if(!_myEnnemiesUpdating.Contains(hit.gameObject))
					{
						_myEnnemiesUpdating.Add(hit.gameObject);
					}
				}
			}
		}

		if (this.tag == "BlueCreep") 
		{
		
			foreach(Collider hit in hitObjects)
			{
				if(hit.tag =="RedCreep")
				{
					if(!_myEnnemiesUpdating.Contains(hit.gameObject))
					{
						_myEnnemiesUpdating.Add(hit.gameObject);
					}
				}
			}
		}
	}
	//====================================================================================================================
}
