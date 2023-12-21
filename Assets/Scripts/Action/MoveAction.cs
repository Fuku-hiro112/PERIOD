using Character;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Character;

public class MoveAction : ActionBase
{
    [SerializeField]
    private Vector3 _moveValue = Vector3.zero;
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private bool _isFollow = true;// true�͂P��false�͂O��
    [SerializeField]
    private string _boolAnimatorParameter = "Crawling";
    private CharacterManager _characterManager;

    private const string c_layerNameChara = "Character";
    private const string c_layerNameAction = "Action";

    int _charaLayer, _actionLayer;// Start�ŏ������K�{


    protected override void OnStart()
    {
        _charaLayer = LayerMask.NameToLayer(c_layerNameChara);
        _actionLayer = LayerMask.NameToLayer(c_layerNameAction);
        GameObject.FindGameObjectWithTag("CharacterManager").TryGetComponent(out _characterManager);
    }

    public async override UniTask Action(Transform[] charas, Animator[] animator) 
    {
        //Chara��Action���C���[�̓����蔻��𖳎�����@MEMO:���ꂪ���Ă�I�u�W�F�N�g�̓����蔻��OFF�ɂ���΂����̂ł́H
        Physics.IgnoreLayerCollision(_charaLayer, _actionLayer, true);// 
        if (!_isFollow) _characterManager._isFollow = !_characterManager._isFollow;
        //TODO: �A�j���[�V�����J�n
        int index = _isFollow ? 1 : 0;// �Ǐ]���邩���L���X�g��fot���[�v�̃C���f�b�N�X�ɑg�ݍ���
        //TODO: Chara���ړ�
        for (int i = 0; i <= index; i++)
        {
            Move(charas[i], animator[i]).Forget();
            //TODO: ���ȏ�̊Ԃ��󂭂܂őҋ@
            await UniTask.Yield();
            
            if (!_isFollow) break;// �Ǐ]�҂����Ȃ��Ȃ烋�[�v�𔲂���
        }
        // Chara��Action���C���[�̓����蔻������ɖ߂�
        Physics.IgnoreLayerCollision(_charaLayer, _actionLayer, false);
    }
    /// <summary>
    /// �����̏����@�A�j���[�V�������s��
    /// </summary>
    /// <param name="chara">����������Transform</param>
    /// <param name="animator">�����������A�j���[�^�[</param>
    private async UniTaskVoid Move(Transform chara,Animator animator)
    {
        animator.SetBool(_boolAnimatorParameter, true);//TODO: ""�̒������� �A�j���[�V�����J�n
        Vector3 pos = chara.position;
        await chara.DOLocalMove(pos + _moveValue, _moveSpeed).WithCancellation(Token);// �ړ�
        animator.SetBool(_boolAnimatorParameter, false);//TODO: ""�̒��������@�A�j���[�V�����I��
    }
}
