using UnityEngine;
public class Carrot : MonoBehaviour
{
    private void Start()
    {
        // ตั้งเวลาให้แครอททำลายตัวเองภายใน 5 วินาที ถ้ามันไม่ชนอะไรเลย (กันขยะล้นแมป)
        Destroy(gameObject, 5f);
    }
    // ฟังก์ชันนี้จะทำงานเมื่อแครอทชนกับวัตถุอื่น
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ถ้าแครอทชน "พื้น" หรือ "กำแพง" ให้มันค่อยๆ หายไป หรือทำลายตัวเอง
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ทำลายแครอททิ้งเมื่อชนพื้น (จะได้ไม่เกะกะ)
           // Destroy(gameObject, 1f);
        }
    }
}