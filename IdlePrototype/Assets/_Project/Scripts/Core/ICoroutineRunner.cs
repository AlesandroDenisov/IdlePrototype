using System.Collections;
using UnityEngine;

namespace IdleArcade.Core
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}