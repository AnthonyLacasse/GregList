using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EClipType
{
    SOUNDTRACK,
    COMPLETE_TASK,
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

public enum EThemeType
{
    LIVING_ROOM,
    DINING_ROOM,
    KITCHEN,
    DEN,
    BATHROOM,
    STUDY,
    LAVATORY,
    BEDROOM,

    COUNT
}


[System.Serializable]
public struct SoundConfig
{
    public EClipType type;
    public AudioClip clip;
}

[System.Serializable]
public struct ThemeConfig
{
    public EThemeType type;
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
    public static AudioManager Instance => m_Instance;

    #endregion

    [SerializeField] private List<SoundConfig> m_Clips;
    [SerializeField] private List<ThemeConfig> m_Themes;

    private AudioPool m_AudioPool;
    private AudioSource m_Turntable;
    private static Dictionary<EClipType, AudioClip> m_ClipDict;
    private static Dictionary<EThemeType, AudioClip> m_ThemeDict;

    public AudioSource Turntable => m_Turntable;

    

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

        //foreach (ThemeConfig theme in m_Themes)
        //{
        //    m_ThemeDict.Add(theme.type, theme.clip);
        //}

    }

    public void PlaySound(EClipType type)
    {
        AudioSource availableSource = m_AudioPool.GetAvailableAudioSource();
        if (availableSource == null)  { return; }

        availableSource.clip = m_ClipDict[type];

        availableSource.Play();
    }

    //public void PlayTheme(EThemeType theme)
    //{
    //    m_Turntable = m_AudioPool.GetAvailableAudioSource();
    //    if (m_Turntable == null) { return; }

    //    m_Turntable.clip = m_ThemeDict[theme];
    //    m_Turntable.loop = true;
    //    m_Turntable.Play();
    //}

    //public void StopTheme()
    //{
    //    if (m_Turntable != null)
    //    {
    //        m_Turntable.Stop();
    //        m_Turntable = null;
    //    }
    //}

}
