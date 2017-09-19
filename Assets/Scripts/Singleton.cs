using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	private static T _instance;

	public static T Instance
	{
		get 
		{
			if (!_instance)
			{
				Debug.LogError("The singleton wasn't added to the scene");
			}
			return _instance;
		}
	}

	void Awake()
	{
        if(_instance==null)
            _instance = (T)(object)this;
        
    }
}
