using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour
{
    public GameObject credits;
    public float speed;
    public float currentTime;
    public float duration;
    public GameObject fade;
    float start = 0;
    public Image fadetoblack;

    private void Start()
    {
        fadetoblack = fade.GetComponent<Image>();
        currentTime = Time.time;    
    }
    // Update is called once per frame
    void Update()
    {
        credits.transform.Translate(Vector3.up * (speed * Time.deltaTime));

        if (currentTime + duration < Time.time)
        {
            fade.SetActive(true);
            float alpha = start += 0.33f * Time.deltaTime;
            fadetoblack.color = new Color(0, 0, 0, alpha);
            if (alpha >= 1.3)
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }


}
