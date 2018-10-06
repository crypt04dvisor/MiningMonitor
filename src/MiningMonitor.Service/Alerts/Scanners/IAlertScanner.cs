﻿using System;
using System.Collections.Generic;

using MiningMonitor.Model;
using MiningMonitor.Model.Alerts;

namespace MiningMonitor.Service.Alerts.Scanners
{
    public interface IAlertScanner
    {
        bool ShouldScan(AlertDefinition definition);
        (DateTime start, DateTime end) CalculateScanRange(AlertDefinition definition, DateTime scanTime);
        bool EndAlert(AlertDefinition definition, Miner miner, Alert alert, IEnumerable<Snapshot> snapshots, DateTime scanTime);
        Alert PerformScan(AlertDefinition definition, Miner miner, IEnumerable<Snapshot> snapshots, DateTime scanTime);
    }
}
