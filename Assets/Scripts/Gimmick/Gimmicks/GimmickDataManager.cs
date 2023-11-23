using Gimmikc;
using System.Linq;
using UnityEngine;

namespace Gimmick
{

    public class GimmickDataManager : MonoBehaviour , ISearchable
    {
        [SerializeField]
        private GimmickDataBase _dataBase;
        [SerializeField]
        private GameObject _canvasGimmick;

        private GimmickSourceData[] _data { get => _dataBase.DataArray; }

        private void Awake()
        {
            // SourceData�̃C���X�^���X��S�Đ������A��\���ɂ��Č����Ȃ��悤�ɂ��Ă���
            foreach (GimmickSourceData gimmickData in _data)
            {
                gimmickData.Prefab.SetActive(false);// Prefab�{�̂�False��
                // ���N���X�Ńf�[�^�ɃA�N�Z�X���A�\����\���̕ύX�����邽�߁A�f�[�^������������
                gimmickData.Prefab = Instantiate(gimmickData.Prefab, _canvasGimmick.transform);
            }
        }

        /// <summary>
        /// ID���猟�����ASourceData��Ԃ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ID�̃f�[�^</returns>
        private GimmickSourceData SearchData(int id)
        {
            /*foreach (GimmickSourceData data in _data)
            {
                if (data.ID == id)
                {
                    return data;
                }
            }*/
            
            var data = _data.FirstOrDefault(d => d.ID == id);// id�ƈ�v����Source��Ԃ� Linq
            if (data == null)
            {
                Debug.Log("<color=red>ID����v���܂���</color>");
                return null;
            }
            else return data;
        }

        /// <summary>
        /// ID��StartPosition��Ԃ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ID�ƈ�v�����M�~�b�N�̃f�[�^</returns>
        public Vector3 SearchPosition(int id)
        {
            return SearchData(id).StartPos;
        }
        /// <summary>
        /// ID��StartPosition��Ԃ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ID�ƈ�v�����M�~�b�N�̃f�[�^</returns>
        public GameObject SearchObject(int id)
        {
            return SearchData(id).Prefab;
        }
    }
}
