using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public SpriteRenderer sr;
    public float decaySpeed = 5f;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Alpha Value" + sr.color.a);
        StartCoroutine(ColorFade());
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    IEnumerator ColorFade()
    {
        color = sr.color;
      
        while (sr.color.a > 0)
        {
            //Debug.Log("Alpha Value" + sr.color.a);
            color.a -= 0.01f * decaySpeed;
            sr.color = color;
            //Debug.Log("Alpha Value" + sr.color.a);

            yield return new WaitForSeconds(0.01f);
        }
        if(sr.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
