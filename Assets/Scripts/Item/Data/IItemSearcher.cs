using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Item
{
    public interface IItemSearcher
    {
        /// <summary>
        /// IDと一致するアイテム名を返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        /// <returns>IDと一致したアイテムの名前データ</returns>
        public string SearchName(int id);
        /// <summary>
        /// IDと一致するアイテム画像を返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        public Sprite SearchSprite(int id);
        /// <summary>
        /// IDと一致する設置オブジェクトを返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        public GameObject SearchObject(int id);
        /// <summary>
        /// IDと一致する効果値を返す
        /// </summary>
        /// <param name="id">アイテムID</param>
        public float SearchEffectValue(int id);
        /// <summary>
        /// IDと一致するアイテム処理を返す
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UniTask SearchProsess(int id);
    }
}