using UnityEngine;
using System.Collections;

public class NetworkBall : MonoBehaviour {

	private Vector3 _position;
	//private Quaternion _rotation;
	public float _LimiteTP = 5;
	public float _Smoothing = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (GetComponent<PhotonView> ().isMine == false) 
		{
			
			transform.position = _position;
			/*
			if (Vector3.Distance(transform.position, _position) < _LimiteTP)
			{
				transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime / _Smoothing);
			}else
			{


			}*/
			//transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime / _Smoothing);
			
			
		}


	}



	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			//stream.SendNext(transform.rotation);
		}
		else
		{
			// Network player, receive data
			_position = (Vector3) stream.ReceiveNext();
			//_rotation = (Quaternion) stream.ReceiveNext();
		}
	}


}
