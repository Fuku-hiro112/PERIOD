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
            // SourceDataのインスタンスを全て生成し、非表示にして見えないようにしている
            foreach (GimmickSourceData gimmickData in _data)
            {
                gimmickData.Prefab.SetActive(false);// Prefab本体をFalseに
                // 他クラスでデータにアクセスし、表示非表示の変更をするため、データを書き換える
                gimmickData.Prefab = Instantiate(gimmickData.Prefab, _canvasGimmick.transform);
            }
        }

        /// <summary>
        /// IDから検索し、SourceDataを返す
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDのデータ</returns>
        private GimmickSourceData SearchData(int id)
        {
            /*foreach (GimmickSourceData data in _data)
            {
                if (data.ID == id)
                {
                    return data;
                }
            }*/
            
            var data = _data.FirstOrDefault(d => d.ID == id);// idと一致したSourceを返す Linq
            if (data == null)
            {
                Debug.Log("<color=red>IDが一致しません</color>");
                return null;
            }
            else return data;
        }

        /// <summary>
        /// IDのStartPositionを返す
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDと一致したギミックのデータ</returns>
        public Vector3 SearchPosition(int id)
        {
            return SearchData(id).StartPos;
        }
        /// <summary>
        /// IDのStartPositionを返す
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDと一致したギミックのデータ</returns>
        public GameObject SearchObject(int id)
        {
            return SearchData(id).Prefab;
        }
    }
}
