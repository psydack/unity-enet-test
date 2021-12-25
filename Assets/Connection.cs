using UnityEngine;

public abstract class Connection : MonoBehaviour
{
	[SerializeField]
	protected string _hostname = "localhost";

	[SerializeField]
	protected short _port = 7070;

	[SerializeField]
	protected byte _maxChannels = byte.MaxValue;

	[SerializeField]
	protected int _maxPeers = 200;

	protected ENet.Host _host;
	protected ENet.Peer _peer;

	private ENet.Address BuildAddress(bool isClient)
	{
		ENet.Address address = new ENet.Address();
		address.Port = (ushort)_port;
		if (isClient) address.SetHost(_hostname);

		return address;
	}

	protected void BuildHost(bool isClient)
	{
		_host = new ENet.Host();
		ENet.Address address = BuildAddress(isClient);

		if (isClient)
		{
			_host.Create();
			_peer = _host.Connect(address, _maxChannels, isClient ? 1u : 2u);
		}
		else
		{
			_host.Create(address, _maxPeers);
		}
	}

	protected bool CheckEvents(out ENet.Event @event)
	{
		return _host.Service(0, out @event) > 0 || _host.CheckEvents(out @event) > 0;
	}

	private void OnDestroy()
	{
		_host?.Flush();
	}
}
