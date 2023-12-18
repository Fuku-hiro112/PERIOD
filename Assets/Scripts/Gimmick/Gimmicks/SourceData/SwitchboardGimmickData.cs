using Character;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace Gimmick
{
    [CreateAssetMenu(fileName = "SwitchboardGimmickData", menuName = "ScriptableObject/Gimmick/SwitchboardGimmickData")]
    public class SwitchboardGimmickData : GimmickSourceDataBase
    {
        public override async UniTask HandleActionAsync(Collider other)
        {
                GameObject gimmick = other.transform.parent.gameObject;
                gimmick.transform.Find("Electrical").gameObject.SetActive(false);
                gimmick.transform.Find("Collider").gameObject.SetActive(false);
        }
    }
}
