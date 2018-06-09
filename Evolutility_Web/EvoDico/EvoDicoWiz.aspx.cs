using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Evolutility;

public partial class EvoWiz : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
		if (Convert.ToString(Request["action"]) == "script")
			EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Build;
		else
		{
			string formid = Request["WIZ"];
			if (String.IsNullOrEmpty(formid))
			{
				formid = "catalog";
			}
			switch (formid)
			{
				case "build":
					EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Build;
					break;
				case "dbscan":
					EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Map_DB;
					break;
				case "install":
					EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Install;
					break;
				case "xml2db":
					EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Import_XML;
					break;
				//case "csv2db":
				//    EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Import_CSV;
				//    break;
				default:
					EvoDicoWiz1.WizardMode = Wizard.EvolWizardMode.Wizard_Catalog;
					break;
			}
		}
    }
}
