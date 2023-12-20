using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Item
{
    // 設置型アイテムのソースデータ
    [CreateAssetMenu(fileName = "SetUpItemSourceData", menuName = "ScriptableObject/Item/Source/SetUp")]
    public class SetUpItem : ItemSourceDataBase
    {
        [SerializeField]// 設置オブジェクト
        private GameObject _prefab;

        public GameObject Prefab { get => _prefab; }

        public override async UniTask HandleUseItem(/*引数多数*/)
        {
            // アイテムを設置する
            // アニメーション
            Debug.Log("アイテム使用:SetUpItem");
        }
    }
}
