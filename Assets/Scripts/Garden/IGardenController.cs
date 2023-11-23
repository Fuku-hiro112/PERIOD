using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IGardenController
{
    void OnStart();
    UniTaskVoid ControlGardenAsync(Func<int> func, GameMode mode, CancellationToken token);
}