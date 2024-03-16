using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadCaser : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite gamepadSprite;
    [SerializeField] private Sprite keyboardSprite;
    [SerializeField] private Player player;
    // Update is called once per frame
    void Update()
    {
        if (player.isGamepadControl())
        {
            spriteRenderer.sprite = gamepadSprite;
        }
        else
        {
            spriteRenderer.sprite = keyboardSprite;
        }
    }
}
