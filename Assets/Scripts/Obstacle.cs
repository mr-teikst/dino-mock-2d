using UnityEngine;

public class Obstacle : Interactable
{
    public int damage = 1;

    protected override void Interact(GameObject player)
    {
         Debug.Log("touch");
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
