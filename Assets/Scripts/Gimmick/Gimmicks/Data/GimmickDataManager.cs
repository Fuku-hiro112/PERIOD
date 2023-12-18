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
        public static GimmickDataManager s_Instance;//Hack: Staticにしたくないが仕方なさそう

        private void Awake()
        {
            // SourceDataのインスタンスを全て生成し、非表示にして見えないようにしている
            foreach (GimmickSourceDataBase gimmickData in _data)
            {
                gimmickData._prefab.SetActive(false);// Prefab本体をFalseに
                //Fixed: 毎回データが書き変わる為修正が必要、他クラスでデータにアクセスし、表示非表示の変更をするため、データを書き換える
                gimmickData.Prefab = Instantiate(gimmickData._prefab, _canvasGimmick.transform);
            }

            if (s_Instance == null)
            {
                s_Instance = this;
            }
        }

        /// <summary>
        /// IDから検索し、SourceDataを返す
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDのデータ</returns>
        public GimmickSourceDataBase SearchData(int id)
        {
            /*foreach (GimmickSourceData data in _data)
            {
                if (data.ID == id)
                {
                    return data;
                }
            }*/
            // idと一致したSourceを返す Linq
            var data = _data.FirstOrDefault(d => d.ID == id);
            if (data == null)
            {
                Debug.Log("<color=red>IDが一致しません</color>");
                return null;
            }
            else return data;
        }

        /// <summary>
        /// IDと一致するカーソルの初期位置を返す
        /// </summary>
        /// <param name="id">ギミックのID</param>
        public Vector3 SearchPosition(int id) => SearchData(id).StartPos;
        /// <summary>
        /// IDと一致するギミックのPrefabを返す
        /// </summary>
        /// <param name="id">ギミックのID</param>
        public GameObject SearchObject(int id) => SearchData(id).Prefab;
    }
}
