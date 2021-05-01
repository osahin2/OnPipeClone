using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
}

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Transform poolStart;

    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;
    

    private int counter = 0;

    private Vector3 distance;
    public Vector3 Distance => distance;

    public void Initialized()
    {
        distance = poolStart.transform.position;

        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
        StartCoroutine(Pooler());
    }
    
    private IEnumerator Pooler()
    {
        while (true)
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                item.objectToPool = GetPooledCylinders();
                item.objectToPool.transform.position = new Vector3(0, 0, distance.z += 6);
                item.objectToPool.SetActive(true);
                yield return new WaitForSeconds(3f);
            }
        }
    }

    private GameObject GetPooledCylinders()
    {
        foreach (var t in pooledObjects)
        {
            counter = Random.Range(0, pooledObjects.Count);
            if (!pooledObjects[counter].activeInHierarchy)
            {
                return pooledObjects[counter];
            }
        }
        return null;
    }
}
