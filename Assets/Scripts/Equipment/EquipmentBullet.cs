using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBullet<T> : BulletBase<T> where T : MonoBehaviour, IObjectPoolable<T>
{
    
}
