using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;

public class SocketIoSession : MonoBehaviour {

	void Start () {
		Debug.Log( "ButtonClicked" );
		sock = IO.Socket( "http://" + GameObject.Find( "IpInput" ).GetComponent<InputField>().text + ":19900" );

		sock.On( Socket.EVENT_CONNECT, () =>
		{
			sock.Emit( "login", "Hello Socket Io" );
		} );
	}

	void OnDestroy()
	{
		if( sock != null )
			sock.Disconnect();
	}

	Socket sock = null;
}
