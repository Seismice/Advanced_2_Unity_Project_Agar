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
            player = PhotonNetwork.Instantiate(player_pref.name, Vector3.zero, Quaternion.identity);
            player.GetComponent<SpriteRenderer>().color = Color.green; // «м≥нити кол≥р гравц€
        }
    }
}
