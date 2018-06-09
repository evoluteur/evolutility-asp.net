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

public partial class localiz : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string pLanguage = "EN";

		if (Request["LNG"] != null)
		{
			pLanguage = Request["LNG"];
			if (String.IsNullOrEmpty(pLanguage))
				pLanguage = "EN";
		}
		switch (pLanguage)
		{
			case "CHS": // Chinese simplified
			case "ZH":
				//pLanguage = "ZH"; 
				Label1.Text = "地址本";
				break;  
			case "HI": // Hindi
				Label1.Text = "पता पुस्तिका";
				break;
			case "DE": // German
				Label1.Text = "Adressbuch";
				break;
			case "PT": // Portuguese
				Label1.Text = "Agenda de Endereços";
				break;
			case "ES": // Spanish
				Label1.Text = "Agenda Direcciones";
				break;
			case "FR": // French
				Label1.Text = "Carnet d'adresses";
				break;
			case "JP": // Japanese
				Label1.Text = "住所録";
				break;
			case "DA": // Danish
				Label1.Text = "Addresse bog";
				break;
			case "TR": // Turkish
				Label1.Text = "Adres defteri";
				break;
			case "RO": // Romanian
				Label1.Text = "Carte de adrese";
				break;
			case "CA": // Catalan
				Label1.Text = "Agenda Adreces";
				break;
			case "IT": // Italian (babelfish here)
				Label1.Text = "Libro di indirizzo";
				break;
			default: // English
				Label1.Text = "Address book";
				pLanguage = "EN";
				break;
		}
		Evo1.Language = pLanguage;
		Evo1.XMLfile = "XML/PIM/addressbook_" + pLanguage + ".XML";
	}
}
