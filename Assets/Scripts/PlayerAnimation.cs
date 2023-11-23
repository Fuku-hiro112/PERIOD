using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    public float walkSpeed; // 歩く速さ
    public float runSpeed;  // 走る速さ

    private Animator _animator;
    private Vector2 movementInput;
    private float currentSpeed;

    private bool isDeath = false;

    Vector3 Dir; //左スティックの操作方向
    [SerializeField] float _MoveSpeed; //移動速度
    [SerializeField] float _Threshold; //しきい値：0.2の２乗を想定


    void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            return;
        }
    }


    void FixedUpdate()
    {
        if (isDeath)
        {
            // Deathアニメーションが再生中の場合、移動は無効にする
            return;
        }


        if (Gamepad.current == null)
        {
            return;
        }

        //移動処理
        float Lx = Gamepad.current.leftStick.ReadValue().x;
        float Ly = Gamepad.current.leftStick.ReadValue().y;
        Dir = new Vector3(Lx, 0, Ly);

        if (Dir.sqrMagnitude > _Threshold)
        {
            transform.position += Dir * _MoveSpeed * Time.fixedDeltaTime;
            //同じ長さの２つのベクトルのDot積（同じ方向なら1、反対方向なら-1）を求める
            _animator.SetFloat("Vertical", Vector3.Dot(Dir, transform.forward));
            _animator.SetFloat("Horizontal", Vector3.Dot(Dir, transform.right));
        }
        else
        {
            _animator.SetFloat("Vertical", 0.0f);
            _animator.SetFloat("Horizontal", 0.0f);
        }
    }


    void Update()
    {
        if (isDeath)
        {
            // Deathアニメーションが再生中の場合、移動は無効にする
            return;
        }

        if (Gamepad.current == null)
        {
            return;
        }

        MoveAnimation();
    }

    /// <summary>
    /// AnimatorのSpeedの値でWalkとRunを切り替えるメソッド
    /// </summary>
    void MoveAnimation()
    {
        // Input Systemからの入力を取得
        movementInput = InputSystem.GetDevice<Gamepad>().leftStick.ReadValue();

        // 入力から速度を計算
        currentSpeed = movementInput.magnitude;

        // アニメーターのパラメーターを設定
        _animator.SetFloat("Speed", currentSpeed);


        // 歩くアニメーションと走るアニメーションを切り替え
        if (currentSpeed > 0.5f)
        {
            if (InputSystem.GetDevice<Gamepad>().leftStickButton.isPressed)
            {
                currentSpeed *= runSpeed; // 走る速さを適用
            }
            else
            {
                currentSpeed *= walkSpeed; // 歩く速さを適用
            }
        }
    }

    /// <summary>
    /// // デッドゾーンに入ったらDeathアニメーションを再生
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            _animator.SetTrigger("Death");
            isDeath = true;
        }
    }
}


