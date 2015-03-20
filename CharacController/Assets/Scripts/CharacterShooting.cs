using UnityEngine;
using System.Collections;

public class CharacterShooting : MonoBehaviour {

	public GameObject fireBall;
	private GameObject[] ballTable;

	void Awake(){
		ballTable = new GameObject[30];

		for(int i = 0; i < 30; i++){
			GameObject newBall ;
			newBall = Instantiate(fireBall) as GameObject;
			ballTable[i] = newBall;
			newBall.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			GameObject shootedBall = getFireBall();
			shootedBall.transform.position = transform.position + Camera.main.transform.forward;
			shootedBall.SetActive(true);

			Rigidbody RB = shootedBall.GetComponent<Rigidbody>();

			RB.velocity = Vector3.zero;
			RB.AddForce(Camera.main.transform.forward * 50.0f, ForceMode.Impulse);
		}
	}

	private GameObject getFireBall(){
		foreach(GameObject g in ballTable){
			if(!g.activeSelf){
				return g;
			}
		}

		return null;
	}
}
