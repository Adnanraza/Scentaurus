using Context;
using System;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Dashboard : BaseClass
{
    public class CL
    {
        public string Name { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ltrTree.Text = GiveChildrenByUserID(Utils.GetUserData().ID);
    }

    public string GiveChildrenByUserID(int userID)
    {
        var context = MyDBContext;
        var referred = context.UserDetails.Where(x => x.ReferrerID == userID).ToList();
        var comm = context.SalesComissionNthLevels.Where(x => x.BeneficierUserID == userID).Sum(x => x.ComissionAmount);
        var users = context.UserSales.Where(x => x.UserID == userID).OrderBy(x => x.ID).Distinct().ToList();
        var html = "<div class='container'><ul>{0}</ul></div>";
        var LIs = String.Empty;

        foreach (var user in referred)
        {
            var sale = context.UserProducts.Where(x => x.UserID == user.ID).Sum(x => x.Quantity * (x.Product.FinalSalePrice.HasValue ? x.Product.FinalSalePrice : 0));
            LIs += "<li data-id='" + user.ID + "'><a href='#'>" + user.FirstName + " " + user.LastName + " (" + (sale == null ? 0 : sale).ToString() 
                +(!user.IsActive ? " - <b>Not Active</b> " : String.Empty) +
                " )</a></li>";
        }

        return String.Format(html, LIs);
    }



    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public static string getChildren(string Id)
    {
        var dash = new Dashboard();
        return dash.GiveChildrenByUserID(int.Parse(Id));
    }
}