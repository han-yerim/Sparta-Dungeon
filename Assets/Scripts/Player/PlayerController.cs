using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed = 5f;
    public float jumpPower = 100f;
    private Vector2 curMovementInput; // 현재 입력값
    public LayerMask groundLayerMask;

    private float baseMoveSpeed;
    private float baseJumpPower;

    private Coroutine speedBuffRoutine;
    private Coroutine jumpBuffRoutine;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity; // 카메라 회전 민감도
    private Vector2 mouseDelta; // 마우스 변화값
    public Action inventory;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기
        baseMoveSpeed = moveSpeed;
        baseJumpPower = jumpPower;
    }

    // 물리 연산 있으면 Fixed로 쓰는게 좋음
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }


    public void OnMove(InputAction.CallbackContext context) // CallbackContext 현재상태 받아오기
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void ApplySpeedBuff(float multiplier, float duration)
    {
        if (speedBuffRoutine != null)
            StopCoroutine(speedBuffRoutine);

        speedBuffRoutine = StartCoroutine(SpeedBuff(multiplier, duration));
    }

    public void ApplyJumpBuff(float multiplier, float duration)
    {
        if (jumpBuffRoutine != null)
            StopCoroutine(jumpBuffRoutine);

        jumpBuffRoutine = StartCoroutine(JumpBuff(multiplier, duration));
    }

    private IEnumerator SpeedBuff(float multiplier, float duration)
    {
        moveSpeed = baseMoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseMoveSpeed;
        speedBuffRoutine = null;
    }

    private IEnumerator JumpBuff(float multiplier, float duration)
    {
        jumpPower = baseJumpPower * multiplier;
        yield return new WaitForSeconds(duration);
        jumpPower = baseJumpPower;
        jumpBuffRoutine = null;
    }
}
