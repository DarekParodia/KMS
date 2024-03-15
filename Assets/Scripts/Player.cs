using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float maxHP = 100.0f;
    [SerializeField] private float currentHP = 100.0f;
    [SerializeField] private GameObject gun;
    [SerializeField] private float gunSmoothness = 15.0f;
    
    PlayerInput playerInput;
    private Vector2 look;
    void Start()
    {
        this.playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
     void Update()
     {
         if (look != Vector2.zero) // Check if there is joystick input
         {
             Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg);
             gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, targetRotation, gunSmoothness * Time.deltaTime);
         }
     }

    
     void OnLook(InputValue value)
     {
         look = value.Get<Vector2>().normalized;
     }

     void setKeyboard()
     {
         this.playerInput.defaultControlScheme = "Keyboard";
     }

     void setGamepad()
     {
         this.playerInput.defaultControlScheme = "Gamepad";
     }
}   
