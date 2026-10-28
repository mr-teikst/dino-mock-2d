using UnityEngine;

public class Coin : Interactable
{
    public int value = 10;

    protected override void Interact(GameObject player)
    {
        GameManager.Instance.AddCoin(value);
        Destroy(gameObject);
    }
}
