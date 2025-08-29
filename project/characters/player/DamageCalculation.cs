using UnityEngine;

public class DamageCalculation : MonoBehaviour
{
    //Attach to a Game Manager to work

    int finalDamageValue;

    public int physicalDamage(int defenderArmor, int attackerDamage)
    {
        finalDamageValue = attackerDamage - defenderArmor;

        if (finalDamageValue < 0)
        {
            finalDamageValue = 1;
        }

        return finalDamageValue;
    }
}
