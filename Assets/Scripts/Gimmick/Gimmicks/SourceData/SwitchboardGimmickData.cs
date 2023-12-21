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
        private CharacterManager _characterManager;
        private OperatorController _operatorController;
        private CancellationTokenSource _token = new CancellationTokenSource();

        public override async UniTask HandleActionAsync(Collider other)
        {
            _characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
            _operatorController = _characterManager.GetComponent<OperatorController>();
            GameObject gimmick = other.transform.parent.gameObject;
            Animator animator = _operatorController.PlayerAnimator;
            animator.SetBool("Switchboard", true);
            await UniTask.Delay(3000, cancellationToken: _token.Token);
            animator.SetBool("Switchboard", false);
            gimmick.transform.Find("Electrical").gameObject.SetActive(false);
            gimmick.transform.Find("Collider").gameObject.SetActive(false);
        }
    }
}
