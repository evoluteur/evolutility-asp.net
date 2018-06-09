using System;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class zEULA : System.Web.UI.UserControl
{
	//public string PathToPictures { get; set; }
	//public string PathToPage { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
		
//<a href="dev_Localization.aspx?LNG=EN"><img alt="English" class="icon" src="../pixevo/flags/en.gif" />English</a>
//<a href="dev_Localization.aspx?LNG=ZH"><img alt="Chinese" class="icon" src="../pixevo/flags/zh.gif" />Chinese</a> 
//<a href="dev_Localization.aspx?LNG=FR"><img alt="French" class="icon" src="../pixevo/flags/fr.gif" />French</a> 
//<a href="dev_Localization.aspx?LNG=DE"><img alt="German" class="icon" src="../pixevo/flags/de.gif" />German</a>
//<a href="dev_Localization.aspx?LNG=HI"><img alt="Hindi" class="icon" src="../pixevo/flags/hi.gif" />Hindi</a>
//<a href="dev_Localization.aspx?LNG=JP"><img alt="Japanese" class="icon" src="../pixevo/flags/jp.gif"/>Japanese</a>
//<a href="dev_Localization.aspx?LNG=ES"><img alt="Spanish" class="icon" src="../pixevo/flags/es.gif" />Spanish</a>
//<a href="dev_Localization.aspx?LNG=PT"><img alt="Portuguese" class="icon" src="../pixevo/flags/pt.gif" />Portuguese</a>
//<a href="dev_Localization.aspx?LNG=IT"><img alt="Italian" class="icon" src="../pixevo/flags/it.gif" />Italian</a> 
//<a href="dev_Localization.aspx?LNG=DA"><img alt="Danish" class="icon" src="../pixevo/flags/da.gif" />Danish</a>
//<a href="dev_Localization.aspx?LNG=RO"><img alt="Romanian" class="icon" src="../pixevo/flags/ro.gif" />Romanian</a>
//<a href="dev_Localization.aspx?LNG=CA"><img alt="Catalan" src="../pixevo/flags/ca.gif" class="icon"/>Catalan</a> 
		spanFlags.InnerHtml = "";
    }

	internal String HTMLFlags( )
	{
		StringBuilder sb=new StringBuilder(); 
		String lang = "EN,English,ZH,Chinese"; 
//

		char[] sepChars = {','};
		string[] ln = lang.Split(sepChars);
		
		//for (int i=0;i<lang.Length;i+2){
		//    sb.Append("<a href=\"{0}?LNG={2}\"><img alt=\"{3}\" class=\"icon\" src=\"{1}{2}.gif\" />{3}</a>", PathToPage, PathToPictures, lang[i], lang[i + 1]);
		//} 

		return sb.ToString();
	}
}
