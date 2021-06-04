using UnityEngine;

public class Duck : MonoBehaviour
{
    [Header("Hunger tuning")]
    [SerializeField] private float hungerRate;
    [SerializeField] private float starvationHungerLevel;
    [SerializeField] private float satiatedHungerLevel;
    [SerializeField] private float breadHungerAmount;

    [Header("Athleticism")]
    [SerializeField] private float swimSpeed;
    [SerializeField] private float munchRadius;

    private DuckState state;
    private Bread target;

    void Update()
    {
        // Hunger needs to increase over time
        state.Hunger += hungerRate * Time.deltaTime;

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
                Eat(target);
                return;
            }

            transform.LookAt(target.transform, Vector3.up);
            transform.position += Vector3.Normalize(target.transform.position - transform.position) * swimSpeed * Time.deltaTime;
        }
    }

    private void Eat(Bread target)
    {
        state.Hunger -= breadHungerAmount;

        target.Munch();
        target = null;
    }
}

public enum DuckSex
{
    Male,
    Female,
}

public struct DuckState
{
    public DuckSex sex;
    public float Hunger
    {
        get => hunger;
        set
        {
            hunger = value;
            if (hunger < 0) hunger = 0;
        }
    }

    private float hunger;
}
