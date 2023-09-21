using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player 타입
    public enum PLAYERTYPE
    {
        DOROTHY,         // 도로시
        SCARECROW,     // 허수아비
        WOODCUTTER,  // 양철 나무꾼
        LION               // 사자
    }
    // Player 상태 
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

    public Vector2 currentPosition;    // 현재 위치
    public Vector2 dir;                    // 현재 이동 방향

    public float moveSpeed = 10.0f;   // 이동 속도
    public float jumpSpeed = 15.0f;   // 점프 속도
    public float downSpeed = 18.0f;  // 하강 속도
    public float limitY = 15.0f;          // 최대 점프 높이

    public int jumpCount = 0;         // 현재 점프 횟수
    public int jumpMaxCount = 2;   // 점프 가능 횟수

    public bool isJump = false;
    public bool isBooster = false;
    public bool isDead = false;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        // 움직임 입력
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

        // 점프 입력
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
