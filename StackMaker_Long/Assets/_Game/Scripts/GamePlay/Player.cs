using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public enum Direct { Forward, Back, Right, Left, None }

public class Player : MonoBehaviour
{
    public List<Transform> brickPlayers = new List<Transform>();

    [SerializeField] private float speed;
    [SerializeField] private Transform brickPlayerPrefab;
    [SerializeField] private Transform holderBrick;

    private Vector3 mouseDown, mouseUp;
    public LayerMask layerMask;

    private bool isMoving;
    private bool isControl;
    private Vector3 moveNextPoint;

    void Update()
    {
        PlayerController();
    }

    private void PlayerController()
    {
        if (GameManager.Instance.InState(GameState.GamePlay) && !isMoving)
        {
            if (Input.GetMouseButtonDown(0) && !isControl)
            {
                mouseDown = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0) && isControl)
            {
                moveNextPoint = mouseUp = Input.mousePosition;
                Direct direct = GetDirect(mouseDown, mouseUp);
                if (direct != Direct.None)
                {
                    moveNextPoint = GetNextPoint(direct);
                    isMoving = true;
                }
            }
        }
        else if (isMoving)
        {
            if (Vector3.Distance(transform.position, moveNextPoint) < 0.1f)
            {
                isMoving = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, moveNextPoint, Time.deltaTime * speed);
            }
        }
    }
    private Direct GetDirect(Vector3 mouseDown, Vector3 mouseUp)
    {
        Direct direct = Direct.None;

        float deltaX = mouseUp.x - mouseDown.x;
        float deltaY = mouseUp.y - mouseDown.y;

        if (Vector3.Distance(mouseDown, mouseUp) < 50)
        {
            direct = Direct.None;
        }
        else
        {
            if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                if (deltaY > 0)
                {
                    direct = Direct.Forward;
                }
                else
                {
                    direct = Direct.Back;
                }
            }
            else
            {
                if (deltaX > 0)
                {
                    direct = Direct.Right;
                }
                else
                {
                    direct = Direct.Left;
                }
            }

        }
        return direct;
    }

    private Vector3 GetNextPoint(Direct direct)
    {
        RaycastHit hit;
        Vector3 nexPoint = transform.position;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case Direct.Forward:
                dir = Vector3.forward;
                break;
            case Direct.Back:
                dir = Vector3.back;
                break;
            case Direct.Right:
                dir = Vector3.right;
                break;
            case Direct.Left:
                dir = Vector3.left;
                break;
            case Direct.None:
                break;
            default:
                break;
        }

        for (int i = 0; i < 100; i++)
        {
            if (Physics.Raycast(transform.position + dir * i + Vector3.up * 2, Vector3.down, out hit, 10f, layerMask))
            {
                Debug.DrawLine(transform.position + dir * i, Vector3.up, Color.green, 10f);
                nexPoint = hit.collider.transform.position;

            }
            else
            {
                break;
            }
        }

        return nexPoint;
    }

    public void OnInit()
    {
        isMoving = false;
        isControl = false;
        ClearBrick();
        transform.localPosition = Vector3.zero;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    BrickColor otherHit = other.GetComponent<BrickColor>();

    //    if (other.CompareTag("BrickColor"))
    //    {
    //        if (brickPlayers.Count == 0)
    //        {
    //            if (!otherHit.isPassed)
    //            {
    //                Debug.Log("Mau Da cam");
    //                AddBrick(otherHit);
    //                otherHit.isPassed = true;
    //            }
    //        }
    //        if (otherHit.birckColors == brickPlayers[0].birckColors)
    //        {  
    //            if (!otherHit.isPassed)
    //            {
    //                Debug.Log("Mau xanh");
    //                AddBrick(otherHit);
    //                otherHit.isPassed = true;
    //            }

    //        }
    //    }
    //}

    public void AddBrick()
    {
        int index = brickPlayers.Count;

        Transform brickPlayer = Instantiate(brickPlayerPrefab, holderBrick);
        brickPlayer.localPosition = Vector3.up + index * 0.5f * Vector3.up;

        brickPlayers.Add(brickPlayer);

    }
        
    public void UnBrick()
    {
        int index = brickPlayers.Count - 1;
        if(index >= 0)
        {
            Transform brickPlayer = brickPlayers[index];
            brickPlayers.RemoveAt(index);
      
            Destroy(brickPlayer.gameObject);
        }
     
    }

    public void ClearBrick()
    {
        for (int i = 0; i < brickPlayers.Count; i++)
        {
            Destroy(brickPlayers[i].gameObject);
        }
        brickPlayers.Clear();
    }

}
