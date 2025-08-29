using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    //Enemy Drop when it dies

    [Header("Drop Settings")]
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropPos;

    public void dropItem()
    {
        if (dropPrefab != null && dropPos != null)
        {
            Instantiate(dropPrefab, dropPos.position, Quaternion.identity);
        }
    }
}
