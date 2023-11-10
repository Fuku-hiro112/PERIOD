using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IGardenController
{
    void OnStart();
    UniTaskVoid ControlGardenAsync(Action action, GameMode mode, CancellationToken token);
}
