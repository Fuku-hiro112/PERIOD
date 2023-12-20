using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Item
{
    // �ݒu�^�A�C�e���̃\�[�X�f�[�^
    [CreateAssetMenu(fileName = "SetUpItemSourceData", menuName = "ScriptableObject/Item/Source/SetUp")]
    public class SetUpItem : ItemSourceDataBase
    {
        [SerializeField]// �ݒu�I�u�W�F�N�g
        private GameObject _prefab;

        public GameObject Prefab { get => _prefab; }

        public override async UniTask HandleUseItem(/*��������*/)
        {
            // �A�C�e����ݒu����
            // �A�j���[�V����
            Debug.Log("�A�C�e���g�p:SetUpItem");
        }
    }
}
