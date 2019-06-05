using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChattingRoll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		userList = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMsgSendButtonClick()
	{
		string msg = MsgInputField.GetComponent<InputField>().text;
		MsgInputField.GetComponent<InputField>().text = "";

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

		UserListField.GetComponent<InputField>().text = text;
	}

	public void RemoveUser( string userId )
	{
		userList.Remove( userId );

		string text = "";
		foreach( string id in userList )
		{
			text += id + "\n";
		}

		UserListField.GetComponent<InputField>().text = text;
	}

	public void AppendMsg( string msg )
	{
		ChattingField.GetComponent<InputField>().text += "\n" + msg;
	}

	private List<string> userList = null;

	public GameObject UserListField;
	public GameObject ChattingField;
	public GameObject MsgInputField;
}
