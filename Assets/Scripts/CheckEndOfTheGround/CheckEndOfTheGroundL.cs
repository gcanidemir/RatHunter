using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndOfTheGroundL : MonoBehaviour
{
    Enemy parentScript;

    void Start()
    {
        parentScript = GetComponentInParent<Enemy>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // If ground ends, turn right.
        if (collision.gameObject.CompareTag("Ground"))
        {
            parentScript.goRight = true;
        }
    }
}

