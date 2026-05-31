using UnityEngine;

public class KucingJalan : MonoBehaviour
{
    public float kecepatan = 1.5f;
    public float waktuGantiArah = 3f;
    private float timer;

    void Start()
    {
        // Setel waktu awal biar langsung milih arah
        timer = waktuGantiArah;
    }

    void Update()
    {
        // Kurangin waktu setiap detik
        timer -= Time.deltaTime;

        // Kalau waktunya habis (0), kucing milih arah baru secara acak
        if (timer <= 0)
        {
            float arahAcak = Random.Range(0f, 360f);
            // Puter badan kucing ke arah acak tadi (Sumbu Y)
            transform.rotation = Quaternion.Euler(0, arahAcak, 0);
            
            // Ulangi timernya dari awal
            timer = waktuGantiArah; 
        }

        // Kucingnya disuruh maju lurus terus ke arah depannya
        transform.Translate(Vector3.forward * kecepatan * Time.deltaTime);
    }
}