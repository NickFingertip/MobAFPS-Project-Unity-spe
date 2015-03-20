using UnityEngine;
using System.Collections;

public class FireBallBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		Invoke("AutoDisable", 5.0f);
	}

	void AutoDisable(){
		gameObject.SetActive(false);
	}
}
