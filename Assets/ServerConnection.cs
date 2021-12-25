using UnityEngine;

public class ServerConnection : Connection
{
	private void Start()
	{
		BuildHost(false);
	}

	private void Update()
	{
		if (_host.IsSet)
		{
			while (CheckEvents(out ENet.Event @event))
			{
				switch (@event.Type)
				{
					case ENet.EventType.None:
						break;

					case ENet.EventType.Connect:
						Debug.Log("Client connected - ID: " + @event.Peer.ID + ", IP: " + @event.Peer.IP);
						break;

					case ENet.EventType.Disconnect:
						Debug.Log("Client disconnected - ID: " + @event.Peer.ID + ", IP: " + @event.Peer.IP);
						break;

					case ENet.EventType.Timeout:
						Debug.Log("Client timeout - ID: " + @event.Peer.ID + ", IP: " + @event.Peer.IP);
						break;

					case ENet.EventType.Receive:
						Debug.Log("Packet received from - ID: " + @event.Peer.ID + ", IP: " + @event.Peer.IP + ", Channel ID: " + @event.ChannelID + ", Data length: " + @event.Packet.Length);
						@event.Packet.Dispose();
						break;
				}
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				ENet.Packet packet = default(ENet.Packet);
				byte[] data = new byte[64];

				packet.Create(data);
				_host.Broadcast(0, ref packet);
			}
		}
	}
}
