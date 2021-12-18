using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalObstacleControl : MonoBehaviour
{
    public float startingPoint;
    public float endPoint;
    void Start()
    {
        TrapMove();
    }
    void TrapMove()
    {
        transform.DOMoveX(endPoint, 2).OnComplete(() => WayBack()).SetLoops(-1, LoopType.Yoyo);
    }
    void WayBack()
    {
        transform.DOMoveX(startingPoint, 2);
    }
}
