using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarColourswitch : MonoBehaviour {

    [SerializeField]
    private Color Maincolour;
    //[SerializeField]
    //private Color MainColour_Trim;
    //[SerializeField]
    //private Color CupColour;
    //[SerializeField]
    //private Color CupColor_Trim;

    public float[] MainColourArray;

    //public Color mycolor
    //{
    //    get
    //    {
    //        return new Color(_myColor[0], _myColor[1], _myColor[2], _myColor[3]);
    //    }
    //    set
    //    {
    //        _myColor[0] = value.r;
    //        _myColor[1] = value.g;
    //        _myColor[2] = value.b;
    //        _myColor[3] = value.a;

    //    }

    //}

        private Color testcolor;
        // Use this for initialization
        void Start ()
        {
            testcolor = new Color(255, 0, 0);
            Image test = GetComponent<Image>();
            test.color = testcolor;

        }
}
