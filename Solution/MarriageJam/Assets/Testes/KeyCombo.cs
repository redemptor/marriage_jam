using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Testes
{
    public class KeyCombo
    {
        public string[] buttons; private int currentIndex = 0; //moves along the array as buttons are pressed

        public float allowedTimeBetweenButtons = 0.7f; //tweak as needed
        private float timeLastButtonPressed;

        public KeyCombo(string[] b)
        {
            buttons = b;
        }

        //usage: call this once a frame. when the combo has been completed, it will return true
        public bool Check()
        {
            if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) currentIndex = 0;
            {
                if (currentIndex < buttons.Length)
                {
                    if ((buttons[currentIndex] == "left" && Input.GetKeyDown(KeyCode.Z)) || (buttons[currentIndex] == "right" && Input.GetKeyDown(KeyCode.Z)))
                    {
                        timeLastButtonPressed = Time.time;
                        currentIndex++;
                       
                    }

                    if (currentIndex >= buttons.Length)
                    {
                        currentIndex = 0;

                        return true;
                    }
                    else return false;
                }
            }

            return false;
        }

    }
}
