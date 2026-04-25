using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource; // ลาก AudioSource มาใส่ช่องนี้ใน Inspector

    [System.Serializable]
    public struct SoundEffect
    {
        public string name;      // ชื่อเสียง (เช่น "Click", "Build", "ZombieHit")
        public AudioClip clip;   // ไฟล์เสียง
    }

    public List<SoundEffect> soundLibrary; // นี่คือ List ที่จะปรากฏใน Inspector

    void Awake()
    {
        instance = this;
    }

    public void PlaySound(string soundName)
    {
        // ค้นหาเสียงจากชื่อใน List
        SoundEffect sound = soundLibrary.Find(s => s.name == soundName);

        if (sound.clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(sound.clip);
        }
        else
        {
            Debug.LogWarning("ไม่พบเสียงชื่อ: " + soundName + " หรือลืมใส่ AudioSource ใน SoundManager!");
        }
    }
}