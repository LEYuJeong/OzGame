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

    public Vector2 currentPosition;     // ���� ��ġ
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentPosition = transform.position;
            playerState = PLAYERSTATE.JUMP;
        }

        StateChange();
    }

    public void StateChange()
    {
        switch (playerState)
        {
            case PLAYERSTATE.IDLE:
                transform.position = currentPosition;

                break;
            case PLAYERSTATE.WALK: 
                break;
            case PLAYERSTATE.JUMP:
                transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);

                if(transform.position.y > limitY)
                {
                    playerState = PLAYERSTATE.DOWN;
                }

                break;
             case PLAYERSTATE.DOWN:
                transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);

                if(transform.position.y < currentPosition.y)
                {
                    playerState = PLAYERSTATE.IDLE;
                }

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
}
