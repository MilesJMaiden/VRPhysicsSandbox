using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportRay : MonoBehaviour
{
    public GameObject leftTeleport;
    public GameObject rightTeleport;

    public InputActionProperty rightActivate;
    public InputActionProperty leftActivate;

    public InputActionProperty rightCancel;
    public InputActionProperty leftCancel;

    public XRRayInteractor rightRay;
    public XRRayInteractor leftRay;
    

    // Update is called once per frame
    void Update()
    {
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPost, out Vector3 rightNormal, out int rightNumber, out bool rightValid);
        rightTeleport.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);

        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPost, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        leftTeleport.SetActive(!isLeftRayHovering &&leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
    }
}
