using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utils
/// </summary>
public static class Utils
{

    public enum LedgerType
    {
        Payment, Fees
    }

    public static class AdjustmentType
    {
        public static string MoveAmountToParentAccount = "Move Amount To Parent Account";
        public static string GetAmountToChildAccount = "Recieved Amount From Child Account";
        public static string LateFee = "Late Fee";
        public static string AdmissionFee = "Admission Fee";
        public static string DebitAdjustment = "Debit Adjustment";
        public static string CreditAdjustment = "Credit Adjustment";
    }

    public static class InstAdjustmentType
    {
        public static string StudentLedgerDebit = "Student Ledger - Debit";
        public static string StudentLedgerCredit = "Student Ledger - Credit";
        public static string TeacherSalary = "Teacher's Salary - Credit";
        public static string Maintainance = "Maintainance - Credit";
        public static string Rent = "Rent - Credit";
        public static string OtherDebit = "Maintainance - Debit";
        public static string OtherCredit = "Maintainance - Credit";
    }

    public static string[] Adjustments = new string[] { AdjustmentType.DebitAdjustment, AdjustmentType.CreditAdjustment };
    public static string[] InstAdjustments = new string[] {
         InstAdjustmentType.Maintainance
        , InstAdjustmentType.Rent
        , InstAdjustmentType.OtherDebit
        , InstAdjustmentType.OtherCredit
    };

    public static UserDetail GetUserData()
    {
        var user = new UserDetail();

        if (HttpContext.Current.Session["UserData"] != null)
        {
            user = (UserDetail)HttpContext.Current.Session["UserData"];
        }

        return user;
    }

    public static void SetUserData(UserDetail user)
    {
        HttpContext.Current.Session["UserData"] = user;
    }

    public static void CleanUserData()
    {
        HttpContext.Current.Session["UserData"] = null;
    }

    public static string GetUserName()
    {
        return GetUserData().Username;
    }

    //public static int? GetCampusID()
    //{
    //    var user = GetUserData();

    //    if (user.Role == "Admin")
    //    {
    //        return (int?)null;
    //    }

    //    return user.CampusID;
    //}

    //public static int GetCampusIDForAddition()
    //{
    //    return HttpContext.Current.Session["CampusID"] != null ? int.Parse(HttpContext.Current.Session["CampusID"].ToString()) : 0;
    //}



    public static void AddEntryInCustomerLedger(int userID, decimal amount, int? refernceID, string adType, string desc)
    {
        decimal balance = 0;
        var entity = new ScentaurusEntities2();
        var data = entity.Ledgers.Where(x => x.UserID == userID).OrderByDescending(x => x.ID).ToList();

        if (data.Count > 0)
        {
            balance = data.FirstOrDefault().Balance;
        }

        var led = new Ledger();
        led.UserID = userID;
        led.ReferenceID = refernceID;
        led.AdjustmentType = adType;
        led.Amount = amount;
        led.Type = amount < 0 ? "Credit" : "Debit";
        led.CreatedBy = GetUserName();
        led.Balance = balance + amount;

        led.CreatedTimeStamp = DateTime.Now;
        entity.Ledgers.Add(led);
        entity.SaveChanges();

        int stdLedgerID = entity.Ledgers.OrderByDescending(x => x.ID).FirstOrDefault().ID;

        //if (amount < 0)
        //{
        //    AddEntryInLedger(amount * -1, stdLedgerID, InstAdjustmentType.StudentLedgerDebit);
        //}
    }

    public static void AddEntryInLedger(decimal amount, int? refernceID, string adType)
    {
        var entity = new ScentaurusEntities2();
        var led = new Ledger();

        led.ReferenceID = refernceID;
        led.AdjustmentType = adType;
        led.Amount = amount;
        led.Type = amount < 0 ? "Credit" : "Debit";

        led.CreatedBy = GetUserName();

        led.CreatedTimeStamp = DateTime.Now;
        entity.Ledgers.Add(led);
        entity.SaveChanges();
    }

    /// <summary>
    ///  This method will Add 'Select' Item At Begining in drop down list. 
    /// </summary>
    public static DropDownList AddSelectItemAtBegining(DropDownList ddl)
    {
        ddl.Items.Insert(0, new ListItem("-----Select-----", "0"));
        return ddl;
    }

    public static DropDownList AddSelectProductItemAtBegining(DropDownList ddl)
    {
        ddl.Items.Insert(0, new ListItem("-----Select Product-----", "0"));
        return ddl;
    }

    public static void ShowAlert(System.Web.UI.Control control, string message)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(control, control.GetType(), "Alert", String.Concat("swal('", message.Replace("'", "~"), "');"), true);
    }

    public static void ShowAlert(System.Web.UI.Control control, string heading, string message)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(control, control.GetType(), "Alert", String.Concat("swal('", heading.Replace("'", "~"), "' , '", message.Replace("'", "~"), "');"), true);
    }

    public static void ShowAlert(System.Web.UI.Control control, string heading, string message, bool success)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(control, control.GetType(), "Alert", String.Concat("swal('", heading.Replace("'", "~"), "' , '", message.Replace("'", "~"), "', '", success ? "success" : "error", "');"), true);
    }
}