using Character;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "GimmickData", menuName = "ScriptableObject/Gimmick/GimmickData")]
    public class GimmickData : GimmickSourceDataBase
    {
        public override async UniTask HandleActionAsync(Collider other)
        {
            GameObject gimmick = other.gameObject;
            gimmick.transform.Find("").gameObject.SetActive(false);
            gimmick.transform.Find("").gameObject.SetActive(false);
        }
    }
}
