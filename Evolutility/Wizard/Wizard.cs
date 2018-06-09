//	Copyright (c) 2003-2009 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Data.SqlClient; 
using System.IO; 
using System.Text; 
using System.Configuration; 
using System.Drawing; 
using System.Data; 
using System.Xml; 
using System.Xml.Serialization;
//using System.Web.HttpUtility; 

[assembly: TagPrefix("Evolutility", "Evol")]

namespace Evolutility 
{

	[ToolboxData("<{0}:Wizard runat=server></{0}:Wizard>"),
	Designer("Evolutility.WizardDesigner"),
	ToolboxBitmap(typeof(Wizard), "Evolutility.Wizard.bmp"),
	Themeable(false),
	DefaultProperty("Title")
	]
	public partial class Wizard : WebControl 
	{ 
		
//### Variables ######################################################################################## 
#region "Variables" 

		public enum EvolWizardMode 
		{ 
			Build = 10, 
			Install = 20, 
			Map_DB = 30, 
			Import_XML = 40, 
			//Import_CSV = 60,
			//Export = 50 
			//Heal = 120 
			Wizard_Catalog = 1000 
		} 

		private int FormID = 0; 
		private int UserID = 0;
		private DataSet ds; 
		private int prevStepID; 
		private XmlDocument myDOM = new XmlDocument(); 
		private int AppID; 
		private string AppName; 
		private string _PathXML = "xml", _PathWeb = "", _PathPix = "pixevo/"; 
		private string _SqlConnection; 
		private int MaxStep = 6; 

		private bool _Mustlogin = true; 
		private bool _BuildPages = false; 
		private bool _BuildDB = false; 
		private bool _ShowASPX = false, _ShowXML = false, _ShowSQL = false, _ShowSkin = false; 
		private int _StepID = 0;
		private string appTitle; 
		private string appXML, appSQL, appASPX; 
		private EvolWizardMode _WizardMode = EvolWizardMode.Build; 
		private string ErrorMsg = "";  
		public string ErrorMessage = "";

		public bool IEbrowser = false; 
		private const string t1 = "<table id=EvolutilityGrid1 class=\"Panel\" cellSpacing=\"1\" cellPadding=\"2\" width=\"100%\" border=\"0\" >";
		private const string t2 = "<THEAD><tr class=\"RowHeader\">";
		private const string TrTd_openPanelLabel = "<tr><td class=\"PanelLabel\">";
		private const string TrTd_openPanelLabel2 = TrTd_openPanelLabel + "<p>&nbsp;&nbsp;&nbsp;";
		internal const string tag_oTRoTD = "<tr><td>";
		internal const string tag_cTDcTR = "</td></tr>";

		private const string PagingSPCall = "EvoSP_PagedItem @SQLselect, @SQLtable, @SQLfrom, @SQLwhere, @SQLorderby, @SQLpk, @pageid, @pagesize"; 
		//private const string whereMapDB = "(rtrim(so.xtype)='U' AND so.name not like 'EvoDico%') "; 
		private const string whereMapDB = "(rtrim(so.xtype)='U') "; 
		//private const string xTableField = "EvoDico_Field INNER JOIN EvoDico_FieldType ON EvoDico_Field.TypeID=EvoDico_FieldType.ID";

		private const string btn_next = "b_nxt";

		private const string lang_Finished = "Finished!";
		private const string lang_NA = "N/A";

#endregion 

//### Properties ######################################################################################## 
#region "Properties" 

//------ Behavior ------------------ 
#region "Properties:Behavior"

		[Category("Behavior"), DefaultValue(false),
		Description("Specifies if building a component metadata also builds a new ASPX web page file.")] 
		public bool BuildPages { 
			get { return _BuildPages; } 
			set { 
				_BuildPages = value;
				ViewState["BuildPages"] = value; 
			} 
		} 

		[Category("Behavior"), DefaultValue(false),
	   Description("Specifies if building a component metadata also builds the database objects too.")] 
		public bool BuildDatabase { 
			get { return _BuildDB; } 
			set { 
				_BuildDB = value;
				ViewState["BuildDatabase"] = value; 
			} 
		}

		[Category("Behavior"), DefaultValue(0),
		Description("Currently displayed step of the selected wizard.")]
		public int StepID { 
			get { return _StepID; } 
			set { _StepID = value; } 
		}

		[Category("Behavior"), DefaultValue(false),
		Description("Displays component ASPX page code (after build).")]   
		public bool ShowASPX { 
			get { return _ShowASPX; } 
			set { _ShowASPX = value; } 
		}

		[Category("Behavior"), DefaultValue(true),
		Description("Displays component XML (after build).")]  
		public bool ShowXML { 
			get { return _ShowXML; } 
			set { _ShowXML = value; } 
		}

		[Category("Behavior"), DefaultValue(true),
		Description("Displays component SQL for database creation (after build).")]  
		public bool ShowSQL { 
			get { return _ShowSQL; } 
			set { _ShowSQL = value; } 
		}

		[Category("Behavior"), DefaultValue(false),
		Description("Displays links to different skins for testing.")]   
		public bool ShowSkin { 
			get { return _ShowSkin; } 
		   set { _ShowSkin = value; } 
		}

		[Category("Behavior"), DefaultValue("1000"),
		Description("Type of wizard.")]   
		public EvolWizardMode WizardMode { 
			get { return _WizardMode; } 
			set { _WizardMode = value; } 
		} 

#endregion 

//------ Data ------------------ 
#region "Properties:Data"

		[Category("Data"), DefaultValue(""), 
		Description("Connection string to the Database.")] 
		public string SqlConnection { 
			get { return _SqlConnection; } 
			set { _SqlConnection = value; } 
		}

		[Category("Data"), DefaultValue("Path to Wizards metadata."),
		Description("Path to Wizards metadata.")] 
		public string PathXML { 
			get { return _PathXML; } 
			set { _PathXML = value; } 
		}

		[Category("Data"), DefaultValue(""),
		Description("Path to Wizards published metadata.")] 
		public string PathWeb { 
			get { return _PathWeb; } 
			set { 
				_PathWeb = value; 
				ViewState["PathWeb"] = value; 
			} 
		}

		[Category("Data"), DefaultValue(true),
		Description("Specifies if user authentication is required.")] 
		public bool MustLogin { 
			get { return _Mustlogin; } 
			set { 
				_Mustlogin = value; 
				ViewState["MustLogin"] = value; 
			} 
		} 

#endregion 

//------ Appearance ------------------ 
#region "Properties:Appearance"

		[Category("Appearance"), DefaultValue(""), 
		Description("Virtual path to the records pictures.")] 
		public string VirtualPathPictures { 
			get { return _PathPix; }  
			set { _PathPix = value; }
		}

		[Category("Appearance"), DefaultValue("")]
		public string Title
		{
			get { return (string)ViewState["Title"]; }
			set { ViewState["Title"] = value; }
		}
		
#endregion

#endregion

		//### Life & Render ######################################################################################## 
#region "Life & Render" 
		
		protected override void OnPreRender(EventArgs e) 
		{
			const string uIDname = "EvoDicoEVOLUserID";
			StringBuilder sbSQL = new StringBuilder();
			int NbFields = 31, NbPanels = 11;
			string sql, sqlbuffer, tablename, columnname;
			StringBuilder myHTML = new StringBuilder();
			string buffer2, buffer3, buffer4;
			string fieldList = null;

			UserID = EvoTC.String2Int(GetPageSession(uIDname));
			if (string.IsNullOrEmpty(_SqlConnection))
				_SqlConnection = EvoUI.GetAppSetting("SQLConnection");
			if (_Mustlogin)
				UserID = EvoTC.String2Int(GetPageSession(uIDname));
			else
				UserID = 1;
			if (UserID < 1)
			{
				if (CheckLogin()) 
				{
					Page.Session[uIDname] = UserID;
					prevStepID = -1;
				}
			}
			FormID = EvoTC.String2Int(GetPageRequest("formID"));
			if (FormID > 0) 
			{
				prevStepID = MaxStep;
				StepID = MaxStep;
				AppID = FormID;
			}
			else 
			{ 
				AppID = EvoTC.String2Int(Convert.ToString(GetPageRequest("AppID")));
				if (prevStepID == 0) 
				{
					appTitle = GetPageRequest("appname");
					if (!string.IsNullOrEmpty(appTitle)) 
						Page.Cache["appname"] = appTitle;
				}
				else
				{
					if (Page.Cache["appname"] != null)
						appTitle = Page.Cache["appname"].ToString();
					if (string.IsNullOrEmpty(appTitle))
						appTitle = GetPageRequest("appname");
				}
			} 
			if (_WizardMode == EvolWizardMode.Build)
			{
				if (string.IsNullOrEmpty(appTitle))
					appTitle = "Test";
				sbSQL = new StringBuilder(); 
				switch (prevStepID)
				{ 
					case 0: //intro 
						string bufferTitle = appTitle.Replace("'", "''"); 
						buffer2 = CleanSQLname(bufferTitle); 
						buffer3 = GetPageRequest("entity").Replace("'", "''"); 
						if (string.IsNullOrEmpty(buffer3))
							buffer3 = bufferTitle.ToLower(); 
						buffer4 = GetPageRequest("entities").Replace("'", "''"); 
						if (string.IsNullOrEmpty(buffer4))
							buffer4 = buffer3 + "s"; 
						if (EvoTC.Right(buffer3, 1) == "s")
						{ 
							buffer3 = buffer3.Substring(0, buffer3.Length - 1); 
						}
						sbSQL.Append("INSERT INTO EvoDico_Form(title, dbtable, dbcolumnpk, entity, entities, UserID, sppaging, description"); 
						sbSQL.AppendFormat(") VALUES ('{0}','{1}','ID','{2}','{3}',{4},'{5}',", bufferTitle, buffer2, buffer3, buffer4, UserID, PagingSPCall);  
						sqlbuffer = GetPageRequest("description");
						if (string.IsNullOrEmpty(sqlbuffer))
							sbSQL.Append("'')");
						else
						{
							if (sqlbuffer.Length > 250)
								sqlbuffer = sqlbuffer.Substring(0, 250);
							sbSQL.AppendFormat("'{0}')", sqlbuffer.Replace("'", "''")); 
						}
						sbSQL.Append(EvoDB.SQL_IDENTITY);
						AppID = EvoTC.String2Int(EvoDB.GetDataScalar(sbSQL.ToString(), _SqlConnection, ref ErrorMessage));
						if (AppID > 0 && string.IsNullOrEmpty(ErrorMsg))
						{
							sql = string.Format("UPDATE EvoDico_Form SET dbtable=dbtable+'{0}' WHERE ID={0}", AppID); 
							ErrorMessage = EvoDB.RunSQL(sql, _SqlConnection, false); 
						}
						break; 
					case 1: //data def 
						sql = EvoDB.BuildSQL("dbtable", "EvoDico_Form", "ID=" + AppID.ToString(), "", 1);
						tablename = EvoDB.GetDataScalar(sql, _SqlConnection, ref ErrorMessage);
						// if user pressed browser back button (deleting panels deletes fields)
						sbSQL.Append("DELETE FROM EvoDico_Panel WHERE formID=").Append(AppID);
						// need dummy panel b/c of DB integrity constraint
						sbSQL.Append(";INSERT INTO EvoDico_Panel(FormID, Label) VALUES(").AppendFormat("{0},'{1}')", AppID, EvoDB.SQLescape(appTitle));
						sbSQL.Append(EvoDB.SQL_IDENTITY);
						int PanelID = EvoTC.String2Int(EvoDB.GetDataScalar(sbSQL.ToString(), _SqlConnection, ref ErrorMessage));
						sbSQL = new StringBuilder();
						fieldList = "~"; 
						for (int i = 1; i < NbFields; i++)
						{ 
							string buffer = GetPageRequest(String.Format("f_n{0}", i));
							if (!string.IsNullOrEmpty(buffer)) 
							{ 
								columnname = CleanSQLname(buffer);
								if (fieldList.IndexOf(String.Format("~{0}~", columnname))<0) 
								{ 
									fieldList += String.Format("{0}~", columnname); 
									int k = EvoTC.String2Int(GetPageRequest("f_t" + i.ToString())); 
									sbSQL.Append("INSERT INTO EvoDico_Field(formid,panelid,label,dbcolumn,dbcolumnRead,dbtablelov,search,searchlist,typeid,fpos)VALUES("); 
									sbSQL.AppendFormat("{0},{1},", AppID, PanelID); //formID 
									sbSQL.Append(dbformat(buffer, 0)).Append(","); //name 
									columnname = columnname.ToUpper();  
									if (k == 4)
									{
										sbSQL.AppendFormat("'{0}ID','", columnname); //dbcolumn 
										sbSQL.Append(columnname).Append("Text','"); //dbcolumnRead 
										sbSQL.Append(tablename).Append(columnname).Append("',"); //dbtablelov 
									} 
									else 
									{ 
										sbSQL.AppendFormat("'{0}','", columnname); //dbcolumn 
										sbSQL.Append(columnname).Append("','',"); //dbcolumnRead 'dbtablelov 
									} 
									//text multiline 
									if (k == 6 | i > 5) 
									{ 
										sbSQL.Append("0,0,");  //search,searchlist 
									} 
									else 
									{ 
										sbSQL.Append("1,"); //search  
										//searchlist 
										if (i < 5) 
											sbSQL.Append("1,");  
										else  
											sbSQL.Append("0,");  
									} 
									sbSQL.Append(k).Append(","); //typeid 
									sbSQL.Append(i * 10).Append(");"); //positionlist 
								} 
							} 
						} 
						ErrorMessage = EvoDB.RunSQL(sbSQL.ToString(), _SqlConnection, true); 
						break; 
					case 2: //data def details 
						for (int i = 1; i < NbFields; i++)
						{
							string s_i = i.ToString();
							int fID = EvoTC.String2Int(GetPageRequest(String.Format("f_id{0}", s_i)));
							if (fID > 0) 
							{ 
								sbSQL.Append("UPDATE EvoDico_field SET "); 
								//length 
								buffer3 = GetPageRequest("f_len" + s_i); 
								if (EvoTC.String2Int(buffer3) < 1)
									buffer3 = "100"; 
								sbSQL.AppendFormat("maxlength={0},", buffer3); 
								//options 
								buffer3 = GetPageRequest("f_op" + s_i);
								if (!String.IsNullOrEmpty(buffer3))
									sbSQL.AppendFormat("options='{0}',", buffer3.Replace("'", "''")); 
								//format 
								buffer3 = GetPageRequest("f_ft" + s_i); 
								if (! String.IsNullOrEmpty(buffer3))
									sbSQL.Append("format='").Append(buffer3.Replace("'", "''")).Append("',"); 
								//height 
								buffer3 = GetPageRequest("f_h" + s_i);
								if (!String.IsNullOrEmpty(buffer3)) 
									sbSQL.AppendFormat("height={0},", EvoTC.String2Int(buffer3));
								//required 
								sbSQL.Append("required=").Append(EvoTC.String2Int(GetPageRequest("f_rq" + s_i)));
								sbSQL.AppendFormat(" WHERE ID={0};", fID); 
							} 
						} 
						ErrorMessage = EvoDB.RunSQL(sbSQL.ToString(), _SqlConnection, true); 
						break; 
					case 3: //search, adv search, search res. 
						for (int i = 1; i < NbFields; i++) 
						{
							int fID = EvoTC.String2Int(GetPageRequest(String.Format("f_id{0}", i)));
							if (fID > 0) 
							{ 
								string s_i = i.ToString();
								sbSQL.Append("UPDATE EvoDico_field SET ");
								sbSQL.Append("search=").Append(-EvoTC.Bool2Int(GetPageRequest("f_s" + s_i) == "1"));
								sbSQL.Append(",searchadv=").Append(-EvoTC.Bool2Int(GetPageRequest("f_sa" + s_i) == "1"));
								sbSQL.Append(",searchlist=").Append(-EvoTC.Bool2Int(GetPageRequest("f_sr" + s_i) == "1"));
								sbSQL.AppendFormat(" WHERE ID={0};", fID); 
							} 
						} 
						ErrorMessage = EvoDB.RunSQL(sbSQL.ToString(), _SqlConnection, true); 
						break; 
					case 4: //panels layout 
						bool isFirst = true;
						for (int i = 1; i < NbPanels; i++) { 
							string buffer = GetPageRequest(String.Format("pn_n{0}", i));
							if (!string.IsNullOrEmpty(buffer)) 
							{
								if (isFirst)
								{
									sbSQL.Append("UPDATE EvoDico_panel SET label=").Append(dbformat(buffer, 0));
									sbSQL.Append(",width=").Append(EvoTC.String2Int(GetPageRequest("pn_w" + i.ToString()))); 
									sbSQL.AppendFormat(" WHERE FormID={0};", AppID);  
									isFirst = false;
								}
								else
								{
									sbSQL.Append("INSERT INTO EvoDico_panel(formid,label,ppos,width)VALUES("); 
									sbSQL.AppendFormat("{0},{1},{2},", AppID, dbformat(buffer, 0), i * 10);
									sbSQL.Append(EvoTC.String2Int(GetPageRequest("pn_w" + i.ToString()))).Append(");"); 
								}
							} 
						} 
						ErrorMessage = EvoDB.RunSQL(sbSQL.ToString(), _SqlConnection, true); 
						break; 
					case 5: //field layout 
						for (int i = 1; i < NbFields; i++)
						{
							int fID = EvoTC.String2Int(GetPageRequest(string.Format("f_id{0}", i)));
							if (fID > 0)
							{ 
								sbSQL.Append("UPDATE EvoDico_field SET ");
								int k = EvoTC.String2Int(GetPageRequest(String.Format("f_w{0}", i))); 
								if (k <= 1 || k > 100)
									k = 100; 
								sbSQL.AppendFormat("width={0},", k);
								sbSQL.Append("panelid=").Append(EvoTC.String2Int(GetPageRequest(string.Format("f_ft{0}", i))));
								sbSQL.AppendFormat(" WHERE ID={0};", fID); 
							} 
						} 
						ErrorMessage = EvoDB.RunSQL(sbSQL.ToString(), _SqlConnection, true); 
						break; 
					default:
						if (FormID > 0) 
							BuildApplication(_BuildDB, _BuildPages); 
						break; 
				}  
			}
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "EvolUtility", EvoUI.JSIncludeEvoScripts(_PathPix, "EN")); 
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "EvolPath", String.Format("<script type=\"text/javascript\">var EvolPATH='{0}';</script>", _PathPix)); 
		} 

		protected override void Render(System.Web.UI.HtmlTextWriter output) 
		{ 
			string intro = null; 
			StringBuilder myHTML = new StringBuilder();

			IEbrowser = Page.Request.Browser.Browser == "IE"; 
			output.Write(EvoUI.CRE); 
			output.Write("<table width=\"100%\" class=\"Panel\" cellpadding=\"10\">\n");  
			if (UserID < 1 && _Mustlogin)
			{
				output.Write(tag_oTRoTD);  
				output.Write(EvoUI.HTMLMessage("Please log in.", EvoUI.MsgType.Del));
				output.Write(EvoUI.tag_cTDcTRoTRoTD);  
				output.Write(EvoUI.FormLogin("Login", "Password", "Login", String.Empty)); 
				output.Write(tag_cTDcTR);
			} 
			else 
			{ 
				switch (_WizardMode) 
				{ 
					//////////// mode BUILD ///////////////////////////////////////////////////////////////////////////////////// 
					case EvolWizardMode.Build: 
						if (StepID > 5 && (FormID > 0 || AppID > 0)) 
						{ 
							if (FormID > 0) 
							{ 
								AppID = FormID; 
								StepID = MaxStep; 
							} 
							else 
							{ 
								FormID = AppID; 
							} 
							BuildApplication(true, false); 
							StepID = 10; 
						} 

						//############################################################################################################# 
						//HTML Generation 
						//############################################################################################################# 
						try 
						{
							myDOM.Load(FileNameWithMask(_PathXML + "Wizard_Build.xml")); 
						} 
						catch 
						{
							ErrorMessage = "XML not found or not valid \"" + _PathXML + "Wizard_Build.xml\".";  
						} 
						if (!string.IsNullOrEmpty(ErrorMessage))
						{
							output.Write(tag_oTRoTD);
							output.Write(EvoUI.HTMLMessage(ErrorMessage, EvoUI.MsgType.Error)); 
							output.Write(tag_cTDcTR); 
						} 
						else 
						{  
							XmlNode aNode = myDOM.DocumentElement.SelectSingleNode(string.Format("//step[@id={0}]", StepID)); 
							if (aNode == null)
							{ 
								Title = "Your web application";
								intro = "Congratulation! Your database driven web application is finished and ready to use.";
								output.Write("<tr class=\"PanelLabel\"><td>");
								output.Write("<h1>{0}</h1>", Title); 
								output.Write(tag_cTDcTR); 
							} 
							else
							{ 
								if (StepID > 0) 
								{ 
									if (!string.IsNullOrEmpty(appTitle)) 
										Title = String.Format("{0}: ", appTitle); 
									else 
										Title = String.Empty; 
								} 
								Title += aNode.Attributes[xAttribute.label].Value; 
								intro = aNode["intro"].InnerText;
								output.Write(HTMLTitleAndStep(Title, StepID, MaxStep + 1));
							}
							if (! String.IsNullOrEmpty(intro))
								output.Write(String.Format("<tr><td><p>{0}</p></td></tr>", intro));
							output.Write(tag_oTRoTD); 
							switch (StepID)
							{ 
								case 0: //intro 
									output.Write(FormBuild()); 
									break; 
								case 1: //(fields) Data Definition 
									output.Write(FormBuild_DataDef()); 
									break; 
								case 2: //Data Definition Details (options, required) 
									output.Write(FormBuild_DataDefDetails()); 
									break; 
								case 3: //searches 
									output.Write(FormBuild_Searches()); 
									break; 
								case 4: //layout (panels) 
									output.Write(FormBuild_LayoutPanels()); 
									break; 
								case 5: //layout (fields) 
									output.Write(FormBuild_LayoutFields()); 
									break; 
								default:
									output.Write(FormBuild_BuildApp());
									break; 
							}
							myHTML.Append(EvoUI.HTMLInputHidden("stepid", StepID.ToString())); 
							if (StepID > 0)
							{
								myHTML.Append(EvoUI.HTMLInputHidden("appTitle", appTitle));
								myHTML.Append(EvoUI.HTMLInputHidden("AppID", AppID.ToString()));
							} 
							output.Write(myHTML.ToString()); 
							output.Write(tag_cTDcTR); 
							if (StepID < MaxStep)
							{
								output.Write(TrTd_openPanelLabel2);
								output.Write(EvoUI.HTMLInputButton(btn_next, " Next ", true, String.Empty));
								output.Write(tag_cTDcTR); 
							}
						} 
						break; 
					//////////// mode INSTALL ///////////////////////////////////////////////////////////////////////////////////// 
					case EvolWizardMode.Install: 
						switch (StepID)
						{ 
							case 0:
								output.Write(HTMLTitleAndStep("Install new applications", 0, 2)); 
								output.Write(tag_oTRoTD); 
								output.Write(FormInstall()); 
								output.Write(tag_cTDcTR);
								output.Write(TrTd_openPanelLabel2);
								output.Write(EvoUI.HTMLInputButton(btn_next, " Next ", true, String.Empty)); 
								output.Write(tag_cTDcTR); 
								break; 
							case 1: 
								output.Write(HTMLTitleAndStep(lang_Finished, 1, 2)); 
								output.Write(tag_oTRoTD); 
								output.Write(FormInstall_Build()); 
								output.Write(tag_cTDcTR); 
								break; 
						} 
						break; 
					//////////// mode Catalog ///////////////////////////////////////////////////////////////////////////////////// 
					case EvolWizardMode.Wizard_Catalog:
						output.Write(TrTd_openPanelLabel);
						output.Write("<h1>Welcome to Evolutility</h1>");
						output.Write(EvoUI.tag_cTDcTRoTRoTD);  
						int i = EvoTC.String2Int(EvoDB.GetDataScalar(EvoDB.BuildSQL("count(*)", "EvoDico_Form"), _SqlConnection, ref ErrorMessage));
						output.Write(FormWizardCatalog(i)); 
						output.Write(tag_cTDcTR); 
						break; 
					//////////// mode IMPORT_DB ///////////////////////////////////////////////////////////////////////////////////// 
					case EvolWizardMode.Map_DB: 
						switch (StepID) 
						{ 
							case 0: //select db tables to map 
								output.Write(HTMLTitleAndStep("Map database tables", 0, 2));
								output.Write(tag_oTRoTD);
								output.Write(FormMapDB());
								output.Write(tag_cTDcTR);
								output.Write(TrTd_openPanelLabel2);
								output.Write(EvoUI.HTMLInputButton(btn_next, " Next ", true, String.Empty)); 
								output.Write(tag_cTDcTR); 
								break; 
							case 1: //map 
								output.Write(HTMLTitleAndStep(lang_Finished, 1, 2));
								output.Write(tag_oTRoTD);
								output.Write(FormMapDB_Objects()); 
								output.Write(tag_cTDcTR); 
								break; 
						} 
						break; 
					case EvolWizardMode.Import_XML: 
						switch (StepID)
						{ 
							case 0: //get xml 
								output.Write(HTMLTitleAndStep("Import XML metadata", 0, 2));
								output.Write(tag_oTRoTD);
								output.Write(FormXML2DB());
								output.Write(tag_cTDcTR);
								output.Write(TrTd_openPanelLabel2);
								output.Write(EvoUI.HTMLInputButton(btn_next, " Next ", true, String.Empty)); 
								output.Write(tag_cTDcTR); 
								break; 
							case 1: //map 
								output.Write(HTMLTitleAndStep(lang_Finished, 1, 2));
								output.Write(tag_oTRoTD);
								output.Write(FormXML2DBNow()); 
								output.Write(tag_cTDcTR); 
								break; 
						} 
						break; 
					//case EvolWizardMode.Import_CSV:
					//    output.Write(HTMLTitleAndStep("Import CSV file", 0, 2));
					//    output.Write(tag_oTRoTD);
					//    // TO DO 

					//    output.Write(tag_cTDcTR); 
					//    break; 
				} 
			}   
			output.Write("</table>"); 
			output.Write(EvoUI.Signature());
		} 
		
		public override void Dispose() 
		{ 
			myDOM = null; 
		} 
		
		protected override void OnLoad(System.EventArgs e) 
		{ 
			StepID = EvoTC.String2Int(GetPageRequest("stepid")); 
			prevStepID = StepID; 
			if (Page.IsPostBack)
			{ 
				if (!string.IsNullOrEmpty(GetPageRequest(btn_next)))
				{ 
					StepID += 1; 
				}  
				else if (!string.IsNullOrEmpty(GetPageRequest("pn_n")))
				{ 
					StepID = MaxStep + 1; 
				} 
			} 
			else 
			{ 
				prevStepID = -1; 
			} 
		} 
		
#endregion 

//--- Wizard Catalog ----------------------------------------------------------------------- 
#region "Wizard Catalog"

		private string FormWizardCatalog(int nbApps)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<p>How do you want to create your new application?</p>");
			myHTML.Append("<ul class=\"Queries\">");
			myHTML.Append("<li><a href=\"EvoDicoWiz.aspx?WIZ=build\">Build a new application from scratch</a></li>");
			myHTML.Append("<li><a href=\"EvoDicoWiz.aspx?WIZ=install\">Install a packaged application</a></li>");
			myHTML.Append("</ul>");

			myHTML.Append("<p>Advanced options</p>");
			myHTML.Append("<ul class=\"Queries\">");
			//myHTML.Append("<li><a href=\"EvoDicoWiz.aspx?WIZ=csv2db\">Import CSV file</a></li>");
			myHTML.Append("<li><a href=\"EvoDicoWiz.aspx?WIZ=dbscan\">Map an existing database table</a></li>");
			myHTML.Append("<li><a href=\"EvoDicoWiz.aspx?WIZ=xml2db\">Import an XML application definition</a></li>");
			myHTML.Append("</ul>");
			if (nbApps > 0)
			{
				myHTML.Append("<p>You already have <a href=\"EvoDicoForm.aspx\">").Append(nbApps).Append(" applications</a>.");
			}
			myHTML.Append("<br/>&nbsp;");
			return myHTML.ToString();
		}

#endregion 

//### MISC. ######################################################################################## 
#region "Misc." 
		
		private string BuildApplication(bool pBuildDB, bool pBuildFiles) 
		{ 
			string FullSQL = null; 
			string sqlbuffer; 
			bool YesNo = false; 
			string tablename, columnname, pk = "ID"; 
			//, dbcolumnlead As String 
			StringBuilder myHTML = new StringBuilder(); 
			string sql, buffer; 
			int i, j, k; 
			string buildError = "";

			string strAppID = AppID.ToString();
			//########### SQL - CREATE DB TABLES ######################################################## 
			if (_ShowSQL)
			{ 
				StringBuilder sbSQL = new StringBuilder();
				sbSQL.AppendLine(EvoDB.BuildSQL("title, dbtable, dbcolumnpk", "EvoDico_form", "id=" + strAppID, String.Empty, 0)); 
				sbSQL.Append(EvoDB.BuildSQL("f.*", "EvoDico_field f, EvoDico_panel p", "f.formID=" + strAppID + " AND p.formID=f.formID AND f.panelid=p.id", "p.ppos,f.fpos,f.id", 0));
				ErrorMsg = String.Empty;
				ds = EvoDB.GetData(sbSQL.ToString(), _SqlConnection, ref ErrorMsg);
				if (ErrorMsg != string.Empty || ds == null || ds.Tables[0].Rows.Count == 0)
				{
					buildError = string.Format("<p>{0}<br/>{1}</p>", ErrorMsg, sbSQL.ToString()); 
				} 
				else 
				{ 
					AppName = ds.Tables[0].Rows[0]["title"].ToString(); 
					tablename = ds.Tables[0].Rows[0]["dbtable"].ToString(); 
					//dbcolumnlead = CStr(ds.Tables[0].Rows(0).Item("dbcolumnlead"))  
					YesNo = false; 
					//create driving table 
					sbSQL = new StringBuilder();
					sbSQL.AppendFormat("CREATE TABLE [{0}] (\n", tablename);
					if (ds.Tables[0].Rows[0]["dbcolumnpk"] != null)
					{
						pk = ds.Tables[0].Rows[0]["dbcolumnpk"].ToString();
						if (pk.Trim() == "")
							pk = "ID";
					}
					sbSQL.AppendFormat("[{0}]", pk).Append(EvoDDL.intIdentity); 
					sbSQL.Append(",\n [UserID] [int] NULL,\n [Publish] [int] NULL,\n [CommentCount] [smallint] NULL"); 
					DataTable t1 = ds.Tables[1];
					for (i = 0; i < t1.Rows.Count; i++) 
					{ 
						k = Convert.ToInt32(t1.Rows[i]["typeid"]); 
						sqlbuffer = t1.Rows[i]["dbcolumn"].ToString().Replace(" ", "_"); 
						if ("-ID-USERID-PUBLISH".IndexOf(string.Format("-{0}-", sqlbuffer.ToUpper())) < 0)
						{
							sbSQL.AppendFormat(",\n [{0}] ", sqlbuffer); 
							switch (k)
							{ 
								case 5: 
								case 3: 
								case 7: 
								case 11: 
								case 12:  
									k = EvoTC.String2Int(t1.Rows[i][xAttribute.maxLength].ToString()); 
									if (k > 0)
										sbSQL.AppendFormat("[nvarchar] ({0})", k); 
									else
										sbSQL.Append(EvoDDL.dbNvarchar100); 
									break;  
								case 6: 
								case 8: //big text 
									k = EvoTC.String2Int(t1.Rows[i][xAttribute.maxLength].ToString()); 
									if (k > 500) 
										sbSQL.Append(" [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL "); 
									else 
									{  
										if (k > 0) 
											sbSQL.AppendFormat("[nvarchar] ({0})", k); 
										else
											sbSQL.Append(EvoDDL.dbNvarchar100); 
									} 
									break; 
								case 4: //lov 
									sbSQL.Append(EvoDDL.dbInt); 
									YesNo = true; 
									break; 
								case 1: //bool 
									sbSQL.Append("[bit]");  
									break; 
								case 2: 
								case 17: 
								case 18: //date time 
									sbSQL.Append("[datetime]"); 
									break; 
								case 9: //dec 
									sbSQL.Append("[money]");  
									break; 
								case 10: //int 
									sbSQL.Append(EvoDDL.dbInt);  
									break; 
								default: 
									sbSQL.Append(EvoDDL.dbNvarchar100); 
									break; 
							} 
						} 
					} 
					sbSQL.Append("\n);\n\n"); 
					//keys and defaults 
					sbSQL.Append(EvoDDL.SQLAlterTable(tablename)); 
					for (i = 0; i < t1.Rows.Count; i++)
					{ 
						k = Convert.ToInt32(t1.Rows[i]["typeid"]); 
						columnname = t1.Rows[i][xAttribute.dbColumn].ToString(); 
						switch (k) 
						{ 
							case 5: 
							case 3: 
							case 7: 
							case 11: 
							case 12: 
								sbSQL.Append(EvoDDL.SQLconstraint(tablename, columnname, "''", true)); 
								break; 
							case 4: 
								sbSQL.Append(EvoDDL.SQLconstraint(tablename, columnname, "1", true)); 
								break; 
							//Case 6 
							// sql += "[bit] NOT NULL" 
							//Case 7 
							// sql += "[datetime]" 
							case 1: 
								sbSQL.Append(EvoDDL.SQLconstraint(tablename, columnname, "0", true)); 
								break; 
						} 
					} 
					sbSQL.Append(EvoDDL.SQLconstraint(tablename, "UserID", "1", true)); 
					sbSQL.Append(EvoDDL.SQLconstraint(tablename, "Publish", "1", true)); 
					sbSQL.Append(EvoDDL.SQLconstraint(tablename, "CommentCount", "0", true)); 
					if (sbSQL.Length > 0 && pBuildDB)
					{ 
						sql = sbSQL.ToString();
						sql = sql.Substring(0, sql.Length - 1) + "\n"; 
						EvoDB.RunSQL(sql, _SqlConnection, true); 
						FullSQL = sql; 
					} 
					//LOVs tables 
					if (YesNo) 
					{ 
						for (i = 0; i < t1.Rows.Count; i++)
						{
							sbSQL = new StringBuilder();
							k = Convert.ToInt32(t1.Rows[i]["typeid"]); 
							//LOV 
							if (k == 4)
							{ 
								columnname = t1.Rows[i][xAttribute.dbColumn].ToString(); 
								columnname = columnname.Substring(0, columnname.Length - 2); 
								if(t1.Rows[i]["dbtablelov"] != null)
									buffer = t1.Rows[i]["dbtablelov"].ToString(); 
								else
									buffer = string.Format("{0}_{1}", tablename, columnname); 
								sbSQL.Append("\n").Append(EvoDDL.SQLCreateTable(buffer)); 
								sqlbuffer = Convert.ToString(t1.Rows[i]["dbcolumnreadlov"]); 
								if (string.IsNullOrEmpty(sqlbuffer)) 
									sqlbuffer = "name"; 
								sbSQL.Append("\n [").Append(sqlbuffer).Append("] nvarchar(255) not null)\n\n"); 
								sbSQL.Append(EvoDDL.SQLAlterTable(buffer));
								sbSQL.Append(" CONSTRAINT [PK_").Append(buffer).Append("] PRIMARY KEY CLUSTERED ([ID]) ON [PRIMARY]\n\n\n");
								string[] myArray = t1.Rows[i]["options"].ToString().Split(new char[] { ',' }); 
								for (j = 0; j < myArray.Length; j++) 
								{ 
									sbSQL.Append("INSERT INTO [").Append(buffer).Append("] (").Append(sqlbuffer);
									sbSQL.Append(")\n VALUES ('").Append(myArray[j].Trim().Replace("'", "''")).Append("')\n"); 
								} 
							} 
							if (sbSQL.Length>0 && pBuildDB)
							{ 
								sql = sbSQL.ToString();
								EvoDB.RunSQL(sql, _SqlConnection, true); 
								FullSQL += sql; 
							} 
						} 
					} 
					sqlbuffer = "/*** www.Evolutility.org - (c) 2009 Olivier Giulieri ***/\n";
					sqlbuffer += String.Format("/*** {0} database - {1} ***/\n\n", AppName, EvoTC.TextNow()); 
					appSQL = sqlbuffer + FullSQL + "\n"; 
					if (pBuildFiles) 
					{ 
						StreamWriter sw = new StreamWriter(_PathWeb + AppName + "-" + strAppID + ".sql"); 
						sw.WriteLine(appSQL); 
						sw.Close(); 
					} 
				} 
				
				//########### XML - CREATE XML DEFINITION ######################################################## 
				EvoDico.dicoDB2XML(AppID, 0, _SqlConnection); 
				if (_ShowXML) 
				{ 
					myHTML.Append(myDOM.InnerXml);
					myHTML.Append(EvoDico.dicoDB2XML(AppID, 0, _SqlConnection)); 
					appXML = myHTML.Replace("\"True\"", "\"1\"").Replace("\"False\"", "\"0\"").Replace(" dbtablelov=\"\"", "").ToString(); 
					if (pBuildFiles)
					{ 
						myDOM.LoadXml(myHTML.ToString()); 
						myDOM.Save(_PathXML + AppName + "-" + strAppID + ".xml"); 
					} 
				}  
				
				//########### ASPX - CREATE WEB PAGE ######################################################## 
				if (_ShowASPX)
				{
					appASPX = ASPXNestingPage(AppName, strAppID); 
					if (pBuildFiles)
					{ 
						StreamWriter sw = new StreamWriter(_PathWeb + AppName + "-" + strAppID + ".aspx"); 
						sw.WriteLine(appASPX); 
						sw.Close(); 
					} 
				} 
			} 
			return buildError; 
		}

		private string ASPXNestingPage(string AppName, string strAppID)
		{
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append("<%@ Page Language=\"vb\" AutoEventWireup=\"false\" validateRequest=\"false\" %>\n"); 
			myHTML.Append("<%@ Register Assembly=\"Evolutility.UIServer\" Namespace=\"Evolutility\" TagPrefix=\"EVOL\" %>\n"); 
			myHTML.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\n"); 
			myHTML.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n");
			myHTML.Append("<title>").Append(AppName).Append("</title>\n");
			myHTML.Append("<meta content=\"Evolutility\" name=\"GENERATOR\"/>\n"); 
			myHTML.Append("<meta content=\"http://schemas.microsoft.com/intellisense/ie5\" name=\"vs_targetSchema\"/>\n"); 
			myHTML.Append("<LINK href=\"../evolutility.css\" rel=\"stylesheet\"/>\n"); 
			myHTML.Append("</head>\n<body>\n"); 
			myHTML.Append("<h1>").Append(AppName).Append("</h1>\n");   
			myHTML.Append("<form id=\"Form1\" method=\"post\" runat=\"server\" encType=\"multipart/form-data\" >\n");  

			myHTML.Append("<p><EVOL:Evolutility id=\"EVOL1\" XMLfile=\"").Append(_PathXML).Append(AppName).Append("-").Append(strAppID).Append(".xml\" runat=\"server\" DBUpdate=\"True\" SqlConnection=\""); 
			myHTML.Append("\" virtualPathToolbar=\"../pixevo/\" dbdelete=\"True\" dbreadonly=\"False\" ShowTitle=\"False\" DisplayModeStart=\"List\">\n"); 
			myHTML.Append("</EVOL:Evolutility></p>\n"); 
			
			myHTML.Append("</form>\n</body>\n</html>\n");
			return myHTML.ToString();
		}
		
		private bool CheckLogin()
		{
			//Check if login/password is valid  
			SqlDataReader dr = null;
			string username	= GetPageRequest(EvoUI.fNameLogin);
			string aSQL;
			SqlConnection cn = new SqlConnection(_SqlConnection);

			if (string.IsNullOrEmpty(ErrorMsg) && !string.IsNullOrEmpty(username))
			{
				aSQL = EvoDB.SQL_EXEC + "EvoDicoSP_Login @login, @password";
				cn.Open();
				SqlCommand cmd = new SqlCommand(aSQL, cn);
				cmd.Parameters.Add(new SqlParameter("@login", username));
				cmd.Parameters.Add(new SqlParameter("@password", GetPageRequest(EvoUI.fNamePassword)));
				try
				{
					dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				}
				catch 
				{}
				if (string.IsNullOrEmpty(ErrorMsg) && dr != null)
				{
					if (dr.Read())
						UserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
					else
						UserID = -1; 
				}
				else
				{
					ErrorMsg = "Invalid login stored procedure.";
					username = string.Empty;
				}
				cmd.Dispose();
				cn.Close();
			}
			return UserID > 0;
		} 

		private string HTMLlov(string TableName, int SelectedItemID, string fieldColumnDisplay)
		{
			StringBuilder myHTML = new StringBuilder();
			DataSet Source = default(DataSet);

			string sql = string.Format("SELECT TOP 300 ID,rtrim({0}) as value FROM {1} ORDER BY ID", fieldColumnDisplay, TableName);
			try
			{
				Source = EvoDB.GetData(sql, _SqlConnection, ref ErrorMsg);
				if (SelectedItemID == 0)
				{
					for (int i = 0; i < Source.Tables[0].Rows.Count; i++)
					{
						myHTML.AppendFormat("<option value=\"{0}\">{1}", Source.Tables[0].Rows[i]["ID"], Source.Tables[0].Rows[i]["value"]);   //& "</option>" & vbCrLf 
					}
				}
				else
				{
					for (int i = 0; i < Source.Tables[0].Rows.Count; i++)
					{
						myHTML.Append("<option value=\"").Append(Source.Tables[0].Rows[i]["ID"]);
						if (SelectedItemID == Convert.ToInt32(Source.Tables[0].Rows[i]["ID"]))
							myHTML.Append("\" selected>");
						else
							myHTML.Append("\">");
						myHTML.Append(Source.Tables[0].Rows[i]["value"]);   //& "</option>" & vbCrLf 
					}
				}
			}
			catch (Exception ex)
			{
				myHTML.Append("<p>").Append(ex.ToString());
				myHTML.Append("<p>").Append(sql);
			}
			return myHTML.ToString();
		} 

		private string FileNameWithMask(string XMLFileName) 
		{ 
			string PhysicalPathXML = null; 
			string PhysicalPath = ""; 
			
			//absolute path 
			if (XMLFileName.IndexOf(":") > -1)
			{ 
				PhysicalPathXML = XMLFileName; 
			} 
			else
			{ 
				//assembly path 
				if (XMLFileName.Substring(0, 14).ToLower() == "<assemblypath>") 
				{ 
					PhysicalPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\\"; 
					if (PhysicalPath.Length > 6)
						PhysicalPath = PhysicalPath.Substring(6); 
					PhysicalPathXML = PhysicalPath + XMLFileName.Substring(14); 
				} 
				//web path 
				else 
				{ 
					PhysicalPathXML = Page.MapPath(XMLFileName); 
				} 
			} 
			return PhysicalPathXML; 
		} 

		private string GetPageRequest(string Key)
		{
			if (Page.Request[Key] == null)
				return String.Empty;
			else
				return Page.Request[Key].ToString();
		}

		private string GetPageSession(string Key)
		{
			if (Page.Session[Key] == null)
				return String.Empty;
			else
				return Page.Session[Key].ToString();
		} 

#endregion 

//### HTML ######################################################################################## 
#region "HTML" 
		
		private string CleanSQLname(string aName)
		{
			if (!string.IsNullOrEmpty(aName))
			{
				if (EvoTC.isInteger(aName.Substring(0, 1)))
					aName = "C" + aName;
				if (aName.Length > 30)
					aName = aName.Substring(0, 30);
				return aName.Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("/", "").Replace("\\", "").Replace("?", "").Replace(".", "").Replace("%", "").Replace("*", "").Replace(" ", "_").Replace("'", "''");
			}
			else
				return string.Empty;
		} 
		 
		private string HTMLInputTextRequiredFormated(string name, string value, string label, int maxlength, string help) 
		{ 
			StringBuilder zHTML = new StringBuilder(); 
			
			if (!string.IsNullOrEmpty(label))
				zHTML.Append(EvoUI.HTMLFieldLabel(name, label + EvoUI.FlagRequired));
			zHTML.Append(EvoUI.HTMLInputText(name, value, maxlength));
			if (!string.IsNullOrEmpty(help))
				zHTML.Append(EvoUI.HTMLHelpTip(name, help)); 
			return zHTML.ToString(); 
		}

		private string HTMLFieldLabelResize(string fName, string fLabel)
		{
			StringBuilder htmlField = new StringBuilder();
			htmlField.Append("<div class=\"FieldLabel\" onmouseover=\"javascript:EvoUI.showResize('").Append(fName).Append("',-1,this)\">");
			htmlField.AppendFormat("<label for=\"{0}\">{1}</label></div>", fName, fLabel);
			return htmlField.ToString();
		}

		private string HTMLFieldTextArea(String fID, String fLabel)
		{
			StringBuilder myHTML = new StringBuilder();
			myHTML.Append(HTMLFieldLabelResize(fID, fLabel));
			myHTML.Append(EvoUI.HTMLInputTextArea(fID, 12));
			return myHTML.ToString();
		}

		private string HTMLTitleAndStep(string Title, int StepID, int MaxStep)
		{  
			StringBuilder myHTML = new StringBuilder();

			myHTML.Append(TrTd_openPanelLabel);
			myHTML.Append("<table width=100%><tr><td valign=\"top\"><h1>");
			myHTML.Append(Title);
			myHTML.Append("</h1></td><td align=right valign=\"top\">");
			myHTML.Append(HTMLStep(StepID, MaxStep));
			myHTML.Append("</td></tr></table></td></tr>");  
			return myHTML.ToString();
		}

		private string HTMLStep(int stepid, int maxstep)
		{
			string c, mySpan = "<span class=\"n{0}{1}\"></span>";
			StringBuilder myHTML = new StringBuilder();
			myHTML.Append("<span class=\"Steps\">");
			for (int i = 1; i <= maxstep; i++)
			{
				if (i <= stepid)
					c = "d";
				else if (i == stepid + 1)
					c = "s";
				else
					c = "";
				myHTML.AppendFormat(mySpan, c, i);
			}
			myHTML.Append("</span>");
			return myHTML.ToString();
		}

		private string CheckedOrNot(bool b)
		{
			if (b)
				return " checked";
			else
				return string.Empty; 
		}

		private string ClassEvenOrOdd(bool b)
		{
			if (b)
				return " class=\"RowEven\" ";
			else
				return " class=\"RowOdd\" ";  
		}

		private string HTMLOptionsDateFormats(int TypeNum)
		{
			DateTime now = DateTime.Now;
			StringBuilder myHTML = new StringBuilder();
			myHTML.Append("<option value=\"\" selected>Default: ");
			switch (TypeNum)
			{
				case 2: // date
					myHTML.AppendFormat(EvoTC.DateFormatSD, now);
					myHTML.Append(EvoUI.HTMLOption("{0:d}", String.Format("Short date: {0:d}", now)));
					myHTML.Append(EvoUI.HTMLOption("{0:D}", String.Format("Long date: {0:D}", now)));
					myHTML.Append(EvoUI.HTMLOption("{0:dd/MM/yyyy}", String.Format("Custom dd/MM/yyyy: {0:dd/MM/yyyy}", now))); 
					break;
				case 17: // date-time
					myHTML.AppendFormat(EvoTC.DateFormatSDT, now);
					string[,] ofs = { 
						{ "{0:f}", "Full date & time" }, 
						{ "{0:F}", "Full date & long time" }, 
						{ "{0:s}", "Sortable date string" }, 
						{ "{0:U}", "Universal sortable, GMT" }, 
						{ "{0:r}", "RFC1123 date string" },
						{ "{0:dd/MM/yyyy} {0:t}", "Custom date & time" }
					};
					for (int i=0; i<6; i++)
						myHTML.Append(EvoUI.HTMLOption(ofs[i, 0], String.Format("{0}: {1}", ofs[i, 1], string.Format(ofs[i, 0], now)))); 
					break;
				case 18: //time 
					myHTML.AppendFormat(EvoTC.DateFormatST, now);
					myHTML.Append(EvoUI.HTMLOption("{0:t}", String.Format("Short time: {0:t}", now)));
					myHTML.Append(EvoUI.HTMLOption("{0:T}", String.Format("Long time: {0:T}", now))); 
					break;
			}
			return myHTML.ToString();
		}

		private string HTMLStepTableHeader(int StepID)
		{
			StringBuilder myHTML = new StringBuilder();
			XmlNodeList aNodeList = myDOM.DocumentElement.SelectNodes(string.Format("//step[@id={0}]/field", StepID));
			myHTML.AppendFormat("<span>{0}\n{1}", t1, t2);
			for (int j = 0; j < aNodeList.Count; j++)
			{
				myHTML.AppendFormat("<td width=\"{0}%\">{1}</td>", aNodeList[j].Attributes[xAttribute.width].Value, aNodeList[j].Attributes[xAttribute.label].Value);
			}
			myHTML.Append("</tr>\n");
			return myHTML.ToString();
		}

		private string HTMLToolsLink(string FormID, string AppName)
		{
			StringBuilder sb = new StringBuilder();

			if (string.IsNullOrEmpty(AppName))
				sb.Append("<p>");
			else
				sb.AppendFormat("<p>{0}: ", AppName);
			if (!string.IsNullOrEmpty(FormID))
			{
				sb.Append("<a target=\"_test\" href=\"EvoDicoTest.aspx?MODE=new&formID=").Append(FormID).Append("\"><b>Run it now!</b>").Append(EvoUI.FlagPopup);
				sb.Append("</a> - <a href=\"EvoDicoForm.aspx?ID=").Append(FormID).Append("&Mode=Edit\"><b>Customize it</b></a>");
				sb.Append(" - <a href=\"EvoDoc.aspx?ID=").Append(AppID).Append("\"><b>Design doc.</b></a>");
				if (_ShowSkin)
				{
					sb.Append("</p><p>Skin test: ");
					sb.Append(HTMLSkinLink("Autonomous", FormID));
					sb.Append(" - ").Append(HTMLSkinLink("Citrus", FormID));
					sb.Append(" - ").Append(HTMLSkinLink("Localize", FormID));
					sb.Append(" - ").Append(HTMLSkinLink("Refreshed", FormID));
					sb.Append(" - ").Append(HTMLSkinLink("Sinorca", FormID));
					sb.Append(" - ").Append(HTMLSkinLink("Sliqua", FormID));
				}
				sb.Append("</p>");
			}
			return sb.ToString();
		}

		private string HTMLSkinLink(string SkinName, string AppID)
		{
			return string.Format("<a target=\"_test\" href=\"css/evol_skin_{0}.aspx?formID={1}\">{0}{2}</a>", SkinName, AppID, EvoUI.FlagPopup);
		} 

#endregion 

//### SQL ######################################################################################## 
#region "SQL" 

		private string dbformat(string myval, int mytype)
		{
			switch (mytype)
			{
				case 0:
				case 1:
				case 10:
				case 11:
				case 12:
					if (myval == null)
						return "N''";
					else
						return string.Format("N'{0}'", myval.Replace("'", "''"));
				case 7:
					if (EvoTC.isDate(myval))
					{
						return string.Format("'{0}'", EvoTC.String2DateTime(myval).ToString("yyyy-M-d"));
					}
					else
						return EvoDB.SQL_NULL;
				case 8:
				case 9:
					return EvoTC.StrVal(myval);
				default:
					return myval;
			}
		}

#endregion 

	} 
} 
