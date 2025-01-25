using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwinScriptMovement : MonoBehaviour
{
    // Existing variables
    private Vector2 movement;
    private Vector2 aim;
    private bool isGamePad;
    private CharacterController controller;
    private PlayerControls playerControls;
    private float playerSpeed = 5f;
    private float dodgeSpeed = 10f;
    private float dodgeDuration = 0.2f;
    private float gamepadRotateSmoothing = 500f;
    private float controllerDeadzone = 0.1f;
    private bool isDodging = false;

    // New variables for shooting
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Controls.Dodge.performed += ctx => Dodge();
        playerControls.Controls.Fire.performed += ctx => Fire();
        playerControls.Controls.AltFire.performed += ctx => AltFire();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        HandleInput();
        if (!isDodging)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, movement.y, 0f);
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void HandleRotation()
    {
        if (isGamePad)
        {
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 aimDirection = Vector3.right * aim.x + Vector3.up * aim.y;
                if (aimDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Vector3 mousePosition = new Vector3(aim.x, aim.y, Camera.main.nearClipPlane);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = worldPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjusting by -90 degrees
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Dodge()
    {
        if (!isDodging)
        {
            StartCoroutine(DodgeCoroutine());
        }
    }

    private IEnumerator DodgeCoroutine()
    {
        isDodging = true;
        Vector3 dodgeDirection = new Vector3(movement.x, movement.y, 0f).normalized;

        float startTime = Time.time;
        while (Time.time < startTime + dodgeDuration)
        {
            controller.Move(dodgeDirection * dodgeSpeed * Time.deltaTime);
            yield return null;
        }

        isDodging = false;
    }

    private void Fire()
    {
        ShootProjectile();
    }

    private void AltFire()
    {
        ShootProjectile();
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * projectileSpeed;
    }

    private void LookAt(Vector3 point)
    {
        Debug.Log("LookAt called with point: " + point);
        Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnDeviceChange(PlayerInput playerInput)
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            isGamePad = true;
        }
        else
        {
            isGamePad = false;
        }
    }
}