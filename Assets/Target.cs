using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isDestructible;
    public int numberOfHitsToDestroy; // should be >= 3 if destructible
    private float HP;

    void Start()
    {
        HP = 5 * numberOfHitsToDestroy;
    }

    public void TakeDamage(int dmg){
        if (isDestructible){
            HP -= dmg;

            if (HP > 0){
                // small explosion
            } else {
                // large explosion

                Destroy(this.gameObject);
            }
        }
    }
}
