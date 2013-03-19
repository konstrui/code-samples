using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Konstrui.Controls.Ecommerce {

    public class CustomItemsPerPageSelector : ItemsPerPageSelector, IPostBackDataHandler {

        protected override void InitializeControls(GenericContainer container) {

            // TODO: Possibly change the visibility of the different controls dependent on the 'ItemsPerPage' and the 'ItemCount'
            if (this.ItemsPerPage <= 0) {
                this.Visible = false;
                return;
            }

            int selectedItemCount = DetermineSelectedItemCount();
            if (selectedItemCount == NoSelectedItemCount) {
                selectedItemCount = this.ItemsPerPage;
            }
            InitilizeSelectorLink(ItemsTimesOne, 1, selectedItemCount);
            InitilizeSelectorLink(ItemsTimesTwo, 2, selectedItemCount);
            InitilizeSelectorLink(ItemsTimesThree, 3, selectedItemCount);

            ViewAll.NavigateUrl = BuildNavigateUrl(ShowAllItemsItemCountValue);
            if (selectedItemCount == ShowAllItemsItemCountValue) {
                ViewAll.Enabled = false;
            }
        }

        private string BuildNavigateUrl(int itemNavigationCount) {

            string url = this.GetBaseUrl();
            string pageSegment = string.Empty;
            string itemsToDisplaySegment = string.Empty;
            if (this.ItemsPerPage > 0) {
                int currentPage = this.DetermineCurrentPage();
                if (currentPage > 1) {
                    pageSegment = string.Format("{0}/{1}/", this.PagerQueryParamKey, currentPage);
                }
                string countString = itemNavigationCount > 0 ? itemNavigationCount.ToString() : ShowAllQueryString;
                itemsToDisplaySegment = string.Format("{0}/{1}/", this.QueryParamKey, countString);
            }

            var result = this.ResolveUrl(string.Concat(url, "/", pageSegment, itemsToDisplaySegment));
            return string.Concat(result, this.GetQueryString());
        }

        private int DetermineCurrentPage() {
            return this.GetPageNumber(this.GetUrlEvaluationMode(), string.Empty, 1);
        }

        private string GetBaseUrl() {
            if (this.baseUrl == null) {
                var provider = SiteMapBase.GetCurrentProvider();
                if (provider != null) {
                    var currentNode = provider.CurrentNode;
                    if (currentNode != null) {
                        var psn = currentNode as PageSiteNode;
                        if (psn != null) {
                            // Check if the page is a Group page and if yes take its first child page and emit an URL that has embedded the URL of the first child
                            var temp = RouteHelper.GetFirstPageDataNode(psn, true);
                            if (psn.IsGroupPage && temp.Url != currentNode.Url) {
                                baseUrl = temp.Url;
                            } else {
                                baseUrl = currentNode.Url;
                            }
                        } else {
                            baseUrl = currentNode.Url;
                        }

                        baseUrl = RouteHelper.GetAbsoluteUrl(baseUrl);
                        return baseUrl;
                    }
                }
                baseUrl = this.Page.Request.Path;
            }
            return baseUrl;
        }

        private void InitilizeSelectorLink(HyperLink selectorLink, int multiplier, int selectedItemCount) {
            int countTimes = this.ItemsPerPage * multiplier;
            selectorLink.Text = countTimes.ToString();
            selectorLink.NavigateUrl = BuildNavigateUrl(countTimes);
            if (selectedItemCount == countTimes) {
                selectorLink.Enabled = false;
            }
        }

        private int DetermineSelectedItemCount() {
            return this.GetItemsPerPage(this.GetUrlEvaluationMode(), string.Empty, NoSelectedItemCount);
        }

        private string baseUrl;
        private const string ShowAllQueryString = "All";
    }
}
