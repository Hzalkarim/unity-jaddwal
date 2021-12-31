using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsOccupied
    {
        get
        {
            return transform.childCount > 0;
        }
    }
    public Team occupiedTeam;

    public void AddPiece(RectTransform piece)
    {
        piece.transform.SetParent(transform);
        piece.anchoredPosition = Vector2.zero;
    }

    public void RemovePiece()
    {
        var hehe = GetComponentInChildren<Scheduler>();
        Destroy(hehe.gameObject);
    }
}
