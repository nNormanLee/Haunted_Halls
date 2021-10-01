using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int whichKey;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {   
        //Button Key
        if(whichKey == 0)
        {
            gameObject.transform.parent.SetActive(True);
        }
        //Voice Key
        else if(whichKey == 1)
        {

        }
        //Other Key
        else if(whichKey == 2)
        {

        }
        //Last Key
        else
        {

        }
    }
}
