using System;
using System.Collections;
using UnityEngine;

public class PlantMaturationProcessTimer
{
    private const int MULTIPLIER_SIXTY = 60; 
    private const int MULTIPLIER_HOURS = 3600; 
        
    private float _countDownInterval;
        
    public event Action<string> TextTimerUpdate;
    public event Action TimerEnded;
        
    public PlantMaturationProcessTimer(float countDownInterval) => 
        _countDownInterval = countDownInterval;

    public IEnumerator StartTimerCoroutine()
    {
        while (_countDownInterval > 0)
        {
            _countDownInterval -= 1f;
                
            int intTime = (int) _countDownInterval;
            int hoursTotal = intTime / MULTIPLIER_HOURS;
            int minutesTotal = intTime / MULTIPLIER_SIXTY;
            int minutesFormatted = minutesTotal % MULTIPLIER_SIXTY;
            int secondsFormatted = intTime % MULTIPLIER_SIXTY;
                
            string str = $"{hoursTotal:00} : {minutesFormatted:00} : {secondsFormatted:00}";
            TextTimerUpdate?.Invoke(str);
            yield return new WaitForSecondsRealtime(1f);
        }
            
        TimeUpActions();
    }
        
    private void TimeUpActions()
    {
        _countDownInterval = 0;
        TextTimerUpdate?.Invoke(FormatTime(_countDownInterval));
        TimerEnded?.Invoke();
    }
        
    private string FormatTime(double time)
    {
        string timeText = "";

        int intTime = (int) time;
        int hoursTotal = intTime / MULTIPLIER_HOURS;
        int minutesTotal = intTime / MULTIPLIER_SIXTY;
        int minutesFormatted = minutesTotal % MULTIPLIER_SIXTY;
        int secondsFormatted = intTime % MULTIPLIER_SIXTY;

        timeText = $"{hoursTotal:00} : {minutesFormatted:00} : {secondsFormatted:00}";
        return timeText;
    }
}