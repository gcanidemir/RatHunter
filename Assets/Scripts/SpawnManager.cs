using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnManager : MonoBehaviour
{
    // Component Variables
    [HideInInspector] public HealthBarBehaviour healthBarScript;
    [HideInInspector] public Text winText;

    // Variables
    public GameObject enemy;
    public GameObject player;
    int level;
    int enemyCount;

    void Start()
    {
        // Level 1
        level = 1;
        Instantiate(enemy, new Vector3(0, 0.677f, 0), transform.rotation);
    }

    void Update()
    {
        // If no enemies, start the next level.
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            level++;
            player.transform.position = new Vector3(0, -3.333f, 0);
            healthBarScript.curHp = 3;

            if (level == 2) // Level 2
            {
                Instantiate(enemy, new Vector3(0, 0.677f, 0), transform.rotation);
                Instantiate(enemy, new Vector3(-6.784f, -1.55f, 0), transform.rotation);
            }
            else if (level == 3) // Level 3
            {
                Instantiate(enemy, new Vector3(0, 0.677f, 0), transform.rotation);
                Instantiate(enemy, new Vector3(-6.784f, -1.55f, 0), transform.rotation);
                Instantiate(enemy, new Vector3(6.784f, -1.55f, 0), transform.rotation);
            }
            else // If won the game.
            {
                winText.gameObject.SetActive(true);
            }
        }
    }
}
