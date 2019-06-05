using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class SocketIoSession
{
	[SockIoCallback]
	public void connect()
	{
		string userId = GameObject.Find("UserIdInput").GetComponent<InputField>().text;
		sockio.Emit( "req_login", userId );

		Debug.Log( "connect Session" );
	}

	[SockIoCallback]
	public void disconnect()
	{
		Debug.Log( "Disconnected" );
	}

	[SockIoCallback]
	public void res_login( string result )
	{
		Debug.Log( "login : " + result);

		SceneManager.LoadScene( "Chatting" );
	}

	[SockIoCallback]
	public void user_list( string userIdList )
	{
		Debug.Log( "user_list" );
	}

	[SockIoCallback]
	public void user_login( string userId )
	{
		Debug.Log( "user_login" );

		GameObject.Find( "ChattingRoll" ).GetComponent<ChattingRoll>().AddUser( userId );
	}

	[SockIoCallback]
	public void user_logout( string userId )
	{
		Debug.Log( "user_logout" );

		GameObject.Find( "ChattingRoll" ).GetComponent<ChattingRoll>().RemoveUser( userId );
	}

	[SockIoCallback]
	public void chat_message( string message )
	{
		GameObject.Find( "ChattingRoll" ).GetComponent<ChattingRoll>().AppendMsg( message );
	}
}