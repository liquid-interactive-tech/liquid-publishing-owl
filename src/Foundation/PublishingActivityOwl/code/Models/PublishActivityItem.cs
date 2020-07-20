using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Liquid.Foundation.PublishingActivityOwl.Repositories
{
    #region PublishActivityItem
    public class PublishActivityItem
    {
        public Item Item { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string CreatedItems { get; set; }
        public string UpdatedItems { get; set; }
        public string DeletedItems { get; set; }
        public string PublishMode { get; set; }
        public bool PublishSubitems { get; set; }
        public bool PublishRelatedItems { get; set; }
        public DateField PublishDate { get; set; }
        public string TargetDatabase { get; set; }
        public string SourceDatabase { get; set; }
    }
    #endregion
}