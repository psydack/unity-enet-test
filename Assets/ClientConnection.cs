using UnityEngine;

public class ClientConnection : Connection
{
	private void Start()
	{
		BuildHost(true);
	}

	private void Update()
	{
		if (_host != null)
		{
			while (CheckEvents(out ENet.Event @event))
			{
				switch (@event.Type)
				{
					case ENet.EventType.None:
						break;

					case ENet.EventType.Connect:
						Debug.Log("Client connected to server");
						break;

					case ENet.EventType.Disconnect:
						Debug.Log("Client disconnected from server");
						break;

					case ENet.EventType.Timeout:
						Debug.Log("Client connection timeout");
						break;

					case ENet.EventType.Receive:
						Debug.Log("Packet received from server - Channel ID: " + @event.ChannelID + ", Data length: " + @event.Packet.Length);
						@event.Packet.Dispose();
						break;
				}
			}
		}
	}
}