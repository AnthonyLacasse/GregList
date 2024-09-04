using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EClipType
{
    SOUNDTRACK,
    MENU_SFX,
    INTERACT,
    TRASH,
    WALK,
    DOOR_OPEN,
    DOOR_CLOSE,
    DOOR_SLIDE,
    WATER_PLANTS,
    LIGHTSWITCH,

    COUNT
}

[System.Serializable]
public struct SoundConfig
{
    public EClipType type;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager m_Instance;
    public AudioManager()
    { }

    private void Awake()
    {
        if(m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    public static AudioManager GetInstance()
    {
        return m_Instance;
    }
    #endregion

    [SerializeField] private List<SoundConfig> m_Clips;

    private AudioPool m_AudioPool;
    private static Dictionary<EClipType, AudioClip> m_ClipDict;

    private void Start()
    {        
        m_AudioPool = new AudioPool();
        m_ClipDict = new Dictionary<EClipType, AudioClip>();
        PopulateDictionnary();
    }

    private void PopulateDictionnary()
    {
        foreach (SoundConfig clip in m_Clips)
        {
            m_ClipDict.Add(clip.type, clip.clip);
        }
    }

    public void PlaySound(EClipType type)
    {
        AudioSource availableSource = m_AudioPool.GetAvailableAudioSource();
        if (availableSource == null)  { return; }

        availableSource.clip = m_ClipDict[type];

        availableSource.Play();
    }

    
}
