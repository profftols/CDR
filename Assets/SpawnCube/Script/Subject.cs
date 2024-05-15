using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public abstract class Subject : MonoBehaviour
{
    protected int MinTimer = 2;
    protected int MaxTimer = 6;
    
    protected virtual IEnumerator LaunchDeath()
    {
        var timer = new WaitForSeconds(Random.Range(MinTimer, MaxTimer));
        
        yield return timer;
    }
}
