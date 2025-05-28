using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    // Component Variables
    Image healthBarFill;
    [HideInInspector] public AudioSource audioSource; // Initializing here, using at Player.cs.

    // Variables
    public float maxHp;
    public float curHp;

    void Start()
    {
        healthBarFill = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        curHp = maxHp;
    }

    void Update()
    {
        healthBarFill.fillAmount = curHp / maxHp; // Change the healt bar.
    }
}
