using UnityEngine;
using Photon.Pun;
using System.Collections;

public class PlayerController : MonoBehaviour, IPunObservable
{
    private PhotonView View;
    private SpriteRenderer _spriteRenderer;

    [Header("Ўвидк≥сть перем≥щенн€ гравц€")]
    public float spead = 5f;

    [Header("–озм≥р гравц€")]
    public float _size = 0.4f;

    private float _enemySize;

    private Vector2 mapSizeUp = new Vector2(20, 20);
    private Vector2 mapSizeDown = new Vector2(-20, -20);

    [SerializeField] private PointController point_pref;

    private Vector2Int Direction;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Direction);
        }
        else
        {
            Direction = (Vector2Int)stream.ReceiveNext();
        }
    }

    void Start()
    {
        View = GetComponent<PhotonView>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (View.Owner.IsLocal)
        {
            Camera.main.GetComponent<CameraController>()._playerTransform = gameObject.transform;
        }
    }

    void Update()
    {
        GetInput();
        CheckBoundsUp();
        CheckBoundsDown();
    }

    private void GetInput()
    {
        if (View.IsMine)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {

                transform.localPosition += transform.up * spead * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {

                transform.localPosition += -transform.up * spead * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localPosition += -transform.right * spead * Time.deltaTime;
                Direction = Vector2Int.left;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localPosition += transform.right * spead * Time.deltaTime;
                Direction = Vector2Int.right;
            }
        }

        if (Direction == Vector2Int.left)
        {
            _spriteRenderer.flipX = false;
        }

        if (Direction == Vector2Int.right)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void CheckBoundsUp()
    {
        if (View.IsMine)
        {
            if (transform.localPosition.x >= mapSizeUp.x)
            {
                transform.localPosition = new Vector3(mapSizeUp.x - 0.01f,
                    transform.localPosition.y, 0);
            }

            if (transform.localPosition.y >= mapSizeUp.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    mapSizeUp.y - 0.01f, 0);
            }
        }
    }

    private void CheckBoundsDown()
    {
        if (View.IsMine)
        {
            if (transform.localPosition.x <= mapSizeDown.x)
            {
                transform.localPosition = new Vector3(mapSizeDown.x + 0.01f,
                    transform.localPosition.y, 0);
            }

            if (transform.localPosition.y <= mapSizeDown.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,
                    mapSizeDown.y + 0.01f, 0);
            }
        }
    }

    private void SendChangeSize()
    {
        View.RPC("Send_ChangeSize", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Send_ChangeSize()
    {
        _size += 0.2f;
        spead = 1 / _size;
        transform.localScale = new Vector3(_size, _size, _size);
    }

    //private void SendChangeSizeForKillEnemy()
    //{
    //    View.RPC("Send_ChangeSizeForKillEnemy", RpcTarget.AllBuffered);
    //}

    //[PunRPC]
    //private void Send_ChangeSizeForKillEnemy()
    //{
    //    _size += _enemySize;
    //    spead = 1 / _size;
    //    transform.localScale = new Vector3(_size, _size, _size);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bounds enemy = other.bounds;
        Bounds current = GetComponent<Collider2D>().bounds;

        Vector2 centerEnemy = enemy.center;
        Vector2 centerCurrent = current.center;

        if (transform.localScale.x == other.transform.localScale.x)
        {
            Debug.Log("return");
            return;
        }
        else if (current.size.x > enemy.size.x &&
            Vector3.Distance(centerCurrent, centerEnemy) < current.size.x)
        {
            if (other.GetComponent<PointController>())
            {
                if (View.IsMine)
                {
                    SendChangeSize();
                    SendPointInstantiateAfterPointKill();
                }

            }
            //else
            //{
            //    if (View.IsMine)
            //    {
            //        _enemySize = other.transform.localScale.x;
            //        SendChangeSizeForKillEnemy();
            //    }
            //}
        }
        else
        {
            if (View.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
                PhotonNetwork.LeaveRoom();
            }
        }
    }

    public void SendPointInstantiateAfterPointKill()
    {
        View.RPC("Point_InstantiateAfterPointKill", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Point_InstantiateAfterPointKill()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 pos = new Vector3(Random.Range(-19f, 19f), Random.Range(-19f, 19f));
            PhotonNetwork.InstantiateRoomObject(point_pref.name, pos, Quaternion.identity);
        }
    }
}
