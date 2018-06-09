using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class zmDefault : System.Web.UI.MasterPage
{
    private string _M1;

    public string M1
    {
        get { return _M1; }
        set { _M1 = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
