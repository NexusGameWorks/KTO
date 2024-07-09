using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MM_PlayerPhaseState))]

public class MM_Test_Player: MonoBehaviour
{
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _JumpHeight;
    [SerializeField]
    private float _MoveSpeed;
    [SerializeField]
    private Material[] _playerMaterials = new Material[2];

    bool isOnGround = false;

    Rigidbody _rb;
    PlayerInput _playerInput;
    MeshRenderer _meshRenderer;
    [SerializeField]
    MM_PlayerPhaseState pState;

    private Vector3 _velocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _meshRenderer = GetComponent<MeshRenderer>();
        pState = GetComponent<MM_PlayerPhaseState>();

        if (_playerInput.user.index == 0)
            _meshRenderer.material = _playerMaterials[0];
        else
            _meshRenderer.material = _playerMaterials[1];

        pState.ChangeState(MM_PlayerPhaseState.State.Liquid);
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        print("Player:" + pState.GetState());
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
        isOnGround = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        isOnGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }


    // ���\�b�h���͉��ł�OK
    // public�ɂ���K�v������
    public void OnMove(InputAction.CallbackContext context)
    {
        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // 2D�Ȃ̂ŉ��ړ�����
        _velocity = new Vector3(axis.x*_MoveSpeed, 0, 0);
    }
    int callOnJumpCount = 0;
    public void OnJump(InputAction.CallbackContext context)
    {
        // �������u�Ԃ�����������
        if (!context.performed) return;

        // �������ł���������if���̒��ɂȂ��Č��Â炭�Ȃ�
        //if (context.performed)

        // �n�ʂɂ��Ȃ��Ȃ璵�ׂȂ�
        if (!isOnGround) return;


        _rb.AddForce(new Vector3(0, _JumpHeight, 0), ForceMode.VelocityChange);

        print(callOnJumpCount++ + ":Jump��������܂���");
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

        print("SLIME(�X���C��)�ɂȂ�܂���");

    }
}
