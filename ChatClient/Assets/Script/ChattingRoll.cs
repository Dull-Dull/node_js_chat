using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChattingRoll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMsgSendButtonClick()
	{
		string msg = MsgInputField.GetComponent<Text>().text;
		MsgInputField.GetComponent<Text>().text = "";

		GameObject.Find( "SockIoSession" ).GetComponent<SocketIoSession>().GetSocketIo().Emit( "chat_message", msg );
	}

	public void AddUser( string userId )
	{
		userList.Add( userId );

		string text = "";
		foreach( string id in userList )
		{
			text += id + "\n";
		}

		UserListField.GetComponent<Text>().text = text;
	}

	public void RemoveUser( string userId )
	{
		userList.Remove( userId );

		string text = "";
		foreach( string id in userList )
		{
			text += id + "\n";
		}

		UserListField.GetComponent<Text>().text = text;
	}

	public void AppendMsg( string msg )
	{
		ChattingField.GetComponent<Text>().text += "\n" + msg;
	}

	private List<string> userList;

	public GameObject UserListField;
	public GameObject ChattingField;
	public GameObject MsgInputField;
}
