using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Ÿ��
    public enum PLAYERTYPE
    {
        DOROTHY,         // ���ν�
        SCARECROW,     // ����ƺ�
        WOODCUTTER,  // ��ö ������
        LION               // ����
    }
    public PLAYERTYPE playerType = PLAYERTYPE.DOROTHY;

    public GameObject contactTile;    // ���� Ÿ�� ���ӿ�����Ʈ 
    public Vector3 tilePosition;
    public Vector3 distance;

    public Vector2 currentPosition;
    public float moveSpeed = 10.0f;   // �̵� �ӵ�
    public float jumpSpeed = 15.0f;   // ���� �ӵ�
    public float downSpeed = 22.0f;
    public float limitY = 3.0f;            // ���� ���� ����

    public int jumpCount = 0;         // ���� ���� Ƚ��
    public int jumpMaxCount = 2;   // ���� ���� Ƚ��

    public bool isGround = false;
    public bool isTile = false;
    public bool isDown = true;
    public bool isBooster = false;
    public bool isDead = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpMaxCount)
        {
            currentPosition = transform.position;
            jumpCount++;
            isGround = false;
            isTile = false;
            isDown = false;
        }

        if (!isGround)
            Jump();

        Move();
        PlayerChange();
    }

    public void Move()
    {
        if (contactTile != null && isTile)
        {
            transform.position = contactTile.transform.position - distance;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            isTile = false;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            isTile = false;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (isDown)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }

        if (transform.position.y < limitY + currentPosition.y)
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
        }
        else
        {
            isDown = true;
        }
    }

    //public void StateChange()
    //{
    //    switch (playerState)
    //    {
    //        case PLAYERSTATE.IDLE:
    //            if (contactTile != null && isTile)
    //            {
    //                transform.position = contactTile.transform.position - distance;
    //            }

    //            break;
    //        case PLAYERSTATE.WALK:
    //            isTile = false;
    //            transform.Translate(dir * moveSpeed * Time.deltaTime);

    //            break;
    //        case PLAYERSTATE.JUMP:
    //            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);

    //            if(transform.position.y > limitY + currentPosition.y)
    //            {
    //                playerState = PLAYERSTATE.DOWN;
    //            }

    //            break;
    //         case PLAYERSTATE.DOWN:
    //            transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);

    //            break;
    //        case PLAYERSTATE.BOOSTER: 
    //            break;
    //        case PLAYERSTATE.DEAD: 
    //            break;
    //    }
    //}

    public void PlayerChange()
    {
        switch(playerType)
        {
            case PLAYERTYPE.DOROTHY:
                jumpMaxCount = 2;

                break;
            case PLAYERTYPE.SCARECROW:
                jumpMaxCount = 1;

                break;
            case PLAYERTYPE.WOODCUTTER:
                jumpMaxCount = 1;

                break;
            case PLAYERTYPE.LION:
                jumpMaxCount = 1;

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Tile"))
        {
            if (playerType == PLAYERTYPE.LION)
            {
                playerType = PLAYERTYPE.DOROTHY;
            }
            else
            {
                playerType++;
            }
            gameObject.name = playerType.ToString();

            jumpCount = 0;
            isGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Tile"))
        {
            if (coll.contacts[0].normal.y > 0.7f)
            {
                isTile = true;
                isGround = true;
                jumpCount = 0;

                contactTile = coll.gameObject;
                tilePosition = contactTile.transform.position;
                // ������ ������ ������Ʈ ��ġ�� ĳ���� ��ġ ����
                distance = tilePosition - transform.position;
            }
        }
    }
}
