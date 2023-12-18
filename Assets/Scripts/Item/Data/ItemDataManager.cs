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

        public static IItemSearcher s_Instance;//Hack: Staticにしたくないが仕方なさそう

        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                Debug.Log("nullだ",this);
            }
        }

        /// <summary>
        /// IDから検索し、SourceDataを返す
        /// </summary>
        /// <typeparam name="T">アイテムのソースデータ</typeparam>
        /// <param name="id"></param>
        /// <returns>IDのデータ</returns>
        private T SearchData<T>(int id) where T : ItemSourceDataBase
        {
            // IDと一致したSourceDataを返す (Linq使用)
            var data = _data.FirstOrDefault(d => d.ID == id);
            if (data == null)
            {
                Debug.Log("<color=red>IDが一致しません</color>");
                return null;
            }
            else return (T)data;
        }

        /// <summary>
        /// IDと一致するアイテム名を返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        /// <returns>IDと一致したアイテムの名前データ</returns>
        public string SearchName(int id) => SearchData<ItemSourceDataBase>(id)?.Name;
        /// <summary>
        /// IDと一致するアイテム画像を返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        public Sprite SearchSprite(int id) => SearchData<ItemSourceDataBase>(id)?.Sprite;
        /// <summary>
        /// IDと一致する設置オブジェクトを返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        public GameObject SearchObject(int id) => SearchData<SetUpItem>(id)?.Prefab;
        /// <summary>
        /// IDと一致する効果値を返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        public float SearchEffectValue(int id) => SearchData<UseItem>(id).EffectValue;
    }
}