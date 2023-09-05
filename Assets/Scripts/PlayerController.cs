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
        BOOSTER,
        DEAD
    }
    public PLAYERTYPE playerType = PLAYERTYPE.DOROTHY;
    public PLAYERSTATE playerState = PLAYERSTATE.IDLE;

    public Vector2 currentPosition;     // 현재 위치
    public float moveSpeed = 10.0f;   // 이동 속도
    public float jumpSpeed = 10.0f;   // 점프 속도
    public float limitY;                    // 최대 점프 높이
     
    public int jumpCount = 0;         // 현재 점프 횟수
    public int jumpMaxCount = 2;   // 점프 가능 횟수

    public bool isJump = false;
    public bool isBooster = false;
    public bool isDead = false;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Jump();
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
                break;
            case PLAYERSTATE.JUMP:
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

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && isJump)
        {
            playerState = PLAYERSTATE.WALK;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && isJump)
        {
            playerState = PLAYERSTATE.WALK;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            playerState = PLAYERSTATE.IDLE;
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerState = PLAYERSTATE.JUMP;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {

        }
        else if (coll.gameObject.CompareTag("Item"))
        {

        }
    }
}
