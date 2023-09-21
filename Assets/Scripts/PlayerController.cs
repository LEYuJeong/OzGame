using System.Collections;
using System.Collections.Generic;
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
    // Player ���� 
    public enum PLAYERSTATE
    {
        IDLE = 0,
        WALK,
        JUMP,
        DOWN,
        BOOSTER,
        DEAD
    }
    public PLAYERTYPE playerType = PLAYERTYPE.DOROTHY;
    public PLAYERSTATE playerState = PLAYERSTATE.IDLE;

    public Vector2 currentPosition;    // ���� ��ġ
    public Vector2 dir;                    // ���� �̵� ����

    public float moveSpeed = 10.0f;   // �̵� �ӵ�
    public float jumpSpeed = 15.0f;   // ���� �ӵ�
    public float downSpeed = 18.0f;  // �ϰ� �ӵ�
    public float limitY = 15.0f;          // �ִ� ���� ����

    public int jumpCount = 0;         // ���� ���� Ƚ��
    public int jumpMaxCount = 2;   // ���� ���� Ƚ��

    public bool isJump = false;
    public bool isBooster = false;
    public bool isDead = false;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        // ������ �Է�
        if (Input.GetKey(KeyCode.LeftArrow) && !isJump)
        {
            dir = Vector2.left;
            playerState = PLAYERSTATE.WALK;
        }
        else if(Input.GetKey(KeyCode.RightArrow) && !isJump)
        {
            dir = Vector2.right;
            playerState = PLAYERSTATE.WALK;
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow)  ||  Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerState = PLAYERSTATE.IDLE;
        }

        // ���� �Է�
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpMaxCount)
        {
            currentPosition = transform.position;
            playerState = PLAYERSTATE.JUMP;
            isJump = true;
            jumpCount++;
        }

        StateChange();
        PlayerChange();
    }

    public void StateChange()
    {
        switch (playerState)
        {
            case PLAYERSTATE.IDLE:

                break;
            case PLAYERSTATE.WALK:
                transform.Translate(dir * moveSpeed * Time.deltaTime);

                break;
            case PLAYERSTATE.JUMP:
                transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);

                if(transform.position.y > limitY + currentPosition.y)
                {
                    playerState = PLAYERSTATE.DOWN;
                }

                break;
             case PLAYERSTATE.DOWN:
                transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);

                break;
            case PLAYERSTATE.BOOSTER: 
                break;
            case PLAYERSTATE.DEAD: 
                break;
        }
    }

    public void PlayerChange()
    {
        switch(playerType)
        {
            case PLAYERTYPE.DOROTHY:
                jumpMaxCount = 2;
                gameObject.name = playerType.ToString();

                break;
            case PLAYERTYPE.SCARECROW:
                jumpMaxCount = 1;
                gameObject.name = playerType.ToString();

                break;
            case PLAYERTYPE.WOODCUTTER:
                jumpMaxCount = 1;
                gameObject.name = playerType.ToString();

                break;
            case PLAYERTYPE.LION:
                jumpMaxCount = 1;
                gameObject.name = playerType.ToString();

                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Tile"))
        {
            if(playerState == PLAYERSTATE.DOWN)
            {
                if(playerType == PLAYERTYPE.LION)
                {
                    playerType = PLAYERTYPE.DOROTHY;
                }
                else
                {
                    playerType++;
                }
            }

            playerState = PLAYERSTATE.IDLE;
            isJump = false;
            jumpCount = 0;
        }
    }
}
