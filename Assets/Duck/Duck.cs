using UnityEngine;

public class Duck : MonoBehaviour
{
    [Header("Hunger tuning")]
    [SerializeField] private float hungerRate;
    [SerializeField] private float starvationHungerLevel;
    [SerializeField] private float reallyHungryLevel;
    [SerializeField] private float satiatedHungerLevel;
    [SerializeField] private float breadHungerAmount;

    [Header("Athleticism")]
    [SerializeField] private float swimSpeed;
    [SerializeField] private float munchRadius;

    private DuckState state;
    private DecisionState decisionState;
    private Bread target;

    void Update()
    {
        UpdateHunger();
        UpdateDuckDecision();

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

    private void UpdateHunger()
    {
        // Hunger needs to increase over time
        state.Hunger += hungerRate * Time.deltaTime;

        //If duck is too hungry,
        if (state.Hunger > starvationHungerLevel)
            Die();
    }

    private enum DecisionState
    {
        Idle,
        Fleeing,
        FindingBread,
        FindingStick,
        StashingStick,
        FindingMate,
        FindingReeds,
        InReeds,
    }

    private void UpdateDuckDecision()
    {
        // TODO: If goose, flee

        if (decisionState == DecisionState.Idle)
        {
            Debug.Log("Hunger: " + state.Hunger + ", WantsBread: " + WantsBread());
        }
    }

    private void Die()
    {
        // TODO: Animate death
        Object.Destroy(gameObject);
    }

    private void Eat(Bread target)
    {
        state.Hunger -= breadHungerAmount;

        target.Munch();
        target = null;
    }

    private bool WantsBread()
    {
        // If hunger is high, return true (wants bread)
        // If hunger is low, return false (not interested in bread)
        // 
        if (state.Hunger < satiatedHungerLevel) return false; // don't look for bread
        if (state.Hunger > reallyHungryLevel) return true; // only look for bread

        // (Assuming satiated at 10 and really hungry at 90 for purpose of explanation...)
        //
        // If hunger is between 10 and 90, find sticks or bread. The hungrier
        // the duck, the more likely it is to choose bread.
        // 
        // So pick a random value between 10 and 90 and check if that's > or < Hunger
        //
        // 10 < Hunger < 90
        //
        //      choose bread
        //  |---------|-----------X------|
        //  10                          90
        // Not hungry                 Hungry
        // Sticks                      Bread
        float hungerTest = Random.Range(satiatedHungerLevel, reallyHungryLevel);
        if (hungerTest > state.Hunger)
            return false;
        else
            return true;
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
