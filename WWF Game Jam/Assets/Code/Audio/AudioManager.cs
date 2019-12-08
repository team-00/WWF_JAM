using UnityEngine;

using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource musicSource;
    [SerializeField, Range(0f, 2f)] float sfxVolMax;
    [SerializeField, Range(0f, 1f)] float musicVolMax;
    [Space]
    [SerializeField] AudioClip trashExplosion;
    [SerializeField] float trashExplosionVolume = 1f;

    [SerializeField] AudioClip failure;
    [SerializeField] float failureVolume = 1f;

    [SerializeField] AudioClip[] money;
    [SerializeField] float moneyVolume = 1f;

    [SerializeField] AudioClip[] button;
    [SerializeField] float buttonVolume = 1f;

    private static AudioManager instance;
    private static float globalMusicVolume = 50f;
    private static float globalSfxVolume = 50f;

    private void Awake()
    {
        instance = this;
        AdjustGlobalMusicVolume(globalMusicVolume);
        AdjustGlobalSfxVolume(globalSfxVolume);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void AdjustGlobalMusicVolume(float vol)
    {
        globalMusicVolume = vol;
        musicSource.volume = vol / 100f * musicVolMax;
    }

    public void AdjustGlobalSfxVolume(float vol)
    {
        globalSfxVolume = vol;
        source.volume = vol / 100f * sfxVolMax;
    }

    private static AudioClip GetSingle(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public static void PlayTrashbagExplosionSound()
    {
        instance.source.PlayOneShot(instance.trashExplosion, instance.trashExplosionVolume);
    }

    public static void PlayFailureSound()
    {
        instance.source.PlayOneShot(instance.failure, instance.failureVolume);
    }

    public static void PlayMoneySound()
    {
        instance.source.PlayOneShot(GetSingle(instance.money), instance.moneyVolume);
    }

    public static void PlayButtonSound()
    {
        instance.source.PlayOneShot(GetSingle(instance.button), instance.buttonVolume);
    }
}
