using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;



[RequireComponent(typeof(LineRenderer))]
public class ControllerTeleportAndGrab : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin;                    // XR Origin (sahnedeki rig root)
    public Transform leftHandTransform;          // sol controller transform (Action Based Controller objesi)
    public Transform rightHandTransform;         // sað controller transform

    [Header("Input Actions (ActionBased)")]
    public InputActionProperty leftTriggerAction; // genelde "Left Hand - Select" / Trigger
    public InputActionProperty rightTriggerAction;
    public InputActionProperty leftGripAction;    // genelde grip use for attach
    public InputActionProperty rightGripAction;

    [Header("Ray / Teleport Ayarlarý")]
    public float maxDistance = 30f;
    public LayerMask validTeleportMask = ~0; // hangi layer'lara ýþýnlanabilir (default: everything)
    public GameObject reticlePrefab;         // opsiyonel: hedefe reticle spawn
    public float reticleYOffset = 0.0f;      // reticle y offset

    [Header("Grab/Attach Ayarlarý")]
    public bool parentToHitTransformIfAvailable = true; // eðer hit Transform varsa ona parent et
    public Transform currentAttachParentLeft = null;
    public Transform currentAttachParentRight = null;

    // internal
    private GameObject leftReticleInstance;
    private GameObject rightReticleInstance;

    // cache camera offset between xrOrigin root and camera
    private Transform mainCamera;

    void Start()
    {
        if (xrOrigin == null)
            Debug.LogError("xrOrigin referansý eksik!");

        mainCamera = Camera.main ? Camera.main.transform : null;
        if (mainCamera == null)
            Debug.LogWarning("Main Camera bulunamadý. Kamera referansýný kontrol et.");

        if (reticlePrefab != null)
        {
            leftReticleInstance = Instantiate(reticlePrefab);
            rightReticleInstance = Instantiate(reticlePrefab);
            leftReticleInstance.SetActive(false);
            rightReticleInstance.SetActive(false);
        }

        // Enable actions if they are not enabled
        EnableAction(leftTriggerAction);
        EnableAction(rightTriggerAction);
        EnableAction(leftGripAction);
        EnableAction(rightGripAction);

        // Bind performed callbacks
        if (leftTriggerAction.action != null) leftTriggerAction.action.performed += ctx => OnTriggerPerformed(true);
        if (rightTriggerAction.action != null) rightTriggerAction.action.performed += ctx => OnTriggerPerformed(false);

        if (leftGripAction.action != null) leftGripAction.action.performed += ctx => OnGripPerformed(true);
        if (rightGripAction.action != null) rightGripAction.action.performed += ctx => OnGripPerformed(false);
    }

    void OnDestroy()
    {
        DisableAction(leftTriggerAction);
        DisableAction(rightTriggerAction);
        DisableAction(leftGripAction);
        DisableAction(rightGripAction);
    }

    void EnableAction(InputActionProperty prop)
    {
        if (prop == null || prop.action == null) return;
        if (!prop.action.enabled) prop.action.Enable();
    }

    void DisableAction(InputActionProperty prop)
    {
        if (prop == null || prop.action == null) return;
        if (prop.action.enabled) prop.action.Disable();
    }

    void Update()
    {
        // Ýsteðe baðlý: ray görselleþtirme ve reticle güncelle
        UpdateReticle(leftHandTransform, leftReticleInstance);
        UpdateReticle(rightHandTransform, rightReticleInstance);
    }

    void UpdateReticle(Transform hand, GameObject reticle)
    {
        if (hand == null || reticle == null) return;

        Ray ray = new Ray(hand.position, hand.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, validTeleportMask))
        {
            reticle.SetActive(true);
            reticle.transform.position = hit.point + Vector3.up * reticleYOffset;
            // rotasyonu hafif yüzey normaline göre ayarla
            reticle.transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            reticle.SetActive(false);
        }
    }

    // triggerPerformed: parameter isLeftController
    void OnTriggerPerformed(bool isLeft)
    {
        Transform hand = isLeft ? leftHandTransform : rightHandTransform;
        if (hand == null) return;

        if (Physics.Raycast(new Ray(hand.position, hand.forward), out RaycastHit hit, maxDistance, validTeleportMask))
        {
            Vector3 targetPoint = hit.point;
            TeleportToPointKeepingHeight(targetPoint);
        }
    }

    // gripPerformed: toggle attach/detach
    void OnGripPerformed(bool isLeft)
    {
        Transform hand = isLeft ? leftHandTransform : rightHandTransform;
        if (hand == null) return;

        if (Physics.Raycast(new Ray(hand.position, hand.forward), out RaycastHit hit, maxDistance, validTeleportMask))
        {
            Transform hitTransform = hit.transform;
            // attach
            if (isLeft)
            {
                if (currentAttachParentLeft == null)
                    AttachTo(hitTransform, true, hit.point);
                else
                    DetachFrom(true);
            }
            else
            {
                if (currentAttachParentRight == null)
                    AttachTo(hitTransform, false, hit.point);
                else
                    DetachFrom(false);
            }
        }
    }

    void TeleportToPointKeepingHeight(Vector3 point)
    {
        if (xrOrigin == null || mainCamera == null) return;

        // Kamera ile xrOrigin arasýndaki offset'i hesapla:
        Vector3 cameraWorldPos = mainCamera.position;
        Vector3 originWorldPos = xrOrigin.transform.position;
        Vector3 cameraOffset = cameraWorldPos - originWorldPos;

        // Yeni origin pozisyonu = hedef nokta - cameraOffset (kamera ayný dünya pozisyonunu alýr)
        Vector3 newOriginPos = point - cameraOffset;

        // Yükseklik koruma: istersen yeniOriginPos.y = xrOrigin.transform.position.y; diye sabitleyebilirsin
        xrOrigin.transform.position = newOriginPos;
    }

    void AttachTo(Transform target, bool leftHand, Vector3 hitPoint)
    {
        if (xrOrigin == null) return;

        // Eðer transform parent'ý yoksa, bir anchor yaratýp orayý parent yap (yerel offset'i korumak için)
        Transform attachParent = null;

        if (parentToHitTransformIfAvailable && target != null)
        {
            attachParent = target;
        }
        else
        {
            GameObject anchor = new GameObject("TeleportAttachAnchor");
            anchor.transform.position = hitPoint;
            attachParent = anchor.transform;
        }

        // Parent et ama world position'ý koru
        xrOrigin.transform.SetParent(attachParent, true);

        if (leftHand)
            currentAttachParentLeft = attachParent;
        else
            currentAttachParentRight = attachParent;

        Debug.Log($"Attached XR Origin to {attachParent.name}");
    }

    void DetachFrom(bool leftHand)
    {
        if (xrOrigin == null) return;

        // Sadece detach ilgili elin parent'ýný kaldýrýr. Eðer diðer el de baðlandýysa XR Origin baþka bir parent'ta olabilir.
        if (leftHand)
        {
            if (currentAttachParentLeft != null)
            {
                // eðer hem left hem right attach varsa diðerinin baðlý olup olmadýðýna bak
                currentAttachParentLeft = null;
                // detach sadece eðer hiç parent kalmadýysa kökten detach et
                if (currentAttachParentRight == null)
                {
                    xrOrigin.transform.SetParent(null, true);
                    Debug.Log("Detached XR Origin (left)");
                }
            }
        }
        else
        {
            if (currentAttachParentRight != null)
            {
                currentAttachParentRight = null;
                if (currentAttachParentLeft == null)
                {
                    xrOrigin.transform.SetParent(null, true);
                    Debug.Log("Detached XR Origin (right)");
                }
            }
        }
    }
}
