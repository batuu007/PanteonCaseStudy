using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrescentTrapControl : MonoBehaviour
{
    public float startingPoint;
    public float endPoint;
    void Start()
    {
        TrapMove();
    }
    void TrapMove()
    {
        transform.DOLocalMoveX(endPoint, 1).OnComplete(() => WayBack()).SetLoops(-1,LoopType.Yoyo);
    }
    void WayBack()
    {
        transform.DOLocalMoveX(startingPoint, 1);
    }
}
