using UnityEngine;
using System.Collections;

public class CoroutineHandler
{
    Coroutine coroutine = null;
    MonoBehaviour source;
    public delegate void OnCoroutineStop();
    OnCoroutineStop onStop = null;
    public bool IsRunning
    {
        get { return coroutine != null; }
    }

    public CoroutineHandler(MonoBehaviour mb)
    {
        source = mb;
    }

    public void StartCoroutine(IEnumerator e, OnCoroutineStop stop = null)
    {
        if (IsRunning)
            StopCoroutine();
        coroutine = source.StartCoroutine(e);
        onStop = stop;
    }

    public void StopCoroutine()
    {
        if (coroutine != null)
            source.StopCoroutine(coroutine);
        coroutine = null;
        onStop?.Invoke();
    }

}