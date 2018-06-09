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
using Evolutility;

public partial class EvoDicoTest : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string formid;

        formid = Request["formID"] ;
        if (!String.IsNullOrEmpty(formid))
			Evo1.XMLfile = formid;
        else
        {
			Evo1.XMLfile = "xml/evoDico_Form.xml";
			Evo1.ShowTitle = false;
			Evo1.ToolbarPosition = UIServer.EvolToolbarPosition.None; 
        }
    }
}
