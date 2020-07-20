using Sitecore.Data.Items;
using Sitecore.Publishing;
using Sitecore.Publishing.Pipelines.PublishItem;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liquid.Foundation.PublishingActivityOwl.Repositories.Interfaces
{
    public interface IPublishingRepository
    {
        void CreateActivityItem(Item context, Publisher publisher, User user);
        void AddToActivityItem(PublishItemContext context);
        IEnumerable<PublishActivityItem> GetAllPublishingItems();
    }
}