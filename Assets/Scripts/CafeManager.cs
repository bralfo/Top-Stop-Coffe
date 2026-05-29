using System.Collections.Generic;
using UnityEngine;

public class CafeManager : MonoBehaviour
{
    public static CafeManager Instance;

    [Header("Todas as mesas")]
    public List<Table> tables = new List<Table>();

    [Header("Pontos importantes")]
    public Transform cashierPoint;
    public Transform exitPoint;

    private void Awake()
    {
        Instance = this;
    }

    public Table GetFreeTable()
    {
        foreach (Table table in tables)
        {
            if (table.IsAvailable())
            {
                table.reserved = true;
                return table;
            }
        }

        return null;
    }


    public void FreeTable(Table table)
    {
        table.occupied = false;
    }
}