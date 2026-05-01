using UnityEngine;
public class Carrot : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Basket"))
        {
           Destroy(gameObject, 1f);
        }
    }
}