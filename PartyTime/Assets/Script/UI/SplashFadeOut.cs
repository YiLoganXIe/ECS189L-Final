using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashFadeOut : MonoBehaviour
{
    [SerializeField] float m_Time = 3;
    private bool anim_finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(FadeTo());
    }

    // Update is called once per frame
    void Update()
    {
        if (anim_finished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator FadeTo()
    {
        yield return new WaitForSeconds(2);
        var m_image = this.gameObject.GetComponent<Image>();
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / m_Time)
        {
            Color newColor = new Color(Mathf.Lerp(1, 0, t), Mathf.Lerp(1, 0, t), Mathf.Lerp(1, 0, t));
            m_image.color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        anim_finished = true;
    }
}
