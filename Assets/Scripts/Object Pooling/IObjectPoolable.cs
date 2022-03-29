using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//T should be the class that is implementing this interface
public interface IObjectPoolable<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    /// <summary>
    /// Reference to the pooler that instantiated this instance
    /// Docs:
    /// Simply declare the property
    /// </summary>
    IObjectPooler<T> ParentObjectPooler { get; set; }

    /// <summary>
    /// Called when the instance returns to the queue
    /// Docs:
    /// Simply disable gameObject
    /// </summary>
    void OnReturn();

    /// <summary>
    /// Called when returning instance back to the queue
    /// Docs:
    /// Cache a reference to the component of type T
    /// Simply return the cached reference
    /// </summary>
    /// <returns>
    /// Returns component of type T
    /// </returns>
    T ReturnComponent();
}
