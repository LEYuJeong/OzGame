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

    public Vector2 currentPosition;     // 현재 위치
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
