﻿using System;

using MiningMonitor.BackgroundWorker.Scheduler;

namespace MiningMonitor.BackgroundWorker.DataCollector
{
    public class DataSynchronizerSchedule : ISchedule
    {
        public bool DoWorkOnStartup { get; set; }
        public TimeSpan Interval { get; set; }
    }
}