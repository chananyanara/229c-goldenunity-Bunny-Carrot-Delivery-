using UnityEngine;

public class GoalBasket : MonoBehaviour

{

    // ฟังก์ชันนี้จะทำงานเมื่อมีวัตถุ (แครอท) บินเข้ามาชนในเขต Trigger ของตะกร้า

    private void OnTriggerEnter2D(Collider2D collision)

    {

        // 1. เช็คว่าวัตถุที่มาชนมี Tag ว่า "Carrot" หรือไม่ (สำคัญมาก!)

        if (collision.CompareTag("Carrot"))

        {

            Debug.Log("Carrot in the basket!");

            // 2. สั่งทำลายแครอทลูกนั้นทิ้งทันที (จะได้ไม่เด้งไปมาในตะกร้าจนคะแนนเกิน)

            Destroy(collision.gameObject);

            // 3. ส่งสัญญาณไปบอก GameManager ให้เพิ่มคะแนน

            if (GameManager.Instance != null)

            {

                GameManager.Instance.AddScore();

            }

            else

            {

                // ถ้าขึ้น Error ตรงนี้ แสดงว่าลืมสร้าง Object GameManager ในหน้า Hierarchy

                Debug.LogError("หา GameManager ไม่เจอ! อย่าลืมสร้าง Object GameManager ใน Scene นะครับ");

            }

        }

    }

}