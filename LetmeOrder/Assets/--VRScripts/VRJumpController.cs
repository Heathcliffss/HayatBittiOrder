using UnityEngine;
using Oculus;

[RequireComponent(typeof(Oculus.Interaction.Locomotion.CharacterController))]
public class VRJumpController : MonoBehaviour
{
    public float jumpForce = 2.5f;
    public float gravity = -9.81f;
    public float fallSpeedClamp = -10f; // En fazla ne kadar hýzlý düþebileceðini sýnýrla

    private Oculus.Interaction.Locomotion.CharacterController characterController;
    private bool isJumping = false;
    private float verticalVelocity = 0f;

    void Start()
    {
        characterController = GetComponent<Oculus.Interaction.Locomotion.CharacterController>();
    }

    void Update()
    {
        // Grip tuþuyla zýplama
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            if (characterController.IsGrounded)
            {
                isJumping = true;
                verticalVelocity = jumpForce;
            }
        }

        // Yerçekimi uygula
        if (!characterController.IsGrounded || verticalVelocity > 0)
        {
            verticalVelocity += gravity * Time.deltaTime;

            // Aþýrý hýzlý düþüþü sýnýrlýyoruz
            verticalVelocity = Mathf.Max(verticalVelocity, fallSpeedClamp);

            characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        }
        else
        {
            // Yere deðdiðimizde yumuþak bir þekilde dur
            verticalVelocity = 0;
            isJumping = false;
        }
    }
}
