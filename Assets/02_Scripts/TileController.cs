using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileController : MonoBehaviour
{
    public enum TILETYPE
    {
        UPANDDOWN,
        RIGHTANDLEFT,
        HEAVYTILE,
        LIGHTTILE
    }
    public TILETYPE tIleType;

    public float currentTime = 0;
    public int moveFlag = 1;  // 타일의 이동 지점을 숫자 표기 ( 시작 지점 1, 도착 지점 2 )
    public float moveSpeed = 0.5f;   // 타일 이동 속도

    void Start()
    {
        StartCoroutine(TileMoveController());
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if(moveFlag == 1)
        {
            if(tIleType == TILETYPE.UPANDDOWN)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
            else if(tIleType == TILETYPE.RIGHTANDLEFT)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (tIleType == TILETYPE.UPANDDOWN)
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            }
            else if (tIleType == TILETYPE.RIGHTANDLEFT)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator TileMoveController()
    {
        moveFlag = (moveFlag == 1) ? 2 : 1;

        yield return new WaitForSeconds(2);
        StartCoroutine(TileMoveController());
    }

    IEnumerator TileDown()
    {
        int loopNum = 0;

        while (currentTime < 5.0f)
        {
            currentTime += Time.deltaTime;
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

            if (loopNum++ > 10000)
                throw new Exception("Infinite Loop");
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerController _player = coll.gameObject.GetComponent<PlayerController>();

        if (_player != null)
        {
            // 가벼운 타일 밟았을 때 처리
            if(tIleType == TILETYPE.LIGHTTILE)
            {
                if (_player.playerType != PlayerController.PLAYERTYPE.SCARECROW)
                {
                    StartCoroutine(TileDown());
                }
            }

            // 무거운 타일 밟았을 때 처리
            if(tIleType == TILETYPE.HEAVYTILE)
            {
                if (_player.playerType == PlayerController.PLAYERTYPE.WOODCUTTER)
                {
                    StartCoroutine(TileDown());
                }
            }
        }
    }
}
