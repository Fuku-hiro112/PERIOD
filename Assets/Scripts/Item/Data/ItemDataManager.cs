using Gimmick;
using System.Linq;
using UnityEngine;

namespace Item
{
    public class ItemDataManager : MonoBehaviour,IItemSearcher
    {
        [SerializeField]
        private ItemDataBase _dataBase;

        private ItemSourceDataBase[] _data { get => _dataBase.ItemSourceDataBases; }

        public static IItemSearcher s_Instance;//Hack: Static�ɂ������Ȃ����d���Ȃ�����

        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                Debug.Log("null��",this);
            }
        }

        /// <summary>
        /// ID���猟�����ASourceData��Ԃ�
        /// </summary>
        /// <typeparam name="T">�A�C�e���̃\�[�X�f�[�^</typeparam>
        /// <param name="id"></param>
        /// <returns>ID�̃f�[�^</returns>
        private T SearchData<T>(int id) where T : ItemSourceDataBase
        {
            // ID�ƈ�v����SourceData��Ԃ� (Linq�g�p)
            var data = _data.FirstOrDefault(d => d.ID == id);
            if (data == null)
            {
                Debug.Log("<color=red>ID����v���܂���</color>");
                return null;
            }
            else return (T)data;
        }

        /// <summary>
        /// ID�ƈ�v����A�C�e������Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        /// <returns>ID�ƈ�v�����A�C�e���̖��O�f�[�^</returns>
        public string SearchName(int id) => SearchData<ItemSourceDataBase>(id)?.Name;
        /// <summary>
        /// ID�ƈ�v����A�C�e���摜��Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        public Sprite SearchSprite(int id) => SearchData<ItemSourceDataBase>(id)?.Sprite;
        /// <summary>
        /// ID�ƈ�v����ݒu�I�u�W�F�N�g��Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        public GameObject SearchObject(int id) => SearchData<SetUpItem>(id)?.Prefab;
        /// <summary>
        /// ID�ƈ�v������ʒl��Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        public float SearchEffectValue(int id) => SearchData<UseItem>(id).EffectValue;
    }
}