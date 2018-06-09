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

public partial class demo_toolbar : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
	{
		setToolbar();
    }
	protected void RO_CheckedChanged(object sender, EventArgs e)
	{
		if (RO.Checked)
		{
			C.Checked = false;
			U.Checked = false;
			D.Checked = false;
			setToolbar();
		}
	}
	protected void CUD_CheckedChanged(object sender, EventArgs e)
	{
		if (C.Checked || U.Checked || D.Checked)
		{
			RO.Checked = false;
			setToolbar();
		}
	}
	private void setToolbar() { 
		Evo1.DBAllowInsert = C.Checked;
		Evo1.DBAllowSearch = R.Checked;
		Evo1.DBAllowUpdate = U.Checked;
		Evo1.DBAllowDelete = D.Checked;
		Evo1.DBReadOnly = RO.Checked;

		Evo1.DBAllowSelections = SEL.Checked; 
		Evo1.DBAllowExport=XPT.Checked;

		Evo1.DBAllowPrint=PRT.Checked;	
	}
}
