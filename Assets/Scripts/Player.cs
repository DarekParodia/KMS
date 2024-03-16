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
    [SerializeField] private float bulletKnockbackFactor = 1.0f;
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
         if (gun.transform.childCount > 0)
         {
             
             // Get the position of the first child of the gun
             Transform factGun = gun.transform.GetChild(0);

             // Create an offset for the bullet position
             // Vector3 bulletOffset = new Vector3(0, 0.5f, 0); // Adjust the y value as needed

             // Create bullet at the position of the first child of the gun plus the offset
             GameObject bullet = Instantiate(bulletPrefab, factGun.position, factGun.rotation);
             bullet.GetComponent<Rigidbody2D>().velocity = gun.transform.right * bulletVelo;
             // bullet.GetComponent<Bullet>().bulletKnockbackFactor = bulletKnockbackFactor;
             // bullet.GetComponent<Bullet>().bulletVelo = bulletVelo;
             Destroy(bullet, bulletTTL);
         }
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
