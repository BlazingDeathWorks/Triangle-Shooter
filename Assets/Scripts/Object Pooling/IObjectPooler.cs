using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//T should be the type that's being pooled
public interface IObjectPooler<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    /// <summary>
    ///Prefab to be instantiated or pooled
    ///Docs:
    ///Simply point the prefab(=>) to the target gameObject to be instantiated
    /// </summary>
    T Prefab { get; }

    /// <summary>
    ///Queue to the pool
    ///Docs:
    ///Simply Initialize Queue to a new Queue of type T
    /// </summary>
    Queue<T> Pool { get; }

    /// <summary>
    ///Method called after prefab is pooled
    ///Docs:
    ///Add statements in order to initialize the instance (i.e. enable the gameObject, set position, etc.)
    /// </summary>
    /// 
    /// <param name="instance">
    /// The object being instantiated/pooled
    /// </param>
    void OnPooled(T instance);
}
