using Character;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using UnityEngine.TextCore.LowLevel;

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
            Vector3 pos = new Vector3(gimmick.transform.position.x, _operatorController.CurrentCharacter.transform.position.y, gimmick.transform.position.z);
            _operatorController.CurrentCharacter.transform.LookAt(pos);
            animator.SetBool("Switchboard", true);
            await UniTask.Delay(3000, cancellationToken: _token.Token);
            animator.SetBool("Switchboard", false);
            gimmick.transform.Find("Electrical").gameObject.SetActive(false);
            gimmick.transform.Find("Collider").gameObject.SetActive(false);
        }
    }
}
