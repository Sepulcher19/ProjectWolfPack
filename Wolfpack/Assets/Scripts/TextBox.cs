using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBox : MonoBehaviour
{
    public int pageNumb = 0;

    public Text T;

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            pageNumb += 1;
        }
        if (pageNumb == 1)
        {
            T.text = "Alpha: Elder, the Winter has been long and dark. There is a new threat in our woods and we seek your advice...";
        }
        if (pageNumb == 2)
        {
            T.text = "Elder: What is it Young aplha?";
        }
        if (pageNumb == 3)
        {
            T.text = "Alpha: The Betas of the pack bring... troubling news.";
        }
        if (pageNumb == 4)
        {
            T.text = "Beta: Elder... We have seen another pack travel into our land from the east.";
        }
        if (pageNumb == 5)
        {
            T.text = "Elder: So our old enemies show themselves...";
        }
        if (pageNumb == 6)
        {
            T.text = "Beta: We've already lost several Beta scouts to their brutality.";
        }
        if (pageNumb == 7)
        {
            T.text = "Alpha: This is why I come to you Elder... you know this old enemy better than any of us!";
        }
        if (pageNumb == 8)
        {
            T.text = "Elder: Calm yourself Alpha, this enemy only recognises one thing...";
        }
        if (pageNumb == 9)
        {
            T.text = "Alpha: That being?...";
        }
        if (pageNumb == 10)
        {
            T.text = "Elder: Strength...";
        }
        if (pageNumb == 11)
        {
            T.text = "Alpha: War it is then...";
        }
        if (pageNumb == 12)
        {
            T.text = "Alpha: Summon the Beta's, we must push them back before more damage is done.";
        }
        if (pageNumb == 14)
        {
            SceneManager.LoadScene("SampleScene"); 
        }
    }
}
