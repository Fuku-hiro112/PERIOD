using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

namespace Item
{
    // �g�p�^�A�C�e���̃\�[�X�f�[�^
    [CreateAssetMenu(fileName = "UseItem", menuName = "ScriptableObject/Item/Source/Use")]
    public class UseItem : ItemSourceDataBase
    {
        // �ΏہH
        [SerializeField]// ���ʒl
        private int _effectValue;

        [SerializeField]
        float _time = 1.5f;

        public int EffectValue { get => _effectValue; }

        public override async UniTask HandleUseItem()
        {
            // �A�C�e�����g��
            // �A�j���[�V����
            // �����I���������ʂ�K��
            Transform dedZoneTrans = GameObject.FindGameObjectWithTag("DeadZone").transform;
            Vector3 _movement = dedZoneTrans.position;
            _movement.z -= _effectValue;
            dedZoneTrans.DOMove(_movement, _time);
            Debug.Log("�A�C�e���g�p:UseItem");
        }
    }
}
