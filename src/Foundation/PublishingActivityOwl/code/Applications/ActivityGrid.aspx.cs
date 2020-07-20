using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Liquid.Foundation.PublishingActivityOwl.Repositories;
using Liquid.Foundation.PublishingActivityOwl.Repositories.Interfaces;
using ComponentArt.Web.UI;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Extensions;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Shell.Web;
using Sitecore.Web.UI.Grids;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Ajax;

namespace Liquid.Foundation.PublishingActivityOwl.Applications
{
    public partial class ActivityGrid : Page, IHasCommandContext
    {
        protected HtmlGenericControl PageBody;

        protected Grid PublishActivityGrid;
      
        protected ClientTemplate LoadingFeedbackTemplate;

        protected ClientTemplate SliderTemplate;

        protected IPublishingRepository repository = new PublishingRepository();

        private bool RebindRequired
        {
            get
            {
                return !this.Page.IsPostBack && this.Request.QueryString["Cart_PublishActivityGrid_Callback"] != "yes" || this.Page.Request.Params["requireRebind"] == "true";
            }
        }

        CommandContext IHasCommandContext.GetCommandContext()
        {
            CommandContext commandContext = new CommandContext();

            string selectedValue = GridUtil.GetSelectedValue("PublishActivityGrid");

            commandContext.Parameters["itemid"] = selectedValue;
            return commandContext;
        }

        protected override void OnInit(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, nameof(e));
            base.OnInit(e);

            this.PublishActivityGrid.ItemDataBound += new Grid.ItemDataBoundEventHandler(this.PublishActivityGrid_ItemDataBound);
            this.PublishActivityGrid.ItemContentCreated += new Grid.ItemContentCreatedEventHandler(this.PublishActivityGridItemContentCreated);
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, nameof(e));
            ShellPage.IsLoggedIn(true);
            base.OnLoad(e);
            ComponentArtGridHandler<PublishActivityItem>.Manage(this.PublishActivityGrid, (IGridSource<PublishActivityItem>)new GridSource<PublishActivityItem>(repository.GetAllPublishingItems()), this.RebindRequired);
            this.PublishActivityGrid.LocalizeGrid();
            this.WriteLanguageAndBrowserCssClass();
        }

        private static void Current_OnExecute(object sender, AjaxCommandEventArgs args)
        {
            Assert.ArgumentNotNull(sender, nameof(sender));
            Assert.ArgumentNotNull((object)args, nameof(args));
            if (args.Name == "usermanager:userdeleted")
            {
                SheerResponse.Eval("refresh()");
            }
            else
            {
                if (!(args.Name == "usermanager:refresh"))
                    return;
                SheerResponse.Eval("refresh()");
            }
        }

        private void WriteLanguageAndBrowserCssClass()
        {
            this.Form.Attributes["class"] = string.Format("{0} {1}", (object)UIUtil.GetBrowserClassString(), (object)UIUtil.GetLanguageCssClassString());
        }

        private void PublishActivityGridItemContentCreated(object sender, GridItemContentCreatedEventArgs e)
        {
            Assert.ArgumentNotNull(sender, nameof(sender));
            Assert.ArgumentNotNull((object)e, nameof(e));
        }

        private void PublishActivityGrid_ItemDataBound(object sender, GridItemDataBoundEventArgs e)
        {
            Item dataItem = e.DataItem as Item;
        }
    }
}