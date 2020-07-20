<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityGrid.aspx.cs" Inherits="Liquid.Foundation.PublishingActivityOwl.Applications.ActivityGrid" %>

<%@ Register Assembly="Sitecore.Kernel" Namespace="Sitecore.Web.UI.HtmlControls" TagPrefix="sc" %>
<%@ Register Assembly="Sitecore.Kernel" Namespace="Sitecore.Web.UI.WebControls" TagPrefix="sc" %>
<%@ Register Assembly="Sitecore.Kernel" Namespace="Sitecore.Web.UI.WebControls.Ribbons" TagPrefix="sc" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ca" %>
<%@ Register Src="~/sitecore/shell/Applications/GlobalHeader.ascx" TagName="GlobalHeader" TagPrefix="uc" %>

<!DOCTYPE html>
<html>
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=edge"> 
  <title><sc:Literal runat="server" Text="Publishing Activity"></sc:Literal></title>
  <link rel="shortcut icon" href="/sitecore/images/favicon.ico" />
  <sc:Stylesheet Src="Content Manager.css" DeviceDependant="true" runat="server" />
  <sc:Stylesheet Src="Grid.css" DeviceDependant="true" runat="server" />
  <sc:Script Src="/sitecore/shell/Controls/SitecoreObjects.js" runat="server" />
  <sc:Script Src="/sitecore/shell/Controls/Lib/jQuery/jquery.noconflict.js" runat="server" />
  <sc:Script Src="/sitecore/shell/Applications/Content Manager/Content Editor.js" runat="server" />
  <style type="text/css">
    html body {
      overflow: hidden;
    }
  </style>
  <script type="text/javascript">
    function PublishActivityGrid_onDoubleClick(sender, eventArgs) {
      scForm.postRequest("", "", "", "publishactivity:viewdetails");
    }
    function OnResize() {

      PublishActivityGrid.render && PublishActivityGrid.render();

      /* re-render again after some "magic amount of time" - without this second re-render grid doesn't pick correct width sometimes */
      setTimeout("PublishActivityGrid.render()", 150);
    }
    function refresh() {
      PublishActivityGrid.scHandler.refresh();
    }
    function OnLoad() {
    }
    window.onresize = OnResize;
    setInterval(function () {
      var searchBox = document.querySelector("[id$=searchBox]");
      if(searchBox && searchBox.value.indexOf('\"') != -1) {
        searchBox.value = searchBox.value.replace(/"/g, "");
      };
    }, 50);

  </script>
</head>
<body style="height: 100%;" id="PageBody" runat="server">
  <form id="PublishActivityGridForm" runat="server">
    <sc:AjaxScriptManager runat="server" />
    <sc:ContinuationManager runat="server" />
    <uc:GlobalHeader runat="server" />
    <div class="scFlexColumnContainer scHeight100">
      <div id="GridCell" class="scFlexContent">
        <div class="scStretchAbsolute scMarginAbsolute" style="overflow: auto">
          <ca:Grid ID="PublishActivityGrid"
            AutoFocusSearchBox="false"
            RunningMode="Callback"
            CssClass="Grid"
            ShowHeader="true"
            HeaderCssClass="GridHeader"
            FillContainer="true"
            FooterCssClass="GridFooter"
            GroupByText=""
            GroupingNotificationText=""
            GroupByCssClass="GroupByCell"
            GroupByTextCssClass="GroupByText"
            GroupBySortAscendingImageUrl="group_asc.gif"
            GroupBySortDescendingImageUrl="group_desc.gif"
            GroupBySortImageWidth="10"
            GroupBySortImageHeight="10"
            GroupingNotificationTextCssClass="GridHeaderText"
            GroupingPageSize="5"
            ManualPaging="true"
            PageSize="15"
            PagerStyle="Slider"
            PagerTextCssClass="GridFooterText"
            PagerButtonHoverEnabled="True"
            PagerImagesFolderUrl="/sitecore/shell/themes/standard/componentart/grid/pager/"
            RenderSearchEngineStamp="True"
            ShowSearchBox="true"
            SearchTextCssClass="GridHeaderText scTextAlignRight "
            SearchBoxCssClass="SearchBox scIgnoreModified"
            SliderHeight="20"
            SliderWidth="150"
            SliderGripWidth="24"
            SliderPopupOffsetX="20"
            SliderPopupClientTemplateId="SliderTemplate"
            TreeLineImagesFolderUrl="/sitecore/shell/themes/standard/componentart/grid/lines/"
            TreeLineImageWidth="22"
            TreeLineImageHeight="19"
            PreExpandOnGroup="false"
            ImagesBaseUrl="/sitecore/shell/themes/standard/componentart/grid/"
            IndentCellWidth="22"
            LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
            LoadingPanelPosition="MiddleCenter"
            Width="100%" Height="100%" runat="server">
            <Levels>
              <ca:GridLevel
                DataKeyField="scGridID"
                ShowTableHeading="false"
                ShowSelectorCells="false"
                RowCssClass="Row"
                ColumnReorderIndicatorImageUrl="reorder.gif"
                DataCellCssClass="DataCell"
                HeadingCellCssClass="HeadingCell"
                HeadingCellHoverCssClass="HeadingCellHover"
                HeadingCellActiveCssClass="HeadingCellActive"
                HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText"
                SelectedRowCssClass="SelectedRow"
                GroupHeadingCssClass="GroupHeading"
                SortAscendingImageUrl="asc.gif"
                SortDescendingImageUrl="desc.gif"
                SortImageWidth="13"
                SortImageHeight="13">
                <Columns>
                  <ca:GridColumn DataField="scGridID" Visible="false" IsSearchable="false" />
                  <ca:GridColumn DataField="Name" Visible="false" IsSearchable="false" />  
				  <ca:GridColumn DataField="Publisher" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Publisher" AllowHtmlContent="False" />
				  <ca:GridColumn DataField="SourceDatabase" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Source Database" AllowHtmlContent="False" />
				  <ca:GridColumn DataField="TargetDatabase" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Target Database" AllowHtmlContent="False" />
				  <ca:GridColumn DataField="PublishMode" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Publish Mode" AllowHtmlContent="False" />
				  <ca:GridColumn DataField="PublishSubitems" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Include Subitems?" AllowHtmlContent="False" />
				  <ca:GridColumn DataField="PublishRelatedItems" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Include Related Items?" AllowHtmlContent="False" />
				  <ca:GridColumn DataField="PublishDate" AllowSorting="true" IsSearchable="true" AllowGrouping="false" SortedDataCellCssClass="SortedDataCell" HeadingText="Publish Date" AllowHtmlContent="False" />
                </Columns>
              </ca:GridLevel>
            </Levels>
            <ClientEvents>
              <ItemDoubleClick EventHandler="PublishActivityGrid_onDoubleClick" />
            </ClientEvents>
            <ClientTemplates>
              <ca:ClientTemplate ID="LoadingFeedbackTemplate">
                <table cellspacing="0" cellpadding="0" border="0">
                  <tr>
                    <td style="font-size: 10px;">
                      <sc:Literal Text="Loading..." runat="server" />
                      ;</td>
                    <td>
                      <img src="/sitecore/shell/themes/standard/componentart/grid/spinner.gif" width="16" height="16" border="0"></td>
                  </tr>
                </table>
              </ca:ClientTemplate>
              <ca:ClientTemplate ID="SliderTemplate">
                <div class="SliderPopup">
                  ## DataItem.PageIndex + 1 ## / ## PublishActivityGrid.PageCount ##
                </div>
              </ca:ClientTemplate>
            </ClientTemplates>
          </ca:Grid>
        </div>
      </div>
    </div>
  </form>
</body>
</html>