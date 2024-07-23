using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MM_PlayerPhaseState))]

public class MM_Test_Player: MonoBehaviour
{
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _JumpPower;
    [SerializeField]
    private float _MoveSpeed;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;
    bool isOnWater = false;

    Rigidbody _rb;
    PlayerInput _playerInput;
    MeshRenderer _meshRenderer;
    [SerializeField]
    MM_PlayerPhaseState pState;

    [SerializeField]
    TextMeshProUGUI Debug_Phasetext;

    private Vector3 _velocity;

    public GameObject[] characters;  // �L�����N�^�[�I�u�W�F�N�g�̔z��
    private int currentIndex = 0;    // ���݂̃L�����N�^�[�C���f�b�N�X


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        pState = GetComponent<MM_PlayerPhaseState>();

        //if (_playerInput.user.index == 0)
        //    _meshRenderer.material = _playerMaterials[0];
        //else
        //    _meshRenderer.material = _playerMaterials[1];

        pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        // �����̃L�����N�^�[�̐ݒ�
        SetActiveCharacter(0);
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        Debug_Phasetext.text = "Player:" + pState.GetState();
        //print("Player:" + pState.GetState());
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        _rb.AddForce(new Vector3(0, -_gravity, 0), ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        isOnGround = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isOnGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isOnGround = false;
    }


    // ���\�b�h���͉��ł�OK
    // public�ɂ���K�v������
    public void OnMove(InputAction.CallbackContext context)
    {
        // �ő̂̎����ɐG��ĂȂ������瓮���Ȃ�
        if(pState.GetState()==MM_PlayerPhaseState.State.Solid)
            if (!isOnWater) return;
        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // 2D�Ȃ̂ŉ��ړ�����
        _velocity = new Vector3(axis.x*_MoveSpeed, 0, 0);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        // �������u�Ԃ�����������
        if (!context.performed) return;
        // �n�ʂɂ��Ȃ��Ȃ璵�ׂȂ�
        if (!isOnGround) return;
        // ���ɐG��Ă����璵�ׂȂ�
        if (isOnWater) return;
        // �C�̂Ȃ璵�ׂȂ�
        if (pState.GetState() == MM_PlayerPhaseState.State.Gas) return;


        _rb.AddForce(new Vector3(0, _JumpPower, 0), ForceMode.VelocityChange);

        print("Jump��������܂���");
    }

    /// <summary>
    /// �C�̂֕ω�
    /// </summary>
    public void OnStateChangeGas(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Gas);

        // ���f�����C�̂̂�ɕς��鏈��
        // ���݂̃L�����N�^�[���A�N�e�B�u�ɐݒ�
        SetActiveCharacter(1);

        print("GAS(�C��)�ɂȂ�܂���");
    }

    /// <summary>
    /// �ő̂֕ω�
    /// </summary>
    public void OnStateChangeSolid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Solid);

        // ���f�����ő̂̂�ɕς��鏈��
        // ���݂̃L�����N�^�[���A�N�e�B�u�ɐݒ�
        SetActiveCharacter(2);

        print("SOLID(�ő�)�ɂȂ�܂���");
    }
    /// <summary>
    /// �t�̂֕ω�
    /// </summary>
    public void OnStateChangeLiquid(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // �ő́E�C�́E�X���C������Ȃ�������󂯕t���Ȃ�
        if (pState.GetState() == MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Liquid);

        // ���f���𐅂̂�ɕς��鏈��
        // ���݂̃L�����N�^�[���A�N�e�B�u�ɐݒ�
        SetActiveCharacter(0);

        print("LIQUID(��)�ɂȂ�܂���");
    }

    /// <summary>
    /// �X���C���֕ω�
    /// </summary>
    public void OnStateChangeSlime(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // ������Ȃ�������󂯕t���Ȃ�
        if (pState.GetState() != MM_PlayerPhaseState.State.Liquid) return;

        pState.ChangeState(MM_PlayerPhaseState.State.Slime);

        // ���f�����X���C���̂�ɕς��鏈��
        //
        //

        print("SLIME(�X���C��)�ɂȂ�܂���");

    }

    private void SetActiveCharacter(int index)
    {
        if(index < 0 || index >= characters.Length)
            return;

        characters[currentIndex].SetActive(false);  // ���̃L�����N�^�[�̏�Ԃ��A�N�e�B�u�ɂ���
        characters[index].SetActive(true);  // index�Ŏw�肵����Ԃ��A�N�e�B�u�ɂ���
        currentIndex = index;  // ���̃C���f�b�N�X���X�V����

    }
}
