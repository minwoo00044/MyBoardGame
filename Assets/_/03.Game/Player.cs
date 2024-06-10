using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2f; // �̵� �ӵ�
    private Vector3 targetPosition;
    private TileTester tileTester;

    private void Start()
    {
        tileTester = FindObjectOfType<TileTester>();
    }

    public void StartMovement(Vector3 target)
    {
        targetPosition = target;
        List<Vector3> path = tileTester.GetPathToPosition(targetPosition);
        if (path != null)
        {
            StopAllCoroutines(); // ���� �̵� �ڷ�ƾ ����
            StartCoroutine(MoveAlongPath(path));
        }
    }

    private IEnumerator MoveAlongPath(List<Vector3> path)
    {
        foreach (Vector3 step in path)
        {
            while (Vector3.Distance(transform.position, step) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, step, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        transform.position = targetPosition; // ���� ��ġ�� ����
        tileTester.CalculateReachableTiles(transform.position, 6);
    }
}
