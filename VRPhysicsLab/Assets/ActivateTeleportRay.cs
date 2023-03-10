using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportRay : MonoBehaviour
{
    public GameObject leftTeleport;
    public GameObject rightTeleport;

    public InputActionProperty rightActivate;
    public InputActionProperty leftActivate;

    public InputActionProperty rightCancel;
    public InputActionProperty leftCancel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rightTeleport.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
        leftTeleport.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
    }
}
