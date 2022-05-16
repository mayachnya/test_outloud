using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    MyInputActions inputActions;
    Vector2 move;
    Vector2 rotate;
    float run;
    float jumpStarted;
    public float walkSpeed = 3f;
    public float runSpeed = 10f;
    public Camera playerCamera;
    private Animator ch_animator;
    Vector3 CameraRotation;

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Awake()
    {
        inputActions = new MyInputActions();

        inputActions.Player.Jump.performed += ctx => Jump();

        inputActions.Player.Run.performed += ctx => run = runSpeed;
        inputActions.Player.Run.canceled += ctx => run = walkSpeed;

        inputActions.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => move = Vector2.zero;

        inputActions.Player.Look.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => rotate = Vector2.zero;
    }

    private void Move()
    {
        if (transform.rotation.x < 0.1f && transform.rotation.x > -0.1f)
            ch_animator.SetBool("Move", true);
        else ch_animator.SetBool("Move", false);
    }

    private void Jump()
    {
        if (jumpStarted + 1f < Time.time)
            jumpStarted = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {

        run = walkSpeed;
        CameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        jumpStarted = -1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation = new Vector3(CameraRotation.x + rotate.y, CameraRotation.y + rotate.x, CameraRotation.z);

        playerCamera.transform.eulerAngles = CameraRotation;
        transform.eulerAngles = new Vector3(transform.rotation.x, CameraRotation.y, transform.rotation.z);

        transform.Translate(Vector3.right * Time.deltaTime * move.x * run, Space.Self);
        transform.Translate(Vector3.forward * Time.deltaTime * move.y * run, Space.Self);

        if (jumpStarted + 0.5f > Time.time)
        {
            transform.Translate((Vector3.up * 8f * Time.deltaTime), Space.Self);
        }
        else if (jumpStarted + 1f > Time.time)
        {
            transform.Translate((Vector3.up * -8f * Time.deltaTime), Space.Self);
        }
    }
}