using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using socket.io;
using System.Reflection;

interface ISockIoCallback
{
	void RunCallback();
}

class SockIoCallbackWithNoParam : ISockIoCallback
{
	public SockIoCallbackWithNoParam( Action _callback )
	{
		callback = _callback;
	}

	public void RunCallback()
	{
		callback();
	}

	private Action callback = null;
}

class SockIoCallbackWithParam : ISockIoCallback
{
	public SockIoCallbackWithParam( Action<string> _callback, string _param )
	{
		callback = _callback;
		param = _param;
	}
	public void RunCallback()
	{
		callback( param );
	}

	private Action<string> callback = null;
	private string param = null;
}

[AttributeUsage( AttributeTargets.Method, Inherited = false, AllowMultiple = false )]
class SockIoCallbackAttribute : Attribute
{

}

public class SockIoManager
{
	public SockIoManager( object component, string ip, int port )
	{
		Connect( ip, port );
		callbackQueue = new Queue<ISockIoCallback>();

		Type componentType = component.GetType();
		MethodInfo[] methods = componentType.GetMethods();

		foreach( MethodInfo info in methods )
		{
			object[] attrs = info.GetCustomAttributes( typeof( SockIoCallbackAttribute ), false );
			if( attrs.Length == 0 )
				continue;

			if( info.GetParameters().Length == 0 )
			{
				sock.On( info.Name, () =>
				{
					lock( callbackQueue )
					{
						callbackQueue.Enqueue( new SockIoCallbackWithNoParam(
							Delegate.CreateDelegate( typeof( Action ), component, info ) as Action ) );
					}
				} );
			}
			else if( info.GetParameters().Length == 1 &&
				info.GetParameters()[ 0 ].ParameterType == typeof( string ) )
			{
				sock.On( info.Name, ( param ) =>
				{
					lock( callbackQueue )
					{
						callbackQueue.Enqueue( new SockIoCallbackWithParam(
							Delegate.CreateDelegate( typeof( Action<string> ), component, info ) as Action<string>,
							param as string ) );
					}
				} );
			}
			else
			{
				//Error
			}
		}
	}

	private void Connect( string ip, int port )
	{
		sock = Socket.Connect( "http://" + ip + ":" + port.ToString() );
	}

	public void Emit( string  eventString, string arg )
	{
		sock.Emit( eventString, arg );
	}

	public void RunCallback()
	{
		if( sock == null )
			return;

		lock( callbackQueue )
		{
			try
			{
				for( int i = 0; i < 10; ++i )
				{
					ISockIoCallback callback = callbackQueue.Dequeue();

					if( callback == null )
						break;

					callback.RunCallback();
				}
			}
			catch( InvalidOperationException ){}
		}
	}

	private Socket sock = null;
	private Queue<ISockIoCallback> callbackQueue = null;
}