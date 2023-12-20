using Gimmick;
using UnityEngine;

namespace Gimmick
{
    public enum Character
    {
        Boy,
        Engineer,
        Both
    }
    public class GimmickController : MonoBehaviour
    {
        [SerializeField]
        private Character _available = Character.Boy;
        public Character Available { get; private set; }

        [SerializeField]
        private int _gimmickID;
        private GameObject _prefab;
        public int GimmickID { get => _gimmickID; }

        [SerializeField]
        private Character _available;
        public Character Available { get => _available; }
        /// <summary>
        /// �M�~�b�N�J�n
        /// </summary>
        public void OnStart()
        {
            // tag�̗��p+�����͈͂����߂邽�߁ADatabase�I�u�W�F�N�g�̎q�I�u�W�F�N�g����T���悤�ɂ��Ă���
            ISearcher iSearchable = 
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
