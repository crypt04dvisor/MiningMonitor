﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using MiningMonitor.Service;

namespace MiningMonitor.Workers.Maintenance
{
    public class MaintenanceWorker : IWorker
    {
        private readonly ISettingsService _settingsService;
        private readonly ISnapshotService _snapshotService;
        private readonly ILogger<MaintenanceWorker> _logger;

        public MaintenanceWorker(ISettingsService settingsService, ISnapshotService snapshotService, ILogger<MaintenanceWorker> logger)
        {
            _settingsService = settingsService;
            _snapshotService = snapshotService;
            _logger = logger;
        }

        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var (_, enablePurgeSetting) = await _settingsService.GetSettingAsync("EnablePurge", cancellationToken);
            var (_, purgeAgeMinutesSetting) = await _settingsService.GetSettingAsync("PurgeAgeMinutes", cancellationToken);

            if (!bool.TryParse(enablePurgeSetting, out var enablePurge) || !int.TryParse(purgeAgeMinutesSetting, out var purgeAgeMinutes) || !enablePurge)
            {
                _logger.LogInformation("Snapshot purge is disabled, skipping.");
                return;
            }

            var purgeCutoff = DateTime.UtcNow - TimeSpan.FromMinutes(purgeAgeMinutes);
            _logger.LogInformation($"Purging snapshot data before {purgeCutoff:MM/dd/yy H:mm}");

            var purgedCount = await _snapshotService.DeleteOldAsync(purgeCutoff, cancellationToken);

            _logger.LogInformation($"Purged {purgedCount} snapshot(s).");
        }
    }
}