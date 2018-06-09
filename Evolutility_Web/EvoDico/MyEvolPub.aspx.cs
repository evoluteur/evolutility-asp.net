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

public partial class EvoDico_MyEvolPub : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string formid;

		formid = Request["formID"];
		if (!String.IsNullOrEmpty(formid))
			Evo1.XMLfile = formid;
		else
		{
			Evo1.Visible = false;
			Label1.Text = "<h1>Shared Applications</h1><p>These applications are shared among all users for collaboration.</p>";
		}
	}
}
