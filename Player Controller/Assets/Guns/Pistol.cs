using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour
{
    public GunData gun_data;
    public Camera cam;
    private Ray ray; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000, Color.green); 
    }

    public void GetPrimaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) PrimaryFire(); 
    }

    public void GetSecondaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) SecondaryFire();
    }

    private void PrimaryFire()
    {
        //raycast 
        ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit; 
        if(Physics.Raycast(ray, out hit, gun_data.range))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 0.05f); 
        }
    }

    private void SecondaryFire()
    {

    }
}