using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] coral_pref;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "game")
        {
            int rand = Random.Range(0, coral_pref.Length);
            Instantiate(coral_pref[rand], transform.position, Quaternion.identity);
        }

    }
}
