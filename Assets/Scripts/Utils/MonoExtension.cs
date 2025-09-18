using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class MonoExtension
    {
        public static IEnumerator WaitAndDo(this MonoBehaviour _,Action action, float delaySec)
        {
            yield return new WaitForSeconds(delaySec);
            action.Invoke();
        }
    }
}