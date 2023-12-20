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
        /// ギミック開始
        /// </summary>
        public void OnStart()
        {
            // tagの乱用+検索範囲を狭めるため、Databaseオブジェクトの子オブジェクトから探すようにしている
            ISearcher iSearchable = 
                GameObject.FindGameObjectWithTag("DataBase").
                transform.Find("GimmickDataManager").GetComponent<GimmickDataManager>();
            
            _prefab = iSearchable.SearchObject(GimmickID);

            // ギミックをActiveに
            _prefab.SetActive(true);
        }
        public void OnUpdate()
        {
            
        }
        /// <summary>
        /// ギミック終了
        /// </summary>
        public void OnEnd()
        {
            // ギミックをFalseに
            _prefab.SetActive(false);
        }
    }
}
