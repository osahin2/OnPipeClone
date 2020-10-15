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
    private Transform poolStart;

    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;
    

    private int counter = 0;
    private Vector3 distance;

    private void Awake()
    {
        poolStart = GameObject.FindGameObjectWithTag("poolStart").GetComponent<Transform>();
        distance = poolStart.transform.position;

    }
    void Start()
    {
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

    IEnumerator Pooler()
    {
        while (true)
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                item.objectToPool = GetPooledCylinders();
                item.objectToPool.transform.position = new Vector3(0, 0, distance.z += 6);
                item.objectToPool.SetActive(true);
                yield return new WaitForSeconds(1.2f);
            }
        }
    }

    public GameObject GetPooledCylinders()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
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
