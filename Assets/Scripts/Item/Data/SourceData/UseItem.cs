using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

namespace Item
{
    // 使用型アイテムのソースデータ
    [CreateAssetMenu(fileName = "UseItem", menuName = "ScriptableObject/Item/Source/Use")]
    public class UseItem : ItemSourceDataBase
    {
        // 対象？
        [SerializeField]// 効果値
        private int _effectValue;

        [SerializeField]
        float _time = 1.5f;

        public int EffectValue { get => _effectValue; }

        public override async UniTask HandleUseItem()
        {
            // アイテムを使う
            // アニメーション
            // ↑が終わったら効果を適応
            Transform dedZoneTrans = GameObject.FindGameObjectWithTag("DeadZone").transform;
            Vector3 _movement = dedZoneTrans.position;
            _movement.z -= _effectValue;
            dedZoneTrans.DOMove(_movement, _time);
            Debug.Log("アイテム使用:UseItem");
        }
    }
}
