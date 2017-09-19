using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool> {

	public GameObject[] PrefabsAInstanciar;

	public int AmountToCreate;

	public List<GameObject>[] Pool;

	void Start()
	{
		Pool = new List<GameObject>[PrefabsAInstanciar.Length];

		for (int i = 0; i < PrefabsAInstanciar.Length; i++)
		{
			Pool [i] = new List<GameObject> ();

			for (int cant = 0; cant < AmountToCreate; cant++)
			{
				GameObject newInstance = GameObject.Instantiate (PrefabsAInstanciar [i], Vector3.zero, Quaternion.identity) as GameObject;
				newInstance.name = PrefabsAInstanciar [i].name;
				newInstance.SetActive (false);
				Pool [i].Add (newInstance);
			}
		}
	}

	public GameObject GetFromPool(GameObject prefab)
	{
		for (int i = 0; i < PrefabsAInstanciar.Length; i++) 
		{
			if (PrefabsAInstanciar [i].name == prefab.name) 
			{
				List<GameObject> listOfMyType = Pool [i];
				if (listOfMyType.Count > 0) 
				{
					GameObject first = listOfMyType [0];
					listOfMyType.Remove (first);
					first.SetActive (true);
					return first;
				}
			}
		}

		//Debug.Log ("No encuentro del tipo en el pool");

		return null;
	}

	public void PoolAgain(GameObject go)
	{
		for (int i = 0; i < PrefabsAInstanciar.Length; i++) 
		{
			if (PrefabsAInstanciar [i].name == go.name) 
			{
				go.SetActive (false);
				Pool [i].Add (go);			
			}
		}
	}

    public void IncreaseAmountToCreate(int cant) {
        AmountToCreate = cant;
        for (int i = 0; i < PrefabsAInstanciar.Length; i++)
        {
            Pool[i] = new List<GameObject>();

            for (int j = 0; j< AmountToCreate; j++)
            {
                GameObject newInstance = GameObject.Instantiate(PrefabsAInstanciar[i], Vector3.zero, Quaternion.identity) as GameObject;
                newInstance.name = PrefabsAInstanciar[i].name;
                newInstance.SetActive(false);
                Pool[i].Add(newInstance);
            }
        }
    }
}


