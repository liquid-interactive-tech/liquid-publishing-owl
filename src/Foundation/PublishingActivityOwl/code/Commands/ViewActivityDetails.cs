using System;
using System.Collections.Specialized;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Continuations;

namespace Liquid.Foundation.PublishingActivityOwl.Commands
{
    [Serializable]
    public class ViewActivityDetails : Command, ISupportsContinuation
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, nameof(context));
            string parameter = context.Parameters["itemid"];

            ContinuationManager.Current.Start((ISupportsContinuation)this, "Run", new ClientPipelineArgs(new NameValueCollection()
            {
                ["itemid"] = parameter
            }));
           
        }

        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, nameof(context));
            return base.QueryState(context);
        }

        protected static void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));

            if (!args.IsPostBack)
            {
                SheerResponse.ShowModalDialog(new ModalDialogOptions(new UrlString("/sitecore/shell/-/xaml/Liquid.Foundation.PublishingActivityOwl.Commands.ViewActivityDetails.aspx")
                {
                    ["itemid"] = args.Parameters["itemid"]
                }.ToString())
                {
                    Width = "950",
                    Height = "700",
                    Response = false,
                    Closable = true
                });
                args.WaitForPostBack();
            }
        }
    }
}