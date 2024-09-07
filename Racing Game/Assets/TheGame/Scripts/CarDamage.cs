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

    // Start is called before the first frame update
    void Start()
    {
        if(damageState >= 0 && damageState < carSprites.Length)
        {
            GetComponent<SpriteRenderer>().sprite = carSprites[damageState];
        }
        
    }
}
