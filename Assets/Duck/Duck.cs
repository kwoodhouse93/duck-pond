using UnityEngine;

public class Duck : MonoBehaviour
{
    [SerializeField] float swimSpeed;
    [SerializeField] float munchRadius;

    private Bread target;

    void Update()
    {
        if (target == null)
        {
            Bread snacc = FindObjectOfType<Bread>();
            if (snacc != null)
            {
                target = snacc;
            }
        }

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < munchRadius)
            {
                target.Munch();
                target = null;
                return;
            }

            transform.LookAt(target.transform, Vector3.up);
            transform.position += Vector3.Normalize(target.transform.position - transform.position) * swimSpeed * Time.deltaTime;
        }
    }
}
