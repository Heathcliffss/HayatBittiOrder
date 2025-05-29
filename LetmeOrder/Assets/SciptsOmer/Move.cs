using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController characterController;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private float currentSpeed;

    public float jumpForce = 5f;
    public float gravity = -12.81f;
    private float verticalVelocity;

    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Zemin kontrolü
        isTouchingGround = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // Koþma kontrolü (Shift ile)
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Zýplama kontrolü (Space ile)
        if (isTouchingGround && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Yerçekimi uygulama
        if (isTouchingGround && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // yere yapýþýk kalmasýný saðla
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Hareket vektörü (x ve z)
        Vector3 moveVector = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        // Hareketin son hali
        Vector3 finalMove = moveVector * currentSpeed;
        finalMove.y = verticalVelocity;

        // Hareket uygula
        characterController.Move(finalMove * Time.deltaTime);
    }
}
