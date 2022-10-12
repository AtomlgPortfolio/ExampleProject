using System.Collections;
using UnityEngine;

namespace ProjectName.Scripts.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}