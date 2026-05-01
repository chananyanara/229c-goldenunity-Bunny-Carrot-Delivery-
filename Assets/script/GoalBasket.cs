using UnityEngine;

public class GoalBasket : MonoBehaviour

{
    

    private void OnTriggerEnter2D(Collider2D collision)

    {
        

        if (collision.CompareTag("Carrot"))

        {

            Debug.Log("Carrot in the basket!");
            

            Destroy(collision.gameObject);
            

            if (GameManager.Instance != null)

            {

                GameManager.Instance.AddScore();

            }

            else

            {
                

                Debug.LogError("หา GameManager ไม่เจอ! อย่าลืมสร้าง Object GameManager ใน Scene นะครับ");

            }

        }

    }

}