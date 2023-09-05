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
        BOOSTER,
        DEAD
    }
    public PLAYERTYPE playerType = PLAYERTYPE.DOROTHY;
    public PLAYERSTATE playerState = PLAYERSTATE.IDLE;

    public Vector2 currentPosition;     // ���� ��ġ
    public float moveSpeed = 10.0f;   // �̵� �ӵ�
    public float jumpSpeed = 10.0f;   // ���� �ӵ�
    public float limitY;                    // �ִ� ���� ����
     
    public int jumpCount = 0;         // ���� ���� Ƚ��
    public int jumpMaxCount = 2;   // ���� ���� Ƚ��

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
