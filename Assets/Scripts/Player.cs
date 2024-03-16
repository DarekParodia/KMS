using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float maxHP = 100.0f;
    [SerializeField] private float currentHP = 100.0f;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletTTL = 2.0f;
    [SerializeField] private float bulletVelo = 10.0f;
    [SerializeField] private float gunSmoothness = 15.0f;
    [SerializeField] private bool isGamepad = false;
    
    PlayerInput playerInput;
    private Vector2 look;
    void Start()
    {
        this.playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
     void Update()
     {
         if (isGamepad)
         {
             if (look != Vector2.zero)
             {
                 Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg);
                 gun.transform.rotation =
                     Quaternion.Lerp(gun.transform.rotation, targetRotation, gunSmoothness * Time.deltaTime);
             }
         }
         else
         {
             // calculate angle beetwen player and mouse
             Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
             Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg);
                gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, targetRotation, gunSmoothness * Time.deltaTime);
         }
     }

    
     void OnLook(InputValue value)
     {
         look = value.Get<Vector2>().normalized;
     }

     void OnFire(InputValue value)
     {
         // create bullet child at gun position
         GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
         bullet.GetComponent<Rigidbody2D>().velocity = gun.transform.right * bulletVelo;
         Destroy(bullet, bulletTTL);
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
