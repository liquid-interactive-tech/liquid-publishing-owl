using System;
using Sitecore.Controls;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web;
using Sitecore.Web.UI.XamlSharp.Xaml;
using System.Web.UI.WebControls;

namespace Liquid.Foundation.PublishingActivityOwl.Applications
{
    /// <summary>
    /// Xaml control dialog page definition for activity log details.
    /// </summary>
    public class ActivityDetail : DialogPage, IHasCommandContext
    {
        protected TextBox CreatedItems;
        protected TextBox UpdatedItems;
        protected TextBox DeletedItems;

        CommandContext IHasCommandContext.GetCommandContext()
        {
            return new CommandContext();
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, nameof(e));
            Item log = GetItem();
            if (log == null)
                return;

            base.OnLoad(e);

            if (XamlControl.AjaxScriptManager.IsEvent)
                return;

            OK.Visible = false;

            SetText(CreatedItems, log, Resources.Fields.scFieldCreatedItems);

            SetText(UpdatedItems, log, Resources.Fields.scFieldUpdatedItems);

            SetText(DeletedItems, log, Resources.Fields.scFieldDeletedItems);

        }

        private void SetText(TextBox element, Item field, string value)
        {
            if (element == null) return;
            element.Text = field.Fields[value].Value.Replace("<br>", "\n");
            element.Text = String.IsNullOrEmpty(element.Text) ? "0 items altered" : element.Text;
        }

        private static Item GetItem()
        {
            // TO-DO: Use below datbase ref.
            return Sitecore.Configuration.Factory.GetDatabase("master").GetItem(WebUtil.GetQueryString("itemid", ""));
        }

        internal virtual Database ContentDatabase
        {
            get
            {
                return Sitecore.Context.Site.ContentDatabase;
            }
        }

        internal virtual Database ContextDatabase
        {
            get
            {
                return Sitecore.Context.Database;
            }
        }
    }
}
