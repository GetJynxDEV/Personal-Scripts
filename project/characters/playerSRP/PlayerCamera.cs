using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimitDown = 90f;
    public float lookXLimitUp = -70f;

    private PlayerInput playerInput;
    private float rotationX = 0f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null ) DebugManager.LogMissingComponent(gameObject, "PlayerInput");

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        rotationX += -playerInput._lookInput.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, lookXLimitUp, lookXLimitDown);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, playerInput._lookInput.x * lookSpeed, 0f);
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
