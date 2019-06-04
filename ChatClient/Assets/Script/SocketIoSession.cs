using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public partial class SocketIoSession : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad( this.gameObject );
	}

	void Start () {
		ip = GameObject.Find( "IpInput" ).GetComponent<InputField>().text;
		sockio = new SockIoManager( this, ip, port );
	}

	void Update()
	{
		sockio.RunCallback();
	}

	void OnDestroy()
	{
		sockio.Disconnect();
	}

	public SockIoManager GetSocketIo()
	{
		return sockio;
	}

	public string ip = "";
	public int port = 19900;

	private SockIoManager sockio = null;
}
