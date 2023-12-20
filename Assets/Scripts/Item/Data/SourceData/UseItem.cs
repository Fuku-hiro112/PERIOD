using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Item
{
    // �g�p�^�A�C�e���̃\�[�X�f�[�^
    [CreateAssetMenu(fileName = "UseItem", menuName = "ScriptableObject/Item/Source/Use")]
    public class UseItem : ItemSourceDataBase
    {
        // �ΏہH
        [SerializeField]// ���ʒl
        private int _effectValue;

        public int EffectValue { get => _effectValue; }

        public override async UniTask HandleUseItem()
        {
            // �A�C�e�����g��
            // �A�j���[�V����
            // �����I���������ʂ�K��
            Debug.Log("�A�C�e���g�p:UseItem");
        }
    }
}
