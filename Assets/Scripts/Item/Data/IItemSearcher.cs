using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Item
{
    public interface IItemSearcher
    {
        /// <summary>
        /// ID�ƈ�v����A�C�e������Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        /// <returns>ID�ƈ�v�����A�C�e���̖��O�f�[�^</returns>
        public string SearchName(int id);
        /// <summary>
        /// ID�ƈ�v����A�C�e���摜��Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        public Sprite SearchSprite(int id);
        /// <summary>
        /// ID�ƈ�v����ݒu�I�u�W�F�N�g��Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        public GameObject SearchObject(int id);
        /// <summary>
        /// ID�ƈ�v������ʒl��Ԃ�
        /// </summary>
        /// <param name="id">�A�C�e��ID</param>
        public float SearchEffectValue(int id);
        /// <summary>
        /// ID�ƈ�v����A�C�e��������Ԃ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UniTask SearchProsess(int id);
    }
}