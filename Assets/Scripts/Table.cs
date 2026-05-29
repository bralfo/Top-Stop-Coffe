using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    public bool occupied;
    public bool reserved;

    public Transform seatPoint;
    private IEnumerable<Table> tables;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsAvailable()
    {
        return !occupied && !reserved;
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
}
