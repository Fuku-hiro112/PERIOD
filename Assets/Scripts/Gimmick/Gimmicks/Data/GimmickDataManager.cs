using Gimmikc;
using System.Linq;
using UnityEngine;

namespace Gimmick
{

    public class GimmickDataManager : MonoBehaviour , ISearcher
    {
        [SerializeField]
        private GimmickDataBase _dataBase;
        [SerializeField]
        private GameObject _canvasGimmick;

        private GimmickSourceDataBase[] _data { get => _dataBase.DataArray; }
        public static GimmickDataManager s_Instance;//Hack: Static�ɂ������Ȃ����d���Ȃ�����

        private void Awake()
        {
            // SourceData�̃C���X�^���X��S�Đ������A��\���ɂ��Č����Ȃ��悤�ɂ��Ă���
            foreach (GimmickSourceDataBase gimmickData in _data)
            {
                gimmickData._prefab.SetActive(false);// Prefab�{�̂�False��
                //Fixed: ����f�[�^�������ς��׏C�����K�v�A���N���X�Ńf�[�^�ɃA�N�Z�X���A�\����\���̕ύX�����邽�߁A�f�[�^������������
                gimmickData.Prefab = Instantiate(gimmickData._prefab, _canvasGimmick.transform);
            }

            if (s_Instance == null)
            {
                s_Instance = this;
            }
        }

        /// <summary>
        /// ID���猟�����ASourceData��Ԃ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ID�̃f�[�^</returns>
        public GimmickSourceDataBase SearchData(int id)
        {
            /*foreach (GimmickSourceData data in _data)
            {
                if (data.ID == id)
                {
                    return data;
                }
            }*/
            // id�ƈ�v����Source��Ԃ� Linq
            var data = _data.FirstOrDefault(d => d.ID == id);
            if (data == null)
            {
                Debug.Log("<color=red>ID����v���܂���</color>");
                return null;
            }
            else return data;
        }

        /// <summary>
        /// ID�ƈ�v����J�[�\���̏����ʒu��Ԃ�
        /// </summary>
        /// <param name="id">�M�~�b�N��ID</param>
        public Vector3 SearchPosition(int id) => SearchData(id).StartPos;
        /// <summary>
        /// ID�ƈ�v����M�~�b�N��Prefab��Ԃ�
        /// </summary>
        /// <param name="id">�M�~�b�N��ID</param>
        public GameObject SearchObject(int id) => SearchData(id).Prefab;
    }
}
