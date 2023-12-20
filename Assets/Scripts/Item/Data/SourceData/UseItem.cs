using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Item
{
    // 使用型アイテムのソースデータ
    [CreateAssetMenu(fileName = "UseItem", menuName = "ScriptableObject/Item/Source/Use")]
    public class UseItem : ItemSourceDataBase
    {
        // 対象？
        [SerializeField]// 効果値
        private int _effectValue;

        public int EffectValue { get => _effectValue; }

        public override async UniTask HandleUseItem()
        {
            // アイテムを使う
            // アニメーション
            // ↑が終わったら効果を適応
            Debug.Log("アイテム使用:UseItem");
        }
    }
}
