using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Buckets.Managers;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Liquid.Foundation.PublishingActivityOwl.Commands
{
    /// <summary>
    /// Scheduled task to purge old activity logs.
    /// </summary>
    public class PurgeActivityLog
    {
        #region Execute
        public void Execute(Item[] items, Sitecore.Tasks.CommandItem command, Sitecore.Tasks.ScheduleItem schedule)
        {
            Assert.IsNotNull(command, "command is null");
            Assert.IsNotNull(schedule, "schedule is null");

            Sitecore.Diagnostics.Log.Info("Task: Started Purging Publishing Activity Log data.", this);

            // Get the module settings item.
            Item settings = Sitecore.Configuration.Factory.GetDatabase("master").GetItem(Resources.Constants.SettingsItem);
            if(settings == null)
                Sitecore.Diagnostics.Log.Error("Task: Failed to get settings item.", this); 

            // Get the specified retention policy time.
            ReferenceField retention = settings.Fields[Resources.Fields.scFieldRetentionPeriod];
            int days = (retention == null || String.IsNullOrEmpty(retention?.TargetItem?.Fields["Value"]?.Value)) ? 7 : Int32.Parse(retention?.TargetItem?.Fields["Value"]?.Value);

            // Filter expired items based on retention period.
            List<Item> expiredLogItems = items.Where(x => (Sitecore.DateUtil.IsoDateToDateTime(((DateField)x.Fields[Resources.Fields.scFieldPublishDate]).Value) - DateTime.Now).TotalDays > days).ToList();

            // Delete old items.
            expiredLogItems.ForEach(x => x.Delete());

            // Sync bucket to cleanup empty folders.
            BucketManager.Sync(Sitecore.Configuration.Factory.GetDatabase("master").GetItem(Resources.Constants.PublishedItemsSaveLocation));

            Sitecore.Diagnostics.Log.Info("Task: Finished Purging Publishing Activity Log data.", this);
        }
        #endregion
    }
}