using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepBehaviour : MonoBehaviour 
{
	//variables navmesh
	private NavMeshAgent _agent;
	private GameObject _destination;
	
	//variables de l'animator
	Animator _animator;
	AnimatorStateInfo _currentState;

	//Variables du target que le creep va attaquer
	//public GameObject _target;
	private Detector _detector;
	//private bool _lock = false;

	//Pour le wave Manager qui permet aux creeps d'aller en ligne
	private GameObject _waveManager;
	public int  _currentIndex;

	//Vie du creep
	public int _life = 10;
	//Attaque du creep
	public int _damage = 5;//Degats
	public float _coolDown = 2f; //Cooldown
	public GameObject _ennemyCurrent;//Ennemi a attaquer
	//Spawn fireball
	public GameObject _FireBallPrefab;
	public int _movementSpeed;
	private float _step;
	public List<GameObject> _list;

	void Awake()
	{

	}

	//====================================================================================================================
	void Start () 
	{
		//Animator components
		_animator = GetComponent<Animator> ();
		_detector = GetComponent<Detector> ();

		//NavMesh
		_agent = this.GetComponent<NavMeshAgent> ();

		//mise en place de la destination des IA au start pour les creep rouges
		if (this.tag == "RedCreep") 
		{
			_destination = GameObject.Find ("SpawnerBlue");
		}
		//puis pour les creeps bleus
		if (this.tag == "BlueCreep") 
		{
			_destination = GameObject.Find ("SpawnerRed");
		}

		//Pour le wave manager
		if(this.tag == "RedCreep")
		{
			_waveManager = GameObject.Find ("WaveManagerRed");
		}
		if(this.tag == "BlueCreep")
		{
			_waveManager = GameObject.Find ("WaveManagerBlue");
		}

		
	}
	//====================================================================================================================

	//====================================================================================================================
	void Update () 
	{
		//Pour detruire le creep
		if(_life <= 0)
		{
			foreach (GameObject bouleDeFeu in _list)
			{
				Destroy(bouleDeFeu.gameObject);
			}
			//Update who do I follow to update the index
			_waveManager.GetComponent<WaveManager>()._whoDoIFollow.Remove(this.gameObject);
			//destroy IA
			Destroy(this.gameObject);
		}

		//Pour animator
		_currentState = _animator.GetCurrentAnimatorStateInfo (0);

		//modification du behaviour en fonction de l'état de l'animator
		_currentIndex = (_waveManager.GetComponent<WaveManager>()._whoDoIFollow.IndexOf(this.gameObject));

		if (_currentState.IsName ("Base Layer.Walking")) 
		{
			if((_waveManager.GetComponent<WaveManager>()._whoDoIFollow.Count > 1) && (_agent != null) &&  (_waveManager.GetComponent<WaveManager>()._whoDoIFollow[_currentIndex -1] != null))
			{
				_agent.SetDestination (_waveManager.GetComponent<WaveManager>()._whoDoIFollow[_currentIndex -1].transform.position);
			}
		}

		if (_currentState.IsName ("Base Layer.Attacking") && _life > 0) 
		{
			if(this != null)
			{
				if(_agent != null)
				{
					_agent.SetDestination (transform.position);

				}
			}
			_coolDown -= Time.deltaTime;
			if(_coolDown <= 0)
			{
				//ajouter la condition de timer
				GameObject FireBall = Instantiate(_FireBallPrefab, this.transform.position, this.transform.rotation) as GameObject;
				FireBall.GetComponent<FireBallScript>().couleur = this.tag;
				FireBall.GetComponent<FireBallScript>()._creep = this.gameObject;
				_list.Add(FireBall.gameObject);
				if(GetComponent<Detector>()._myEnnemiesCurrent.Count != 0)
				{
					_ennemyCurrent = GetComponent<Detector>()._myEnnemiesCurrent[0];
				}

				if(_ennemyCurrent.gameObject != null)
				{
					_ennemyCurrent.GetComponent<CreepBehaviour>()._life -= _damage;
					_coolDown = 1f;
				}
			}
			if(_ennemyCurrent != null)
			transform.LookAt(_ennemyCurrent.transform);
		}

		//--------- script d'instantiate des boules de feu
		_step = _movementSpeed * Time.deltaTime;

		for (int i = 0; i< _list.Count; i++) 
		{
			GameObject bouleDeFeu = _list[i];

			if(_ennemyCurrent == null)
			{
				_list.Remove(bouleDeFeu);
				i--;
				Destroy(bouleDeFeu);
				Debug.Log("detruire boule");
			}

			if(_ennemyCurrent != null)
			{
				bouleDeFeu.transform.position = Vector3.MoveTowards(bouleDeFeu.transform.position, _ennemyCurrent.transform.position, _step);
			}

			/*else if((bouleDeFeu.transform.position.magnitude -  _ennemyCurrent.transform.position.magnitude) < 1f)
			{
				_list.Remove(bouleDeFeu);
				i--;
				Destroy(bouleDeFeu);
				Debug.Log("detruire boule2");
			}*/
		}
	}
	//====================================================================================================================


}
