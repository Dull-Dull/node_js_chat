﻿using UnityEngine;

public class LoginRoll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnLoginButtonClick()
	{
		GameObject sockIoSession = new GameObject( "SockIoSession" );
		sockIoSession.AddComponent<SocketIoSession>();		
	}
}
