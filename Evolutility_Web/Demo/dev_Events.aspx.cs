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

public partial class demo_event : BasePage
{
	//protected void Evo1_DBChange(object sender, Evolutility.UIServer.DatabaseEventArgs e)
	//{
	//    Label1.Text = string.Format("DB: {0} #{1}", e.Action.ToString(), e.ID);
	//}
	//protected void Evo1_CredentialChange(object sender, Evolutility.UIServer.CredentialEventArgs e)
	//{
	//    Label1.Text = string.Format("Cred.: {0} #{1} {2}", e.Action, e.UserID, e.UserName);
	//}
	protected void Page_Load(object sender, EventArgs e)
	{ 
	}
	protected void Evo1_DBChange(object sender, Evolutility.UIServer.DatabaseEventArgs e)
	{
		updateLabel(string.Format("{0} #{1}<br/>Database event - {3}<br/><br/>{2}", e.Action.ToString(), e.ID, Label1.Text, System.DateTime.Now.ToLongTimeString()));
	}
	protected void Evo1_CredentialChange(object sender, Evolutility.UIServer.CredentialEventArgs e)
	{
		updateLabel(string.Format("{0} #{1} {2}<br/>Credential event - {4}<br/><br/>{3}", e.Action, e.UserID, e.UserName, Label1.Text, System.DateTime.Now.ToLongTimeString()));
	}
	private void updateLabel(String Text)
	{
		if (Text.Length > 1000)
			Text = Text.Substring(0, 1000);
		Label1.Text = Text;
	}
}
