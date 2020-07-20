using System;
using Sitecore.Security.Accounts;
using Sitecore.Publishing;
using Sitecore.Events;
using Sitecore.Data.Items;
using Liquid.Foundation.PublishingActivityOwl.Repositories.Interfaces;
using Liquid.Foundation.PublishingActivityOwl.Repositories;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace Liquid.Foundation.PublishingActivityOwl.Events
{
    /// <summary>
    /// Publishing events to capture log entries.
    /// </summary>
    public class Publishing
    {
        /// <summary>
        /// Creates the activity log item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPublishBegin(object sender, EventArgs args)
        {
            IPublishingRepository repository = new PublishingRepository();

            SitecoreEventArgs eventArgs = args as SitecoreEventArgs;
            Publisher publisher = Event.ExtractParameter(args, 0) as Publisher;
            User publishingUser = User.FromName(publisher.Options.UserName, true);

            Item baseItem = ((Publisher)(eventArgs.Parameters[0])).Options.RootItem as Item;
            Item contextItem = baseItem.Database.GetItem(baseItem.ID, baseItem.Language, baseItem.Version);

            repository.CreateActivityItem(contextItem, publisher, publishingUser);

        }

        /// <summary>
        /// Adds created, modified, deleted items to activity log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnItemProcessed(object sender, EventArgs args)
        {
            IPublishingRepository repository = new PublishingRepository();

            ItemProcessedEventArgs itemProcessedEventArgs = args as ItemProcessedEventArgs;
            PublishItemContext context = itemProcessedEventArgs != null ? itemProcessedEventArgs.Context : null;

            var operation = context.Result.Operation.ToString().ToLower();

            if(!operation.Contains("skipped"))
            {
                repository.AddToActivityItem(context);
            }

        }
    }
}