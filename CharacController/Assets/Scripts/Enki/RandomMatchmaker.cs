using UnityEngine;
using System.Collections;

public class RandomMatchmaker : MonoBehaviour {


	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.7");
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		//GUILayout.Label(PhotonNetwork.room.ToString());
	}
	
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom ("Room1");
	} 
	
	void OnJoinedRoom ()
	{
		Debug.Log ("Room Joined");
		Debug.Log (PhotonNetwork.room);

		GameObject Player = PhotonNetwork.Instantiate ("Player", new Vector3(Random.Range(-10.0f, 10.0f), 10, Random.Range(-10.0f, 10.0f)), Quaternion.identity, 0) as GameObject;


		/*
		move _mo = Player.GetComponent<move> ();
		MouseLook _ML = Player.GetComponent<MouseLook> ();
		MouseLook _ML2 = Player.transform.Find ("Camera").GetComponent<MouseLook> ();
		Camera _cam = Player.transform.Find ("Camera").GetComponent<Camera> ();
		Camera _camui =  Player.transform.Find ("CameraUI").GetComponent<Camera> ();
		CreatePortal _crea = Player.GetComponent<CreatePortal> ();

		Player.transform.Find ("FootCollider").gameObject.SetActive (true);

		_mo.enabled = true;
		//_ML.enabled = true;
		//_ML2.enabled = true;
		_cam.enabled = true;
		_camui.enabled = true;
		_crea.enabled = true;*/
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
