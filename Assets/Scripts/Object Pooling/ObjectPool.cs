using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    ///<summary>
    ///Method used for instantiating the prefab gameobject
    ///Handles retrieving prefabs from the queue and connecting the instance to the parent
    ///Docs:
    ///Call this method when you want to instantiate the prefab
    ///</summary>
    ///
    ///<param name="objectPoolable">
    ///Reference to the pooler implementing IObjectPoolable<T>
    /// </param>
    public static void Pool<T>(IObjectPooler<T> objectPoolable) where T : MonoBehaviour, IObjectPoolable<T>
    {
        if (objectPoolable.Pool.Count <= 0)
        {
            objectPoolable.Pool.Enqueue(Object.Instantiate(objectPoolable.Prefab, new Vector3(0, 0, 0), Quaternion.identity));
        }
        T instance = objectPoolable.Pool.Dequeue();
        instance.GetComponent<IObjectPoolable<T>>().ParentObjectPooler = objectPoolable;
        objectPoolable.OnPooled(instance);
    }

    ///<summary>
    ///Method used for adding the gameobject back into the queue
    ///Docs:
    ///Call this method when you want to disable the instance and return it back into its parent queue
    /// </summary>
    /// 
    ///<param name="objectPooled">
    ///Reference to the pooled object implementing IObjectPooled<T>
    /// </param>
    public static void Return<T>(IObjectPoolable<T> objectPooled) where T : MonoBehaviour, IObjectPoolable<T>
    {
        objectPooled.ParentObjectPooler.Pool.Enqueue(objectPooled.ReturnComponent());
        objectPooled.OnReturn();
    }
}
