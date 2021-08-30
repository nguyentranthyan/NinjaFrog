using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    [SerializeField] private AudioClip[] m_mainThemes;
    private AudioSource m_audioSource;
    private ObjectPool m_pooler;


    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_pooler = GetComponent<ObjectPool>();
        PlayMusic();
    }

    public void PlayMusic()
	{
		if (m_audioSource == null)
		{
            return;
		}

        int randomTheme = Random.Range(0, m_mainThemes.Length);

        m_audioSource.clip = m_mainThemes[0];
        m_audioSource.Play();
	}

    public void PlaySound(AudioClip clip, float volume = 1f)
	{
        //Get audio GameObject
        GameObject newAudioSources = m_pooler.GetObjectToPool();
        newAudioSources.SetActive(true);

        //Get AudioSource from Object
        AudioSource sources = newAudioSources.GetComponent<AudioSource>();

        //SetUp AudioSource
        sources.clip = clip;
        sources.volume = volume;
        sources.Play();
        StartCoroutine(IEReturnToPool(newAudioSources,clip.length + 0.1f));
	}

    private IEnumerator IEReturnToPool(GameObject objectToReturn, float time)
	{
        yield return new WaitForSeconds(time);
        objectToReturn.SetActive(false);
	}
}
