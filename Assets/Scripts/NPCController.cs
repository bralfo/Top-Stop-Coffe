using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCState
    {
        Entering,
        GoingToTable,
        WaitingFood,
        Eating,
        GoingToCashier,
        Leaving
    }

    [Header("Movimento")]
    public float moveSpeed = 2f;

    [Header("Tempos")]
    public float waitingFoodTime = 3f;
    public float eatingTime = 5f;

    private NPCState currentState;

    private Table currentTable;
    private Vector3 targetPosition;

    private bool actionStarted = false;

    void Start()
    {
        EnterCafe();
    }

    void Update()
    {
        MoveToTarget();

        switch (currentState)
        {
            case NPCState.GoingToTable:

                if (ReachedTarget())
                {
                    currentState = NPCState.WaitingFood;
                    actionStarted = false;
                }

                break;

            case NPCState.WaitingFood:

                if (!actionStarted)
                {
                    actionStarted = true;
                    StartCoroutine(WaitFood());
                }

                break;

            case NPCState.Eating:

                if (!actionStarted)
                {
                    actionStarted = true;
                    StartCoroutine(EatFood());
                }

                break;

            case NPCState.GoingToCashier:

                if (ReachedTarget())
                {
                    GoToExit();
                }

                break;

            case NPCState.Leaving:

                if (ReachedTarget())
                {
                    Destroy(gameObject);
                }

                break;
        }
    }

    void EnterCafe()
    {
        currentTable = CafeManager.Instance.GetFreeTable();

        if (currentTable == null)
        {
            Destroy(gameObject);
            return;
        }

        targetPosition = currentTable.seatPoint.position;

        currentState = NPCState.GoingToTable;
    }

    void MoveToTarget()
    {

        if (currentState == NPCState.GoingToTable ||
            currentState == NPCState.GoingToCashier ||
            currentState == NPCState.Leaving)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    bool ReachedTarget()
    {
        currentTable.occupied = true;
        currentTable.reserved = false;
        return Vector2.Distance(transform.position, targetPosition) < 0.1f;
    }

    System.Collections.IEnumerator WaitFood()
    {
        int orderAmount = Random.Range(1, 3);

        Debug.Log("NPC pediu " + orderAmount + " item(ns)");

        yield return new WaitForSeconds(waitingFoodTime);

        currentState = NPCState.Eating;

        actionStarted = false;
    }

    System.Collections.IEnumerator EatFood()
    {
        Debug.Log("NPC está comendo");

        yield return new WaitForSeconds(eatingTime);

        CafeManager.Instance.FreeTable(currentTable);

        targetPosition = CafeManager.Instance.cashierPoint.position;

        currentState = NPCState.GoingToCashier;

        actionStarted = false;
    }

    void GoToExit()
    {
        currentTable.occupied = false;
        currentTable.reserved = false;

        Debug.Log("NPC pagou");

        targetPosition = CafeManager.Instance.exitPoint.position;

        currentState = NPCState.Leaving;
    }
}