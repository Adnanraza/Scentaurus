using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //LoadData();
        //LoadTopPerformer();
    }

    private void LoadTopPerformer()
    {
        //int campusID = Utils.GetCampusIDForAddition();
        //EduMSEntities entity = new EduMSEntities();
        //var obj = entity.proc_GetTopPerformer(campusID).ToList();

        //foreach (var d in obj)
        //{
        //    hdnCat.Value += d.FirstName + " " + d.LastName + ",";
        //    hdnData.Value += d.Actual.ToString() + ",";
        //}


    }

    private void LoadData()
    {
        //int camp = int.Parse((Master.FindControl("ddlCampus") as DropDownList).SelectedValue);
        //EduMSEntities entity = new EduMSEntities();
        //var ledger = entity.Ledgers.OrderByDescending(x => x.ID).ToList();
        //int lastMonth = DateTime.Now.Month - 1;
        //hdnCredit.Value = (ledger.Where(x => x.Type.ToLower().Contains("credit") && x.CampusID == camp && x.CreatedTimeStamp.Value.Month == lastMonth).Sum(x => x.Amount) * -1).ToString();
        //hdnDebit.Value = ledger.Where(x => x.Type.ToLower().Contains("debit") && x.CampusID == camp && x.CreatedTimeStamp.Value.Month == lastMonth).Sum(x => x.Amount).ToString();

        //var cre = entity.proc_GetLedgerByFilter(camp, "Credit").ToList();
        //var deb = entity.proc_GetLedgerByFilter(camp, "Debit").ToList();

        //if (cre.Count > 5)
        //{
        //    cre = cre.Take(5).ToList();
        //}

        //if (deb.Count > 5)
        //{
        //    deb = deb.Take(5).ToList();
        //}

        //string table = @"
        //                   <table id='datatable' style='display:none'>
        //                        <thead>
        //                            <tr>
        //                                <th></th>
        //                                <th>Credit</th>
        //                                <th>Debit</th>
        //                            </tr>
        //                        </thead>
        //                        <tbody>";

        //for (int i = 5; i > 0; i--)
        //{
        //    DateTime date = DateTime.Now.AddMonths(i * -1);
        //    decimal credit = 0;
        //    decimal debit = 0;


        //    if (cre.Where(x => x.Month == date.Month && x.Year == date.Year).ToList().Count > 0)
        //    {
        //        credit = cre.Where(x => x.Month == date.Month && x.Year == date.Year).FirstOrDefault().Amount.GetValueOrDefault();
        //    }

        //    if (deb.Where(x => x.Month == date.Month && x.Year == date.Year).ToList().Count > 0)
        //    {
        //        debit = deb.Where(x => x.Month == date.Month && x.Year == date.Year).FirstOrDefault().Amount.GetValueOrDefault();
        //    }

        //    table += @"<tr>
        //                    <th>" + date.ToString("MMM/yyyy") + @"</th>
        //                    <td>" + credit.ToString() + @"</td>
        //                    <td>" + debit.ToString() + @"</td>
        //                </tr>";
        //}

        //table += "</tbody></table>";
        //l5Month.Text = table;
    }
}