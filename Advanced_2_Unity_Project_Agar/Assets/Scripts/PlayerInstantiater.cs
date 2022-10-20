using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerInstantiater : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject player_pref;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "game")
        {
            Vector3 posPlayer = new Vector3(Random.Range(-19f, 19f), Random.Range(-19f, 19f));
            player = PhotonNetwork.Instantiate(player_pref.name, posPlayer, Quaternion.identity);
            player.GetComponent<SpriteRenderer>().color = Color.green; // «м≥нити кол≥р гравц€
        }
    }
}
