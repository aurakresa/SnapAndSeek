using UnityEngine;
using TMPro; 
using UnityEngine.UI; 
using System.Collections;

public class KameraFoto : MonoBehaviour
{
    [Header("Pengaturan Kamera")]
    public Camera kameraUtama;
    public float jarakFoto = 15f; 
    public int targetDitemukan = 0;
    public int totalTarget = 2; // Total target di ruangan ini

    [Header("UI & Efek")]
    public TextMeshProUGUI teksMisi;
    public Image efekFlash;
    public AudioSource suaraJepret;

    [Header("Sistem Portal & Level 2")]
    public GameObject pintuPortal;
    
    // --- FITUR BARU BUAT LEVEL 2 ---
    public GameObject targetRahasia; // Masukin setan ke sini
    public int skorBuatMunculin = 1; // Muncul setelah target ke-berapa?

    void Start()
    {
        // Matiin portal pas awal mulai
        if(pintuPortal != null) pintuPortal.SetActive(false);
        
        // Sembunyiin setan pas game baru mulai!
        if(targetRahasia != null) targetRahasia.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            JepretFoto();
        }
    }

    void JepretFoto()
    {
        if(suaraJepret != null) suaraJepret.Play();
        StartCoroutine(FlashKamera());

        RaycastHit hit;
        if (Physics.Raycast(kameraUtama.transform.position, kameraUtama.transform.forward, out hit, jarakFoto))
        {
            Debug.Log("Laser nabrak: " + hit.collider.name);
            if (hit.collider.CompareTag("TargetFoto"))
            {
                targetDitemukan++;
                hit.collider.tag = "Untagged"; // Kunci biar gak difoto 2x
                
                // Kedipin objek
                hit.collider.gameObject.SetActive(false);
                hit.collider.gameObject.SetActive(true);

                // Update UI Standar
                teksMisi.text = "Target: " + targetDitemukan + " / " + totalTarget;

                // --- LOGIKA SETAN MUNCUL ---
                // Kalau ada objek rahasia dan skornya udah pas
                if(targetRahasia != null && targetDitemukan == skorBuatMunculin)
                {
                    targetRahasia.SetActive(true); // Bangunin Setannya!
                    teksMisi.text = "Anomali 1 Selesai!\nAWAS... ADA YANG BERGERAK! FOTO DIA!";
                }

                // Cek Tamat
                if(targetDitemukan >= totalTarget)
                {
                    teksMisi.text = "SELAMAT! SEMUA ANOMALI DITEMUKAN!";
                    if(pintuPortal != null) pintuPortal.SetActive(true);
                }
            }
        }

    }

    IEnumerator FlashKamera()
    {
        Color c = efekFlash.color;
        c.a = 1f;
        efekFlash.color = c;
        while (efekFlash.color.a > 0)
        {
            c.a -= Time.deltaTime * 2f; 
            efekFlash.color = c;
            yield return null;
        }
    }
}