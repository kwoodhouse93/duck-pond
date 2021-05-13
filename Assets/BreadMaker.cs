using UnityEngine;

public class BreadMaker : MonoBehaviour
{
    [SerializeField] private GameObject bread;
    [SerializeField] private float breadRange;

    void Update()
    {
        Bread b = FindObjectOfType<Bread>();
        if (b == null)
        {
            Object.Instantiate(bread, NewBreadPos(), NewBreadRot());
        }
    }

    private Vector3 NewBreadPos()
    {
        return new Vector3(
            Random.Range(-breadRange, breadRange),
            0,
            Random.Range(-breadRange, breadRange)
        );
    }

    private Quaternion NewBreadRot()
    {
        return Quaternion.identity;
    }
}
