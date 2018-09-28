﻿using System;
using System.Collections.Generic;
using MiningMonitor.Model;
using MiningMonitor.Model.Alerts;

namespace MiningMonitor.Service.Alerts
{
    public interface IScan
    {
        AlertDefinition Definition { get; }
        DateTime ScanStart { get; }

        bool EndAlert(Alert alert, IEnumerable<Snapshot> snapshots);
        Alert PerformScan(IEnumerable<Snapshot> snapshots);
    }
}