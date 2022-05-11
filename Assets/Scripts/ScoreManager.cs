using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;



namespace Game.UI
{
    public class ScoreManager : MonoBehaviour
    {

        public DatabaseBridge databaseBridge;
        public Color loseColor;

        TextMeshProUGUI _text;

        float _centsCounter;
        float _time;//time already passed
        int _dolarCounter;
        bool _isOnDangerZone;
        bool _isOnEnd;

        void Start()
        {

            _text = GetComponent<TextMeshProUGUI>();

        }
        void Update()
        {
            if (_isOnEnd)
                return;


            if (_time < 90)
                _time += Time.deltaTime * 3;

            _centsCounter += _isOnDangerZone ? _time / 2 : _time;

            if (_centsCounter >= 99)
            {
                _centsCounter -= 99;
                _dolarCounter++;
            }


            //if cents are below 10, it append a 0 in front
            var _centstring = _centsCounter < 10 ? "0" + (int)_centsCounter : ((int)_centsCounter).ToString();

            _text.text = "$" + _dolarCounter + "," + _centstring;

        }
        public void IsOnEnd()
        {
            _isOnEnd = true;

            if (_text.color == loseColor)//if lose
                databaseBridge.OnPlayed(float.Parse("0", CultureInfo.InvariantCulture.NumberFormat));
            else{
                var _centstring = _centsCounter < 10 ? "0" + (int)_centsCounter : ((int)_centsCounter).ToString();
                float score = float.Parse((_dolarCounter + "." + _centstring), CultureInfo.InvariantCulture.NumberFormat);
                databaseBridge.OnPlayed(score);

            }
        }
        public void IsOnLose()
        {
            _text.text = "0,00";
            _text.color = loseColor;
        }
    }
}
