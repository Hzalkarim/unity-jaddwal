using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public RectTransform cellPrefab;
    public List<Cell> cells;

    public int size = 7;

    public void FillBoard(Action<Cell> onClickHandler)
    {
        cells = new List<Cell>();

        for (int y = 0; y < size; ++y)
        {
            for (int x = 0; x < size; ++x)
            {
                var cellRect = Instantiate(cellPrefab, transform);
                cellRect.SetParent(transform);
                var pos = new Vector2(x * 100, y * -100);
                cellRect.anchoredPosition = pos;

                var cell = cellRect.GetComponent<Cell>();
                cellRect.GetComponent<Button>().onClick.AddListener(() => onClickHandler(cell));

                cells.Add(cell);
            }
        }
    }
}
