using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ActionBase : MonoBehaviour
{
    protected CancellationToken Token = default;
    private void Start()
    {
        Token = this.GetCancellationTokenOnDestroy();
        OnStart();
    }
    // Startä÷êîÇÃë„ÇÌÇË
    protected virtual void OnStart() { }

    public async virtual UniTask Action(Transform[] charas, Animator[] animator) 
    { 
        await UniTask.Yield();
    }
}
