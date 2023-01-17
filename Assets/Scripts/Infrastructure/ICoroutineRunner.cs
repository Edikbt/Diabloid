using System.Collections;
using UnityEngine;

namespace Diabloid
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}