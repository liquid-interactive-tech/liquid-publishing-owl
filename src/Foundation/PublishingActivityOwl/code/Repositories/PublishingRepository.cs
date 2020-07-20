using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;
using Sitecore.Publishing;
using Sitecore.Data.Fields;
using Liquid.Foundation.PublishingActivityOwl.Repositories.Interfaces;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace Liquid.Foundation.PublishingActivityOwl.Repositories
{
    public class PublishingRepository : IPublishingRepository
    {
        public void CreateActivityItem(Item context, Publisher publisher, User user)
        {
            var db = publisher.Options.SourceDatabase;

            Item SaveLocation = db.GetItem(Resources.Constants.PublishedItemsSaveLocation);

            string name = publisher.Options.RecoveryId.ToString();
            var template = db.GetTemplate(new Sitecore.Data.ID(Resources.Constants.PublishActivityItemTemplate));

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                try
                {
                    Item newItem = SaveLocation.Add(name, template);
                    if (newItem != null)
                    {
                        newItem.Editing.BeginEdit();
                        newItem[Resources.Fields.scFieldPublisher] = user.DisplayName;
                        newItem[Resources.Fields.scFieldPublishMode] = publisher.Options.Mode.ToString();
                        newItem[Resources.Fields.scFieldSourceDatabase] = publisher.Options.SourceDatabase.ToString();
                        newItem[Resources.Fields.scFieldTargetDatabase] = publisher.Options.TargetDatabase.ToString();
                        newItem[Resources.Fields.scFieldPublishSubitems] = (publisher.Options.Deep) ? "true" : "false";
                        newItem[Resources.Fields.scFieldPublishRelated] = (publisher.Options.PublishRelatedItems) ? "true" : "false";
                        newItem.Editing.EndEdit();
                    }
                }
                catch(Exception e)
                {
                }
            }
        }

        public void AddToActivityItem(PublishItemContext context)
        {
            var db = context.PublishOptions.SourceDatabase;

            var operation = context.Result.Operation.ToString().ToLower();

            Item SaveLocation = db.GetItem(Resources.Constants.PublishedItemsSaveLocation);

            Item ActivityItem = SaveLocation.Axes.GetDescendants().Where(x => x.Name.Contains(context.PublishContext.PublishOptions.RecoveryId.ToString())).FirstOrDefault();

            Item AcitivtyItemFresh = db.GetItem(ActivityItem.ID);


            Item processedItem = db.GetItem(context.ItemId);

            var path = processedItem?.Paths?.FullPath;
            var id = context?.ItemId?.ToString();

            if (context.Action.ToString().ToLowerInvariant().Contains("delete"))
            {
                path = context.PublishOptions.RootItem.Paths.FullPath + "/" + context.ItemName;
                id = context.ItemId.ToString();
            
            }

            // TO-DO: Use db ref, use Core db.
            var publishedItemsSaveLocation = Sitecore.Configuration.Factory.GetDatabase("master").GetItem(Resources.Constants.PublishedItemsSaveLocation).GetChildren().Where(x => x.TemplateID.ToString() == Resources.Constants.PublishActivityItemTemplate);
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                try
                {
                    if (AcitivtyItemFresh != null)
                    {
                        AcitivtyItemFresh.Editing.BeginEdit();

                        switch (operation)
                        {
                            case "created":
                                TextField items = AcitivtyItemFresh.Fields[Resources.Fields.scFieldCreatedItems];
                                items.Value += path + " - " + id + "<br>";
                                break;

                            case "updated":
                                TextField updateditems = AcitivtyItemFresh.Fields[Resources.Fields.scFieldUpdatedItems];
                                updateditems.Value += path + " - " + id + "<br>";
                                break;

                            case "deleted":
                                TextField deleteditems = AcitivtyItemFresh.Fields[Resources.Fields.scFieldDeletedItems];
                                deleteditems.Value += path + " - " + id + "<br>";
                                break;
                        }

                       

                        AcitivtyItemFresh.Editing.EndEdit();
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        public IEnumerable<PublishActivityItem> GetAllPublishingItems()
        {
            // TO-DO: Do not use master db; also use internal ref.
            Sitecore.Data.Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            Item SaveLocation = masterDb.GetItem(Resources.Constants.PublishedItemsSaveLocation);
            return new List<Item>(masterDb.SelectItems(SaveLocation.Paths.LongID + String.Format("//*[@@templateid='{0}']", Resources.Constants.PublishActivityItemTemplate))).Select(x => new PublishActivityItem()
            {
                Item = x,
                Name = x.ID.ToString(),
                Publisher = x.Fields["Publisher"].Value,
                CreatedItems = x.Fields["Created Items"].Value,
                UpdatedItems = x.Fields["Updated Items"].Value,
                DeletedItems = x.Fields["Deleted Items"].Value,
                PublishMode = x.Fields["Publish Mode"].Value,
                PublishSubitems = x.Fields["Publish Subitems"].Value == "true" ? true : false,
                PublishRelatedItems = x.Fields["Publish Related Items"].Value == "true" ? true : false,
                PublishDate = (DateField)x.Fields["Publish Date"],
                TargetDatabase = x.Fields["Target Database"].Value,
                SourceDatabase = x.Fields["Source Database"].Value
            }).OrderByDescending(s => ((DateField)s.PublishDate).DateTime);
        }
    }
}