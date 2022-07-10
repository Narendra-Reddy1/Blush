using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    public static void SpawnObjects(GameObject objectToSpawn, Transform parent, int poolSize,
        List<GameObject> objectsPool, Vector3? position =null, Quaternion?rotation = null)

    {
        for (int i = 0; i < poolSize; i++)
        {
            objectsPool.Clear();
            GameObject spawnedObject = Object.Instantiate(objectToSpawn,(Vector3) position,(Quaternion) rotation, parent);
            spawnedObject.SetActive(false);
            objectsPool.Add(spawnedObject);
        }
    }


}
