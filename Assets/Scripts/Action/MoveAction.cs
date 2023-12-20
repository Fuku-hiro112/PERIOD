using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MoveAction : ActionBase
{
    [SerializeField]
    private Vector3 _moveValue = Vector3.zero;
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private bool _isFollow = true;// trueは１にfalseは０に
    [SerializeField]
    private string _boolAnimatorParameter = default;

    private const string c_layerNameChara = "Character";
    private const string c_layerNameAction = "Action";

    int _charaLayer, _actionLayer;// Startで初期化必須

    protected override void OnStart()
    {
        _charaLayer = LayerMask.NameToLayer(c_layerNameChara);
        _actionLayer = LayerMask.NameToLayer(c_layerNameAction);
    }

    public async override UniTask Action(Transform[] charas, Animator[] animator) 
    {
        //CharaとActionレイヤーの当たり判定を無視する　MEMO:これがついてるオブジェクトの当たり判定OFFにすればいいのでは？
        Physics.IgnoreLayerCollision(_charaLayer, _actionLayer, true);// 

        //TODO: アニメーション開始
        int index = _isFollow ? 1 : 0;// 追従するかをキャストしfotループのインデックスに組み込む
        //TODO: Charaが移動
        for (int i = 0; i <= index; i++)
        {
            Move(charas[i], animator[i]).Forget();
            //TODO: 一定以上の間が空くまで待機
            await UniTask.Yield();
            
            if (!_isFollow) break;// 追従者が居ないならループを抜ける
        }
        // CharaとActionレイヤーの当たり判定を元に戻す
        Physics.IgnoreLayerCollision(_charaLayer, _actionLayer, false);
    }
    /// <summary>
    /// 動きの処理　アニメーションも行う
    /// </summary>
    /// <param name="chara">動かしたいTransform</param>
    /// <param name="animator">動かしたいアニメーター</param>
    private async UniTaskVoid Move(Transform chara,Animator animator)
    {
        animator.SetBool(_boolAnimatorParameter, true);//TODO: ""の中を書く アニメーション開始
        await chara.DOLocalMove(_moveValue, _moveSpeed).WithCancellation(Token);// 移動
        animator.SetBool(_boolAnimatorParameter, false);//TODO: ""の中を書く　アニメーション終了
    }
}
