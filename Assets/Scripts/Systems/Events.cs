using System;

public static class Events
{

    public static class Level
    {
        public static Action LevelStarted;
        public static Action LevelFinished;
    }

    public static class Timer
    {
        public static Action TimerStarted;
        public static Action TimerPaused;
        public static Action TimerUnpaused;
        public static Action TimerFinished;
        public static Action<float> TimerTick; //time percentage
    }
}
