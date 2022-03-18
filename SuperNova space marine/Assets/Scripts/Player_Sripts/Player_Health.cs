using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Health : MonoBehaviour
{
    public float health;
    public float targetHealth;
    private Image healthBar;
    private TMP_Text healthText;
    bool voiceMessage = false;

    void Start()
    {
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TMP_Text>();
        healthBar = GameObject.FindGameObjectWithTag("hpBar").GetComponent<Image>();

        targetHealth = 100;
        health = 100;
    }

    void Update()
    {
        healthText.text = "Health: " + health + "/" + targetHealth;
        healthBar.fillAmount = health / 100;
        if (health > targetHealth) // prevents the player from exceeding max health
        {
            health = health - (health - targetHealth);
        }


        if (health < 25 && voiceMessage == false)
        {
            SFX_Manager.instance.Play("Health_Critical");
            voiceMessage = true;
        }
        else if (health > 25 && voiceMessage == true)
        {
            voiceMessage = false;
        }

    }

}
