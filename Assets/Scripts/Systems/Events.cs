﻿using System;

public static class Events
{
    public static class Toys
    {
        public static Action<string, string> Destroy;
    }

    public static class Level
    {
        public static Action Start;
        public static Action Finish;
        public static Action Pause;
        public static Action Unpause;
        public static Action ShowTitle;
    }

    public static class Timer
    {
        public static Action<float> TickOverall;
        public static Action<float> Tick;
    }

    public static class Drain
    {
        public static Action Start;
        public static Action Stop;
        public static Action<float> Tick;
        public static Action Finish;
    }

    public static class Missions
    {
        public static Action<Mission> FulfillMission;
        public static Action<Mission> NewMission;
        public static Action MaxMissionsReached;
        public static Action MaxMissionsCleared;
    }

    public static class Vortex
    {
        public static Action Start;
        public static Action Stop;
    }

    public static class Score
    {
        public static Action<int> Update;
    }
}
