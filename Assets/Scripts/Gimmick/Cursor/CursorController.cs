using Character;
using Gimmikc;
using Input;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gimmick
{
    // ギミック中のカーソルの移動、及び当たり判定
    public class CursorController : MonoBehaviour
    {
        [SerializeField]
        private CursorInput _cursorInput;
        [SerializeField]
        private float _speed = 1;
        [SerializeField]
        private float _dashMagnification = 2;
        private Image _image;

        // 初期位置
        public Vector3 StartPosition = Vector3.zero;
        public bool IsClear { get; private set; }

        private void Awake()
        {
            TryGetComponent(out _image);
            _image.enabled = false;
        }
        /// <summary>
        /// スタート時
        /// </summary>
        public void OnStart(int id)
        {
            // tagの乱用+検索範囲を狭めるため、Databaseオブジェクトの子オブジェクトから探すようにしている
            ISearchable iSearchable =
                GameObject.FindGameObjectWithTag("DataBase").
                transform.Find("GimmickDataManager").GetComponent<GimmickDataManager>();
            
            _image.enabled = true;
            StartPosition = iSearchable.SearchPosition(id);
            // カーソルをスタート地点へ
            this.transform.position = StartPosition;

            IsClear = false;
        }

        /// <summary>
        /// Update時
        /// </summary>
        public void OnUpdate()
        {
            // 移動値の計算
            Vector3 moveValue = _cursorInput.MoveValue * Time.deltaTime * _speed;
            
            // ダッシュ処理
            /*if (_cursorInput.IsGimmickAction())
            {
                moveValue *= _dashMagnification;
            }*/

            //移動 
            this.gameObject.transform.position += moveValue;// 設計的に別の場所から呼び出すようにしているため,どのオブジェクトか分からなくなると考えた為,this.gameobjectで明示している
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                // 初期地点へ
                this.transform.position = StartPosition;
                //TODO: ゲームパットを振動 and 画面の揺れ and サウンド　別クラスから呼び出そう
            }
            /*else if (other.gameObject.CompareTag("CheckPoint"))// いる？
            //{
            //    //TODO: チェックポイント到達処理
            //    // 次のチェックポイント解放など
            }*/
            else if (other.gameObject.CompareTag("Finish"))
            {
                Debug.Log("ギミック終了");
                //TODO: ギミック終了処理（あれば）
                _image.enabled = false;
                IsClear = true;
            }
        }
    }
}
