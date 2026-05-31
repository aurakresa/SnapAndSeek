using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ada buat pindah-pindah level

public class PintuPortal : MonoBehaviour
{
    [Header("Hubungkan ke Player")]
    public KameraFoto scriptKamera; 

    void OnTriggerEnter(Collider other)
    {
        // 1. Cek apakah yang masuk ke pintu itu "Player"
        if (other.CompareTag("Player"))
        {
            // 2. Cek ke otak player, misinya udah kelar belum?
            if (scriptKamera.targetDitemukan >= scriptKamera.totalTarget)
            {
                Debug.Log("Misi Selesai! Teleportasi ke Level 2...");
                // Load level urutan ke-1 di Build Profiles (Level 2)
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("Pintu dikunci! Selesain misinya dulu coy!");
            }
        }
    }
}