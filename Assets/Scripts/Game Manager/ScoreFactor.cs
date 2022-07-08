using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class ScoreFactor : MonoBehaviour
{
    protected abstract int ScoreMultiply { get; }

    public abstract int CalculateScore();
}
