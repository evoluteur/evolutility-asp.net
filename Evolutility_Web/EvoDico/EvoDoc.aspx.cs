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

public partial class EvoDicoWiz : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!String.IsNullOrEmpty(Request["md"]) && Request["md"].ToString()=="fld")
		{
			docTitle.InnerHtml = "Design document: fields";
			evo1.XMLfile = "xml/evoDoc_Field.xml";
		}
	}
}
