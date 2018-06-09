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

public partial class EvoDico_MyEvol : BasePage
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
			Label1.Text = "<h1>My Applications</h1><p>These are all my applications (private and public).</p>";
		}
	}
}
