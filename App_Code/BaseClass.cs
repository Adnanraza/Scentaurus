using Context;
using System.Data.Entity;

/// <summary>
/// Summary description for BaseClass
/// </summary>
public partial class BaseClass : System.Web.UI.Page
{
    public ScentaurusEntities2 MyDBContext { get { return new ScentaurusEntities2(); } }
}