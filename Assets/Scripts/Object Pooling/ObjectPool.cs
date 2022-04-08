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
    ///<param name="objectPooler">
    ///Reference to the pooler implementing IObjectPooler<T>
    /// </param>
    public static void Pool<T>(IObjectPooler<T> objectPooler) where T : MonoBehaviour, IObjectPoolable<T>
    {
        if (objectPooler.Pool.Count <= 0)
        {
            objectPooler.Pool.Enqueue(Object.Instantiate(objectPooler.Prefab, new Vector3(0, 0, 0), Quaternion.identity));
        }
        T instance = objectPooler.Pool.Dequeue();
        instance.GetComponent<IObjectPoolable<T>>().ParentObjectPooler = objectPooler;
        objectPooler.OnPooled(instance);
    }

    ///<summary>
    ///Method used for adding the gameobject back into the queue
    ///Docs:
    ///Call this method when you want to disable the instance and return it back into its parent queue
    /// </summary>
    /// 
    ///<param name="objectPoolable">
    ///Reference to the pooled object implementing IObjectPoolable<T>
    /// </param>
    public static void Return<T>(IObjectPoolable<T> objectPoolable) where T : MonoBehaviour, IObjectPoolable<T>
    {
        objectPoolable.ParentObjectPooler.Pool.Enqueue(objectPoolable.ReturnComponent());
        objectPoolable.OnReturn();
    }
}
