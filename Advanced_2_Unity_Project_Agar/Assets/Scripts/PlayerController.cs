using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private PhotonView View;

    [Header("Ўвидк≥сть перем≥щенн€ гравц€")]
    public float spead = 7f;

    private void Start()
    {
        View = GetComponent<PhotonView>();
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.UpArrow) && View.IsMine)
        {

            transform.localPosition += transform.up * spead * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) && View.IsMine)
        {

            transform.localPosition += -transform.up * spead * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && View.IsMine)
        {
            transform.localPosition += -transform.right * spead * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) && View.IsMine)
        {
            transform.localPosition += transform.right * spead * Time.deltaTime;
        }
    }
}
