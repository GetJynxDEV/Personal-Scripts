using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCinemachine : MonoBehaviour
{
    [Header("Camera Settings")]
    public CinemachineCamera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimitDown = 90f;
    public float lookXLimitUp = -70f;

    private PlayerInput playerInput;
    private float rotationX = 0f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null) DebugManager.LogMissingComponent(gameObject, "PlayerCamera", "PlayerInput");
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        rotationX += -playerInput.lookInput.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, lookXLimitUp, lookXLimitDown);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, playerInput.lookInput.x * lookSpeed, 0f);
    }

    public void SetLookSpeed(float speed)
    {
        lookSpeed = speed;
    }

    public void SetVerticalLimits(float limitUp, float limitDown)
    {
        lookXLimitUp = limitUp;
        lookXLimitDown = limitDown;
    }
}
