using Gimmikc;
using UnityEngine;

namespace Gimmick
{
    public class GimmickController : MonoBehaviour
    {
        [SerializeField]
        private int _gimmickID;
        private GameObject _prefab;
        public int GimmickID { get => _gimmickID; }

        /// <summary>
        /// �M�~�b�N�J�n
        /// </summary>
        public void OnStart()
        {
            // tag�̗��p+�����͈͂����߂邽�߁ADatabase�I�u�W�F�N�g�̎q�I�u�W�F�N�g����T���悤�ɂ��Ă���
            ISearchable iSearchable = 
                GameObject.FindGameObjectWithTag("DataBase").
                transform.Find("GimmickDataManager").GetComponent<GimmickDataManager>();
            
            _prefab = iSearchable.SearchObject(GimmickID);

            // �M�~�b�N��Active��
            _prefab.SetActive(true);
        }
        public void OnUpdate()
        {
            
        }
        /// <summary>
        /// �M�~�b�N�I��
        /// </summary>
        public void OnEnd()
        {
            // �M�~�b�N��False��
            _prefab.SetActive(false);
        }
    }
}
