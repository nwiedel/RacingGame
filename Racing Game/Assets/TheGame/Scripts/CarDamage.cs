using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasse, die den Schadensstatus des Spielers darstellt.
/// </summary>
public class CarDamage : MonoBehaviour
{
    [SerializeField]
    private Sprite[] carSprites;
    [Range(0, 4)][SerializeField]
    private int damageState;

    [SerializeField] int maxHealth = 100;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    [Header("Damage")]
    [SerializeField] private int obstacleDamage = 15;
    [SerializeField] private int trackDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Kollision erkennen 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Objekt prüfen
        // 7 Obstacle Kollision
        // 8 Track Collision
        if(collision.gameObject.layer == 8)
        {
            TakeDamage(trackDamage);
        }
        if(collision.gameObject.layer == 7)
        {
            TakeDamage(obstacleDamage);
        }
        // Schaden anwenden

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth > 0)
        {
            UpdateVehicleState();
        }
    }

    public void UpdateVehicleState()
    {
        // Sicherstellen, das die Gesundheit nicht unter null fällt
        currentHealth = Mathf.Max(currentHealth, 0);

        // Berechne den Index für das  entsprechende Sprite
        // maxHealth / anzahl der Zustände (Sprites)
        int junks = maxHealth / carSprites.Length;
        //               100  /   5    = 20
        int healthDiff = maxHealth - currentHealth;
        //                20  /  20    =  1
        int spriteIndex = Mathf.Min(healthDiff / junks, carSprites.Length - 1);

        print($"HealthDiff: {healthDiff} SpriteIndex: {spriteIndex} currentHealt: {currentHealth}");

        // Sprite aktualisieren
        spriteRenderer.sprite = carSprites[spriteIndex];
    }
}
