using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance => _instance;

	private static T _instance;

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
	}
}
