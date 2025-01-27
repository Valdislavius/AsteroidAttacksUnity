using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberFormater
{ 
    

    public static string FormatNumber(float number)
    {
        if(number >= 1000 && number < 1000000)
        {
            number = number / 1000;
            return number.ToString("0.#") + "k";
        } 
        else if(number >= 1000000)
        {
            number = number / 1000000;
            return number.ToString("0.#") + "kk";
        }
        return number.ToString();
    }
}
