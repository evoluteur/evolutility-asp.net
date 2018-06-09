//	Copyright (c) 2003-2011 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is open source software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the open source software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed WITHOUT ANY WARRANTY;
//	without even the implied warranty of MERCHANTABILITY
//	or FITNESS FOR A PARTICULAR PURPOSE.
//	See the GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.

//  Commercial license may be purchased at www.evolutility.org <http://www.evolutility.org/product/Purchase.aspx>.


//#define DB_MySQL
#undef DB_MySQL

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;  
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.Text;
using System.Drawing;
using System.Data;

#if DB_MySQL
using MySql.Data;
using MySql.Data.MySqlClient;
#else
using System.Data.SqlClient;
#endif

[assembly: TagPrefix("Evolutility", "Evol")]

namespace Evolutility
{
	[ToolboxData("<{0}:UIServer runat=server VirtualPathToolbar=PixEvo ></{0}:UIServer>"),
	Designer("Evolutility.UIServerDesigner"),
	ToolboxBitmap(typeof(UIServer), "Evolutility.UIServer.bmp"),
	Themeable(false),
	LicenseProviderAttribute(), 
	DefaultEvent("DBChange"), 
	DefaultProperty("Text")]
	public partial class UIServer : WebControl, IPostBackEventHandler, IPostBackDataHandler
	{

//### Variables ####################################################################################### 
#region "Variables" 

#region "UDTs"

		public enum EvolToolbarPosition
		{
			Top = 0,			
			Top_And_Bottom = 2,
			None = -1
		}//Left = 1,

		public enum EvolTabPosition
		{
			Top = 0,
			Left = 1			
		}//Right = 2

		private enum LOVFormat
		{
			HTML, 
			JSON			
		}//XML

#endregion

//### Variables - DB & XML ### 
#region "Variables DB & XML"

		private string UID;
		private string navBar;
		private string lockDbname;
		private string loclValue;
		private int _FormID = 0;
		private int _ItemID = 0, PrevItemID = 0;
		private bool PostBackEventUsed = false;
		private int ToDo = 0;
		private string _Text;
		private const string dot = ".";
		private const string dMode = "EVOL_Mode";
		private const string sValue = "value";
		private string ValidationMsg; 
		private string HeaderMsg = string.Empty;
		private int nav;
		private int navd = 2;
		private bool _NavLinks = true;
		private bool _ShowTitle = true;
		private int _DisplayMode = 110;
		private EvolDisplayMode _DisplayModeStart = EvolDisplayMode.List;
		private string ErrorMsg = string.Empty;

		private XmlNamespaceManager nsManager; 
		private DataSet ds;
		private DataSet ds2;

		private string dbcolumnicon = string.Empty; 
		// _ItemLock As Integer = -1, 
		private string icon;

		private Data def_Data;
		private const string dbPrimaryKey = "ID";
		//Private dbuserlog As String = "", dbupdatelogs As String = "", dbmylanuserlogMSG As String = "" 
		//, dbLockinglabel As String 
		private string dbwherelock;
		private string newOrderBy = dbPrimaryKey;
		private int _RowsPerPage = 20;
		private int pageID = 1;
		private string queryUrlParam = String.Empty;
		private string dbtabledetails = String.Empty;
		private string dbcolumndetails = String.Empty;
		private bool detailsLoaded = false;

		private int nbFieldEditable = 0;

		private const string mauid = "EVOLUserID";
		private const string Tilda = "~";
		private const string coma = ",";
		private const string s0 = "0", s1 = "1", s2 = "2", s3 = "3", s4 = "4";
		private const string xptCSV = "CSV", xptHTML = "HTML", xptSQL = "SQL", xptTAB = "TAB", xptXML = "XML", xptJSON = "JSON";
		//lower case b/c used for description 
		private const string SQL_and = " AND ";

		private XmlDocument myDOM = new XmlDocument();
		internal bool XMLloaded = false;

		private const string HTML_Sep = " - ";

#endregion

//### Properties ### 
#region "Properties"

		private bool _DBReadOnly;
		private bool _DataIsMetadata;
		private bool _DBAllowDesign = false, _ShowDesigner = false;
		private bool _DBAllowUpdate = true;
		private bool _DBAllowInsert = true;
		//private bool _DBAllowClone = false;
		private bool _DBAllowDelete = true;
		private bool _DBAllowUpdateDetails = false;
		private bool _DBAllowInsertDetails = false;
		private bool _DBAllowSearch = true;
		private bool _DBAllowExport = false;
		private bool _DBAllowCharts = true;
		private bool _DBAllowMassUpdate = false;
		private bool _DBAllowSelections = false;
		private bool _DBAllowHelp = false;
		private bool _DBAllowPrint = true;
		private bool _DBAllowLogout = true;
		//private bool _DBRecordAuditing = false; 
		private string _DBApplicationKey = string.Empty;
		private string _Language = "EN";

		private string _XMLfile;
		private int _UserID = -1;
		private string _ItemKey;

		private string _PathPixToolbar = "";
		private string _PathPix = "";
		private string _PathDesign = "";

		private string _SqlConnection;
		private string _SqlConnectionDico;

#endregion

//### Variables - HTML & Javascript ### 
#region "Variables HTML & Javascript"

		private bool IEbrowser = false;
		private int IEbrowserVersion;
		private EvolTabPosition _TabPosition;
		private EvolToolbarPosition _ToolbarPosition;
		private bool _AllowSorting = true;
		private bool _CollapsiblePanels = true;
		private Color _BackColorRowMouseOver;

		private const string vNameItemID = "EVOL_ItemID";
		private const string tableBeginTr = "<table class=\"Holder\"><tr>";
		private const string tableBeginTr2 = "<table class=\"Holder2\"><tr>";
		private const string trTableEnd = "</tr></table>";
		private const string TdTrTableEnd = "</td></tr></table>";
		private const string HTMLSpace3 = "&nbsp;&nbsp;&nbsp;";
		private const string inNewBrowser = "_blank";
		private const string SMALL_tag = "<small>";
		private const string SMALL_tagClose = "</small>";
		private const string vbCrLf = "\n";

#endregion

#endregion   

//### Properties ###################################################################################### 
#region "Properties" 

//------ Behavior ------------------ 
#region "Properties:Behavior"

		[Category("Behavior"),
		Description("Form currently displayed (ReadOnly property).")]
		public virtual EvolDisplayMode DisplayMode
		{
			get
			{
				switch (_DisplayMode)
				{
					case 0:
						return EvolDisplayMode.View;
					case 1:
						return EvolDisplayMode.Edit;
					case 3:
						return EvolDisplayMode.Search;
					case 4:
						return EvolDisplayMode.AdvancedSearch;
					default:
						return EvolDisplayMode.List;
				}
			}
		}

		[Category("Behavior"), DefaultValue(EvolDisplayMode.List),
		Description("Default form the control instance.")]
		public virtual EvolDisplayMode DisplayModeStart
		{
			get { return _DisplayModeStart; }
			set { _DisplayModeStart = value; }
		}

		[Category("Behavior"), DefaultValue(true), 
		Description("Allow sorting in List form.")]
		public bool AllowSorting
		{
			get { return _AllowSorting; }
			set { _AllowSorting = value; }
		}

		//[Category("Behavior"), DefaultValue(false), Description("Let users rate records.")]
		//public bool UserRating
		//{
		//    get { return _DBAllowRating; }
		//    set { _DBAllowRating = value; }
		//} 

		[Category("Behavior"), DefaultValue(true), 
		Description("Determines if users can hide and show panels.")]
		public bool CollapsiblePanels
		{
			get { return _CollapsiblePanels; }
			set { _CollapsiblePanels = value; }
		}

#endregion 

//------ Data ------------------ 
#region "Properties:Data" 

		[Category("Data"), DefaultValue(""), 
		Description("Metadata definition XML file name or form ID (when using EvoDico).")]
		public string XMLfile
		{
			get { return _XMLfile; }
			set
			{
				_XMLfile = value;
				if (EvoTC.isInteger(_XMLfile))
				{
					_FormID = Convert.ToInt32(_XMLfile);
				}
			}
		}

		[Category("Data"), DefaultValue(""), 
		Description("Connection string to the Database containing the data.")]
		public string SqlConnection
		{
			get { 
				if(_DataIsMetadata)
					return _SqlConnectionDico; 
				else
					return _SqlConnection; 
			}
			set { _SqlConnection = value; }
		}

		[Category("Data"), DefaultValue(""), 
		Description("Connection string to the Database containing the metadata.")]
		public string SqlConnectionDico
		{
			get { return _SqlConnectionDico; } 
			set { _SqlConnectionDico = value; }
		}


		[Category("Data"), DefaultValue(false),
	   Description("Indicates if the data is the metadata (uses SqlConnectionDico for data).")]
		public bool DataIsMetadata
		{
			get { return _DataIsMetadata; }
			set { _DataIsMetadata = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Allows customizations (only applies when using EvoDico).")]
		public bool DBAllowDesign
		{
			get { return _DBAllowDesign; }
			set { _DBAllowDesign = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Makes entity Read-Only (disables functions New, Edit, Save, Delete...)")]
		public bool DBReadOnly
		{
			get { return _DBReadOnly; }
			set { _DBReadOnly = value; }
		}

		[Category("Data"), DefaultValue(true), 
		Description("Enables record updates.")]
		public bool DBAllowUpdate
		{
			get { return _DBAllowUpdate; }
			set { _DBAllowUpdate = value; }
		}

		[Category("Data"), DefaultValue(true), 
		Description("Enables new records addition.")]
		public bool DBAllowInsert
		{
			get { return _DBAllowInsert; }
			set { _DBAllowInsert = value; }
		}

		//[Category("Data"), DefaultValue(true), 
		//Description("Add Clone button to the toolbar (for \"shallow copy\" or deep copy using stored procedure.")]
		//public bool DBAllowClone
		//{
		//    get { return _DBAllowClone; }
		//    set { _DBAllowClone = value; }
		//}

		[Category("Data"), DefaultValue(true),
		Description("Enables record deletion.")]
		public bool DBAllowDelete
		{
			get { return _DBAllowDelete; }
			set { _DBAllowDelete = value; }
		}

		[Category("Data"), DefaultValue(true), 
		Description("Enables search and advanced search functions.")]
		public bool DBAllowSearch
		{
			get { return _DBAllowSearch; }
			set { _DBAllowSearch = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Enables export.")]
		public bool DBAllowExport
		{
			get { return _DBAllowExport; }
			set { _DBAllowExport = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Enables charts.")]
		public bool DBAllowCharts
		{
			get { return _DBAllowCharts; }
			set { _DBAllowCharts = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Enables mass update.")]
		public bool DBAllowMassUpdate
		{
			get { return _DBAllowMassUpdate; }
			set { _DBAllowMassUpdate = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Enables selections (predefined queries).")]
		public bool DBAllowSelections
		{
			get { return _DBAllowSelections; }
			set { _DBAllowSelections = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Provides help tooltips (in edit form).")]
		public bool DBAllowHelp
		{
			get { return _DBAllowHelp; }
			set { _DBAllowHelp = value; }
		}

		[Category("Data"), DefaultValue(true), 
		Description("Shows Print as toolbar icon.")]
		public bool DBAllowPrint
		{
			get { return _DBAllowPrint; }
			set { _DBAllowPrint = value; }
		}

		[Category("Data"), DefaultValue(true),
		Description("Shows Logout toolbar icon (when the user is logged in).")]
		public bool DBAllowLogout
		{
			get { return _DBAllowLogout; }
			set { _DBAllowLogout = value; }
		}

		[Category("Data"), 
		Description("ID of the current record (Read-Only property).")]
		public int ItemID // Read-Only
		{
			get { return _ItemID; }
		}

		[Category("Data"), DefaultValue(20),
		Description("Number of records per page in lists.")]
		public int RowsPerPage
		{
			get { return _RowsPerPage; }
			set { _RowsPerPage = value; }
		}

		[Category("Data"),  
		Description("ID of the current user (Read-Only property).")]
		public int UserID // Read-Only
		{
			get { return _UserID; }
		}

		[Category("Data"), DefaultValue(EvolSecurityModel.Single_User), 
		Description("User access management: Single user, Single user with password identification, multiple users with row level security (each user only sees his own data), or multiple users sharing records (each user can view all data but only modify or delete his own).")]
		public EvolSecurityModel SecurityModel
		{
			get { return _SecurityModel; }
			set { _SecurityModel = value; }
		}

		[Category("Data"), DefaultValue(""), 
		Description("Application Key used to share credentials between application components.")]
		public string SecurityKey
		{
			get { return _DBApplicationKey; }
			set { _DBApplicationKey = value; }
		}

		//[Category("Data"), Description("Log last updates date and author.")]
		//public bool DBRecordAuditing
		//{
		//    get { return _DBRecordAuditing; }
		//    set { _DBRecordAuditing = value; }
		//}

		[Category("Data"), DefaultValue(false), 
		Description("Enables details record updates (only applies in master-details usage).")]
		public bool DBAllowUpdateDetails
		{
			get { return _DBAllowUpdateDetails; }
			set { _DBAllowUpdateDetails = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Enables new details records addition (only applies in master-details usage).")]
		public bool DBAllowInsertDetails
		{
			get { return _DBAllowInsertDetails; }
			set { _DBAllowInsertDetails = value; }
		}

		[Category("Data"), DefaultValue(EvolCommentsMode.None), 
		Description("Lets users post comments on records.")]
		public EvolCommentsMode UserComments
		{
			get { return _DBAllowComments; }
			set { _DBAllowComments = value; }
		}

		[Category("Data"), DefaultValue("EN"),
		Description("Language: \"EN\" for English, \"FR\" for French,  \"JP\" for Japanese...")]
		public string Language
		{
			get { return _Language; }
			set { _Language = value; }
		}

		[Category("Data"), DefaultValue(false), 
		Description("Enables caching of lists of values.")]
		public bool UseCache
		{
			get { return _UseCache; }
			set { _UseCache = value; }
		}

#endregion

//------ Appearance ------------------ 
#region "Properties:Appearance"

		[Category("Appearance"), DefaultValue(""),
		Description("Introduction text only displayed the first time the control is displayed.")]
		public string Text
		{
			get { return _Text; }
			set { _Text = value; }
		}

		[Category("Appearance"), DefaultValue("#FFEABB"), 
		Description("BackColor of table rows when the mouse is over it.")]
		public Color BackColorRowMouseOver
		{
			get { return _BackColorRowMouseOver; }
			set { _BackColorRowMouseOver = value; }
		}

		[Category("Appearance"), DefaultValue(EvolToolbarPosition.Top), 
		Description("Position of the toolbar.")]
		public EvolToolbarPosition ToolbarPosition
		{
			get
			{
				_ToolbarPosition = (EvolToolbarPosition)ViewState["ToolbarPosition"];
				return _ToolbarPosition;
			}
			set
			{
				_ToolbarPosition = value;
				ViewState["ToolbarPosition"] = _ToolbarPosition;
			}
		}

		[Category("Appearance"), DefaultValue(EvolTabPosition.Top), 
		Description("Position/orientation of tabs UI elements.")]
		public EvolTabPosition TabPosition
		{
			get
			{
				_TabPosition = (EvolTabPosition)ViewState["TabPosition"];
				return _TabPosition;
			}
			set
			{
				_TabPosition = value;
				ViewState["TabPosition"] = _TabPosition;
			}
		}

		[Category("Appearance"), DefaultValue(true), 
		Description("Makes title and form name visible.")]
		public bool ShowTitle
		{
			get { return _ShowTitle; }
			set { _ShowTitle = value; }
		}

		[Category("Appearance"), DefaultValue(""), 
		Description("Virtual path to the toolbar pictures.")]
		public string VirtualPathToolbar
		{
			get { return _PathPixToolbar; }
			set
			{
				if (value != string.Empty && !value.EndsWith("/"))
				{
					_PathPixToolbar = value + "/";
				}
				else
				{
					_PathPixToolbar = value;
				}
			}
		}

		[Category("Appearance"), DefaultValue(""), 
		Description("Virtual path to the records pictures.")]
		public string VirtualPathPictures
		{
			get { return _PathPix; }
			//FormatedPath(Value) 
			set { _PathPix = value; }
		}

		[Category("Appearance"), DefaultValue(""), 
		Description("Virtual path to the app designer.")]
		public string VirtualPathDesigner
		{
			get { return _PathDesign; }
			//FormatedPath(Value) 
			set { _PathDesign = value; }
		}

		[Category("Appearance"), DefaultValue(true), 
		Description("Shows navigation links to First, Previous, Next, and Last records (in Edit and View forms).")]
		public bool NavigationLinks
		{
			get { return _NavLinks; }
			set { _NavLinks = value; }
		}

#endregion

#endregion

//### Rendering ####################################################################################### 
#region "Rendering" 

		protected override void Render(System.Web.UI.HtmlTextWriter output)
		{
			if (GetPageRequest("action") == "getform")
				SendFormHTML(GetPageRequest("form"));
			else if (_DisplayMode == 71 | _DisplayMode == 72)
				RenderExport();
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(EvoUI.CRE).AppendFormat("<div id=\"{0}\" class=\"Evo {1}\">\n", this.ID, EvoLang.R2L?" R2L":string.Empty);
				if (_DisplayMode != 50)
					sb.Append(HTMLmenu(false));
				sb.Append(EvoUI.HTMLInputHidden(vNameItemID, _ItemID.ToString()));
				output.Write(sb.ToString());
				if (string.IsNullOrEmpty(ErrorMsg))
				{
					if (!string.IsNullOrEmpty(HeaderMsg))
					{
						output.Write(EvoUI.HTMLMessage(HeaderMsg, EvoUI.MsgType.Info));
						HeaderMsg = string.Empty;
					}
					else if (!Page.IsPostBack && !string.IsNullOrEmpty(_Text))
						output.Write(EvoUI.HTMLMessage(_Text, EvoUI.MsgType.x));
				}
				else
				{
					output.Write(EvoUI.HTMLMessage(ErrorMsg, EvoUI.MsgType.Error));
					ErrorMsg = string.Empty;
				}
				if (XMLloaded)
				{
					output.Write(String.Format("<span id=\"{0}_Content\">", this.ID));
					switch (_DisplayMode)
					{
						case 0: // view
						case 1: // edit
						case 2: // new
							output.Write(FormEdit(_DisplayMode));
							break;
						case 3: // search
						case 4: // advanced search  
							output.Write(FormSearch(_DisplayMode));
							break;
						case 102: // query
						case 103: // search result
						case 104: // adv search result
						case 105: // last query
						case 110: // all 
							output.Write(FormList(_DisplayMode));
							break;
						case 50: // login 
							output.Write(EvoUI.FormLogin(EvoLang.Login, EvoLang.Password, EvoLang.LoginB, GetPageRequest(EvoUI.fNameLogin)));
							break;
						case 60: // selections 
							output.Write(FormQueries());
							break;
						case 70: // export 
							output.Write(FormExport());
							break;
						case 80: // mass update 
							output.Write(FormMassUpdate());
							break;
						case 90: // charts
							output.Write(FormCharts());
							break;
						default:
							AddError("Invalid parameters.");
							break;
					}
					output.Write("</span>");
					output.Write(EvoUI.HTMLInputHidden(dMode, _DisplayMode.ToString()));
				}
				if (ErrorMsg != string.Empty && !ErrorMsg.Equals("-"))
					output.Write(EvoUI.HTMLMessage(ErrorMsg, EvoUI.MsgType.Error));
				if (_DisplayMode != 50 && _ToolbarPosition == EvolToolbarPosition.Top_And_Bottom)
					output.Write(HTMLmenu(true));
				output.Write(EvoUI.Signature());
				output.Write("</div>");
				if (genJS.Length > 0)
				{
					if (JSinDetails)
					{
						genJS.Remove(genJS.Length - 1, 1);
						genJS.Append("}}};");
					}
					else
						genJS.Append("};");
					JSWrite(genJS.ToString());
				}
			}
		}

		private void SendFormHTML(string mModeName)
		{
			// used to send form HTML for AJAX partial page refresh
			const string sep = "{0}@!#@{1}";
			string Filename;
			UTF8Encoding Encoding = new UTF8Encoding();
			System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
			response.Clear();
			Filename = def_Data.entities.Replace(" ", "_");
			response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", Filename, "html"));
			response.ContentType = "application/octet-stream";
			switch (mModeName)
			{
				case "search": // search
					response.BinaryWrite(Encoding.GetBytes(string.Format(sep, ModeName(3), FormSearch(3))));
					break;
				case "searchp": // advanced search
					response.BinaryWrite(Encoding.GetBytes(string.Format(sep, ModeName(4), FormSearch(4))));
					break;
				case "sel": // selections
					response.BinaryWrite(Encoding.GetBytes(string.Format(sep, ModeName(60), FormQueries())));
					break;
				case "mupdate": // mass update
					response.BinaryWrite(Encoding.GetBytes(string.Format(sep, ModeName(90), FormMassUpdate())));
					break;
				//case "new": // new record - MUST HAVE VALIDATION JS TO WORK
				//    _ItemID = -1;
				//    response.BinaryWrite(Encoding.GetBytes(string.Format(sep, ModeName(1), FormEdit(2))));
				//    break;
				default:
					response.BinaryWrite(Encoding.GetBytes(string.Format(sep, "Error", EvoUI.HTMLMessage("Invalid parameters in AJAX request.", EvoUI.MsgType.Info))));
					break;
			}
			response.Charset = string.Empty;
			response.End();
		}

		private void RenderExport()
		{
			// Sends Exports data in requested format
			string fileName, fileExt, formatName;
			UTF8Encoding Encoding = new UTF8Encoding();
			System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

			response.Clear();
			formatName = GetPageRequest("evoZOut", xptCSV);
			if (formatName == "JSON")
				fileExt = "JS.txt"; // easier to open from browser
			else
				fileExt = formatName;
			if(def_Data.entities==EvoLang.entities)
				fileName = def_Data.dbtable;
			else
				fileName = def_Data.entities.Replace(" ", "_");
			response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", fileName, fileExt));
			if (formatName.Equals(xptCSV))
				response.ContentType = "application/ms-excel";
			else
				response.ContentType = "application/octet-stream";
			response.BinaryWrite(Encoding.GetBytes(GenerateExport(formatName))); 
			response.Charset = string.Empty;
			if (_DisplayMode == 71)
				_DisplayMode = 0;
			else if (_DisplayMode == 72)
				_DisplayMode = 105;
			response.End();
		}

#endregion 

//### Life ############################################################################################ 
#region "Life" 

		protected override void OnPreRender(EventArgs e)
		{
			// performs CRUD operation on posted record (or insert comments)
			// also get Mode and other parameters

			StringBuilder sql = new StringBuilder();
			StringBuilder sql2 = new StringBuilder();
			int modeID = 0;
			string aSQL = "", dbcolumn;
			Int32 NbFileUploads = 0;
			int fieldReadOnly = 0;
			bool fieldRequired = false;
			string buffer;
			int i = 0;
			bool MustTriggerInsert = false;
			string fieldType;
			int fieldMaxLength = 0;
			XmlNodeList aNodeList; 

			base.OnPreRender(e);
			UID = "EVOLU_"; 	//UniqueID.Replace(":", "_") 
			EvoLang.LoadLanguage(_Language);
			EvoTC.LoadFormats(_Language);
			GetSqlConnections();
			if (dicoLoadXML(""))
			{
				//////// get parameters ////////////////////////////// 
				if (_UserID < 1 && (_SecurityModel != EvolSecurityModel.Single_User || _DBAllowComments.Equals(EvolCommentsMode.Logged_Users))) 
					_UserID = GetUserID(); 
				if (Page.IsPostBack)
				{
					if (!string.IsNullOrEmpty(def_Data.dblockingcolumn) && Page.Cache["locking"]!=null) 
						dbwherelock = Page.Cache["locking"].ToString(); 
					if (ToDo != 12 && _ItemID < 1)
						_ItemID = EvoTC.String2Int(GetPageRequest(vNameItemID, s0)); 
					if (!PostBackEventUsed)
					{
						if (ToDo != 1 && _DisplayMode != 105 && _DisplayMode != 102)
						{
							buffer = GetPageRequest(dMode);
							if (EvoTC.isInteger(buffer))
								_DisplayMode = Convert.ToInt32(buffer);
							if (_DisplayMode == 50)
								_DisplayMode += 1;
							else if (_DisplayMode > 0 && _DisplayMode < 5 && !_ShowDesigner)
								_DisplayMode += 100;
						}
					}
				}
				else
				{
					_UseCache = false;
					modeID = EvoUI.ModeRequestInt(GetPageRequest("MODE"));
					if (modeID < 50)
					{
						//new 
						if (modeID == 12)
						{
							ToDo = 25;
							modeID = 1;
							_ItemID = 0;
						}
						else
							i = EvoTC.String2Int(GetPageRequest("ID"));
					}
					else
						i = 0;
					if (i > 0)
					{
						_ItemID = i;
						if (modeID < 2)
						{
							if (_DisplayModeStart == EvolDisplayMode.Edit)
								_DisplayMode = 1;
							else
								_DisplayMode = modeID;
						}
						else
							_DisplayMode = 0;
					}
					else
					{
						if (modeID == 0)
						{
							queryUrlParam = GetPageRequest("QUERY");
							if (string.IsNullOrEmpty(queryUrlParam))
							{
								if ((int)_DisplayModeStart > -1)
								{
									_DisplayMode = (int)_DisplayModeStart;
									if (_DisplayMode == (int)EvolDisplayMode.NewItem)
									{
										_ItemID = 0;
										_DisplayMode = 1;
										ToDo = 25;
									}
								}
								else
									_DisplayMode = 110;
							}
							else
								_DisplayMode = 102;
						}
						else
							_DisplayMode = modeID;
					}
					if (ToDo != 25 && !_DBReadOnly)
					{
						if (_UserID > 0 || _SecurityModel.Equals(EvolSecurityModel.Single_User))
						{
							if ((_DBAllowUpdate && _ItemID > 0) || (_DBAllowInsert && _ItemID < 1))
							{
								switch (GetPageRequest("tdLOVE"))
								{
									case s1:
										_DisplayMode = 1;
										break;
									case "1N":
										_DisplayMode = 1;
										_ItemID = 0;
										ToDo = 25;
										break;
								}
							}
						}
					}
				}
				if (_DisplayMode == 51)
				{
					if (CheckLogin() != string.Empty)
					{
						_DisplayMode = _DisplayModeStart.GetHashCode();	// 110 'list 
						if (_DisplayMode == 0 || _DisplayMode == 1)
							nav = 1;
					}
					else
						_DisplayMode = 50;	//login 
				}
				if (_UserID > 0 || _SecurityModel.Equals(EvolSecurityModel.Single_User))
				{
					//////// processing DB CRUD ////////////////////////////// 
					//-------- user comments -------- 
					if (_DisplayMode == 0 && _DBAllowComments == EvolCommentsMode.Logged_Users)
						PostUserComments();
					aSQL = string.Empty;
					//If _DBReadOnly andalso Not (_DBAllowInsert orelse _DBAllowUpdate) Then 'check for readonly... 
					// If _DisplayMode = 1 Then _DisplayMode = 0 
					//Else 
					//check for readonly... 
					if (_DBReadOnly)
					{
						if (_DisplayMode == 1)
							_DisplayMode = 0;
					}
					else
					{
						if (_DBAllowMassUpdate && _DisplayMode == 80 && !string.IsNullOrEmpty(Page.Request["MU"]))
						{
							//-------- Mass Update -------- 
							aNodeList = myDOM.DocumentElement.SelectNodes(xQuery.aggregableFields, nsManager); 
							int maxLoop = aNodeList.Count;
							for (i = 0; i < maxLoop; i++)
							{
								XmlNode cn = aNodeList[i];
								if (cn.Attributes[xAttribute.dbReadOnly] == null)
									fieldReadOnly = 0;
								else
									fieldReadOnly = EvoTC.String2Int(cn.Attributes[xAttribute.dbReadOnly].Value);
								if (fieldReadOnly == 0)
								{
									fieldType = cn.Attributes[xAttribute.type].Value;
									dbcolumn =  cn.Attributes[xAttribute.dbColumn].Value;
									string fieldValue = GetPageRequest(UID + dbcolumn);
									if (!string.IsNullOrEmpty(fieldValue))
									{
										if (fieldType == EvoDB.t_bool)
											fieldValue = (fieldValue == "Y")?"0":"1"; 
										sql2.AppendFormat("{0}={1},", dbcolumn, fieldValue);
									}
								}
							}
							if (sql2.Length > 0)
							{
								sql2.Remove(sql2.Length - 1, 1); 
								sql.AppendFormat("UPDATE {0} SET {1}", def_Data.dbtable, sql2); 
								buffer = getSQLw();
								if(!string.IsNullOrEmpty(buffer))
									sql.Append(EvoDB.SQL_WHERE).Append("ID IN (SELECT ID FROM ").Append(def_Data.dbtable).Append(" T ").Append(EvoDB.SQL_WHERE).Append(buffer).Append(")");
								sql.Append(EvoDB.SQL_ROWCOUNT);
								string nbUpdates = EvoDB.GetDataScalar(sql.ToString(), SqlConnection, ref ErrorMsg);
								HeaderMsg = EvoTC.CondiConcat(HeaderMsg, string.Format(EvoLang.MassUpdated, nbUpdates, def_Data.entities, EvoTC.TextNowTime()), vbCrLf);
								////DEBUG
								//HeaderMsg += "<br/><br/>" + sql.ToString();
							}

						}
						else if (_DBAllowInsert || _DBAllowUpdate)
						{
							//-------- master table -------- 
							if (ToDo > -1)
							{
								NbFileUploads = 0;
								//verification + save 
								if (_DisplayMode == 101)
								{
									aNodeList = myDOM.DocumentElement.SelectNodes(xQuery.panelField, nsManager);
									ValidationMsg = string.Empty;
									//INSERT 
									if (_ItemID == 0)
									{
										if (_DBAllowInsert)
										{
											//If spInsert = "" Then 
											int maxLoop = aNodeList.Count;
											for (i = 0; i < maxLoop; i++)
											{
												XmlNode cn = aNodeList[i];
												if (cn.Attributes[xAttribute.dbReadOnly] == null)
													fieldReadOnly = 0;
												else
													fieldReadOnly = EvoTC.String2Int(cn.Attributes[xAttribute.dbReadOnly].Value);
												if (fieldReadOnly != 1)
												{
													dbcolumn = UID + cn.Attributes[xAttribute.dbColumn].Value;
													string fieldValue = GetPageRequest(dbcolumn);
													fieldType = cn.Attributes[xAttribute.type].Value;
													switch (fieldType)
													{
														case EvoDB.t_date:
															break;
														case EvoDB.t_pix:
														case EvoDB.t_doc:
															fieldValue = UploadDoc(NbFileUploads, UID + dbcolumn, fieldType == EvoDB.t_pix, false);
															NbFileUploads += 1;
															break;
														default:
															fieldMaxLength = xAttribute.GetFieldMaxLength(cn);
															break;
													}
													if (string.IsNullOrEmpty(fieldValue))
													{
														if (fieldType != EvoDB.t_bool && cn.Attributes[xAttribute.required] != null && cn.Attributes[xAttribute.required].Value == s1)
															ValidationMsg += cn.Attributes[xAttribute.label].Value + " required.";
													}
													else if (fieldValue != Tilda && fieldValue != string.Empty)
													{
														sql2.AppendFormat(EvoDB.SQL_NAME_0c, cn.Attributes[xAttribute.dbColumn].Value);
														sql.Append(EvoDB.dbFormat(fieldValue, fieldType, fieldMaxLength, _Language)).Append(coma);
													}
												}
											}
											if (string.IsNullOrEmpty(ValidationMsg))
											{
												if (sql.Length > 0)
												{
													if (_SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) || _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing) || _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Collaboration))
													{
														sql2.Append(def_Data.dbcolumnuserid).Append(coma);
														sql.Append(_UserID).Append(coma);
													}
													aSQL = EvoDB.sqlINSERT(def_Data.dbtable, sql2.ToString().Substring(0, sql2.Length - 1), sql.ToString().Substring(0, sql.Length - 1));
												}
												if (!string.IsNullOrEmpty(aSQL))
												{
													string cacheName = string.Format("LastInsert{0}", this.UniqueID);
													if (Page.Cache[cacheName] == null || aSQL != Page.Cache[cacheName].ToString())
													{
														_ItemID = EvoTC.String2Int(EvoDB.GetDataScalar(aSQL + EvoDB.SQL_IDENTITY, SqlConnection, ref ErrorMsg));
														if (_ItemID > 0)
														{
															HeaderMsg = EvoTC.CondiConcat(HeaderMsg, string.Format(EvoLang.NewSave, def_Data.entity, EvoTC.TextNowTime()), vbCrLf);
															Page.Cache[cacheName] = aSQL;
															MustTriggerInsert = true;
														}
														else
														{
															Page.Cache.Remove(cacheName);
															ToDo = 25;
														}
													}
												}
											}
											else
												AddError(ValidationMsg);
											_DisplayMode = (_DBAllowUpdate) ? 1 : 0;
											if (ErrorMsg == "")
												nav = 4;
										}
										else
											AddError(EvoLang.err_NoPermission + string.Format(EvoLang.InsertEntity, def_Data.entity));
									}
									//UPDATE 
									else
									{
										if (_DBAllowUpdate)
										{
											//If spUpdate = "" Then 
											int maxLoop = aNodeList.Count;
											for (i = 0; i < maxLoop; i++)
											{
												XmlNode cn = aNodeList[i];
												if (cn.Attributes[xAttribute.dbReadOnly] == null)
													fieldReadOnly = 0;
												else
													fieldReadOnly = EvoTC.String2Int(cn.Attributes[xAttribute.dbReadOnly].Value);
												//readonly=1, insert no edit =2 
												if (fieldReadOnly == 0)
												{
													dbcolumn = cn.Attributes[xAttribute.dbColumn].Value;
													fieldType = cn.Attributes[xAttribute.type].Value;
													if (fieldType == EvoDB.t_pix || fieldType == EvoDB.t_doc)
													{
														buffer = UploadDoc(NbFileUploads, UID + dbcolumn, fieldType == EvoDB.t_pix, false);
														NbFileUploads += 1;
													}
													else
														buffer = GetPageRequest(UID + dbcolumn);
													if (string.IsNullOrEmpty(buffer))
													{
														if (cn.Attributes[xAttribute.required] == null)
															fieldRequired = false;
														else
															fieldRequired = cn.Attributes[xAttribute.required].Value == s1;
														if (fieldRequired)
															ValidationMsg += string.Format(EvoLang.MHValidValue, cn.Attributes[xAttribute.label].Value);
														else
														{
															if (fieldType != EvoDB.t_lov && buffer != Page.Server.UrlDecode(GetPageRequest(UID + dbcolumn + "_ov")))
																sql.AppendFormat(EvoDB.SQL_NAME_0e1c, dbcolumn, EvoDB.dbFormat(buffer, fieldType, 0, _Language));
														}
													}
													else if (!buffer.Equals(Tilda) && buffer != Page.Server.UrlDecode(GetPageRequest(UID + dbcolumn + "_ov")))
														sql.AppendFormat(EvoDB.SQL_NAME_0e1c, dbcolumn, EvoDB.dbFormat(buffer, fieldType, 0, _Language));
												}
											}
											if (string.IsNullOrEmpty(ValidationMsg))
											{
												if (sql.Length > 0)
												{
													aSQL = string.Format("UPDATE {0} SET {1}", def_Data.dbtable, sql.ToString().Substring(0, sql.Length - 1));
													//If _DBRecordAuditing Then aSQL += ",lastupdate=getdate(),lastupdateby=" & _UserID 
													aSQL += string.Format(" WHERE {0}={1}", def_Data.dbcolumnpk, _ItemID);
													if (_SecurityModel.Equals(EvolSecurityModel.Multiple_Users_RLS) || _SecurityModel.Equals(EvolSecurityModel.Multiple_Users_Sharing))
														aSQL += string.Format(" AND {0}={1} ", def_Data.dbcolumnuserid, _UserID);
												}
												if (aSQL.Length > 1)
												{
													buffer = EvoDB.RunSQL(aSQL, SqlConnection, true);
													AddError(buffer);
													if (string.IsNullOrEmpty(ErrorMsg))
													{
														HeaderMsg = EvoTC.CondiConcat(HeaderMsg, string.Format(EvoLang.Updated, EvoTC.ToUpperLowers(def_Data.entity), EvoTC.TextNowTime()), vbCrLf);
														OnDBChange(new DatabaseEventArgs(DBAction.UPDATE, _ItemID));
													}
													else
														AddError(string.Format(EvoLang.err_Update, def_Data.entity, _ItemID.ToString()));
												}
												else
													HeaderMsg = EvoTC.CondiConcat(HeaderMsg, EvoLang.NoUpdate, vbCrLf);
											}
										}
										else
											AddError(EvoLang.err_NoPermission + string.Format(EvoLang.ModifyEntity, def_Data.entity));
										nav = -1;
										_DisplayMode = 1;
									}
								}
								//DELETE 
								else if (_DisplayMode == 10)
								{
									if (BuildSQLDeleteItem() < 0)
										_DisplayMode = 1;
								}
							}
							//-------- details table -------- 
							if (_DisplayMode == 1 && _ItemID > 0 && (_DBAllowUpdateDetails || _DBAllowInsertDetails) && GetPageRequest("evoUDtls") == s1)
							{
								aSQL = BuildSQLDetailsUpsert();
								if (aSQL != string.Empty)
								{
									buffer = EvoDB.RunSQL(aSQL, SqlConnection, false);
									if (string.IsNullOrEmpty(buffer))
										HeaderMsg += EvoUI.tag_BR + EvoLang.DetailsUpdated;
									else
										AddError(buffer);
								}
							}
						}
					}
					//////// get next record to display ////////////////////////////// 
					if (def_Data != null && !string.IsNullOrEmpty(def_Data.dblockingcolumn) && Page.Cache["locking"]!=null) 
						dbwherelock = Page.Cache["locking"].ToString();
					if (ToDo == 25)
					{
						_ItemID = 0;
						_DisplayMode = 1;
					}
					else
					{
						if (_DisplayMode == 0 || _DisplayMode == 1)
						{
							if (nav == 0 && _ItemID == 0)
							{
								if (Page.IsPostBack)
								{
									if (_DisplayMode == 0)
										nav = 4;
									else
										ds = null;
								}
								else
									nav = 1;
							}
							if (_ItemID != 0 || nav > 0)
							{
								PrevItemID = _ItemID;
#if DB_MySQL
								ds = EvoDB.GetDataParameters(BuildSQLnav(nav, true), SqlConnection, new MySqlParameter[] { new MySqlParameter(EvoDB.p_itemid, _ItemID) }, ref ErrorMsg);
#else
								ds = EvoDB.GetDataParameters(BuildSQLnav(nav, true), SqlConnection, new SqlParameter[] {new SqlParameter(EvoDB.p_itemid, _ItemID)}, ref ErrorMsg);
#endif
								try
								{
									if (ds.Tables[0].Rows.Count == 0)
									{
										if (_ItemID > 0)
#if DB_MySQL
											ds = EvoDB.GetDataParameters(BuildSQLnav(nav, false), SqlConnection, new MySqlParameter[] { new MySqlParameter(EvoDB.p_itemid, _ItemID) }, ref ErrorMsg);
#else
											ds = EvoDB.GetDataParameters(BuildSQLnav(nav, false), SqlConnection, new SqlParameter[] { new SqlParameter(EvoDB.p_itemid, _ItemID) }, ref ErrorMsg);
#endif
										else
											_ItemID = 0;
									}
									else
									{
										i = Convert.ToInt32(ds.Tables[0].Rows[0][dbPrimaryKey]);
										if (i == _ItemID)
										{
											if (nav == 2)
												navd = 1;
											else if (nav == 3)
												navd = nav;
										}
										else
											_ItemID = i;
									}
								} 
								catch
								{
									_ItemID = 0;
									if (string.IsNullOrEmpty(ErrorMsg))
										AddError(EvoLang.err_NoQuery);
								}
							}
							if (string.IsNullOrEmpty(ErrorMsg) && MustTriggerInsert)
								OnDBChange(new DatabaseEventArgs(DBAction.INSERT, _ItemID));
						}
					}
					aNodeList = null;
				}
				else
				{
					if (_DisplayMode == 51)
					{
						if (CheckLogin() != string.Empty)
							_DisplayMode = _DisplayModeStart.GetHashCode();
						else
							_DisplayMode = 50;
					}
				}
				if (_SecurityModel != EvolSecurityModel.Single_User && _DisplayMode != 51 && _UserID < 1)
				{
					if (string.IsNullOrEmpty(ErrorMsg) && string.IsNullOrEmpty(HeaderMsg))
						HeaderMsg = EvoLang.PleaseLogin;
					_DisplayMode = 50;
				}
			}
			JSRegisterScripts(this.ID);
		}

		public void RaisePostBackEvent(string eventArgument) 
		{ 
			int aDisplayMode = 0;		    
			if (EvoTC.isInteger(eventArgument))
			{ 
				aDisplayMode = EvoTC.String2Int(eventArgument); 
				switch (aDisplayMode) 
				{ 
					case 0: // view
					case 1: // edit
					case 10: // delete 
						_DisplayMode = aDisplayMode; 
						break; 
					case 3: // search 
					case 4: // adv search 
					case 102: // selection
					case 105: // last search result 
					case 110: // list all
						_DisplayMode = aDisplayMode; 
						ToDo = -1; 
						break; 
					case 201: // nav view
					case 202: 
					case 203: 
					case 204: 
						nav = aDisplayMode - 200; 
						_DisplayMode = 0; 
						break; 
					case 301: // nav edit
					case 302: 
					case 303: 
					case 304: 
						nav = aDisplayMode - 300; 
						_DisplayMode = 1; 
						break; 
					case 60: // queries 
						_DisplayMode = aDisplayMode; 
						break; 
					case 24: // save 
						_DisplayMode = 101; 
						break; 
					case 25: // save and add another 
						_DisplayMode = 101; 
						ToDo = aDisplayMode; 
						break; 
					case 12: // new 
						ToDo = aDisplayMode; 
						_DisplayMode = 1; 
						_ItemID = 0; 
						break; 
					case 49: // logout 
						_ItemID = GetUserID(); 
						SetUserID(-1);
						OnCredentialChange(new CredentialEventArgs(CredentialAction.Logout, _ItemID, String.Empty, _DBApplicationKey, String.Empty)); 
						_ItemID = 0; 
						_DisplayMode = 50; 
						break; 
					case 70: // export 
					case 71: 
					case 72: 
					case 80: 
					case 81: // mass update 
					case 90: // chart
						_DisplayMode = aDisplayMode; 
						break;
					default: 
						if(aDisplayMode<0) 
						{
							ToDo = aDisplayMode;
							_ItemID = -aDisplayMode;
						}
						_DisplayMode = 0; 
						break; 
				} 
				PostBackEventUsed = true; 
			} 
			else 
			{ 
				string eventType = eventArgument.Substring(0, 1);
				switch (eventType) 
				{ 
					case "-": //lookup w/ pkey string 
						ToDo = -99; 
						_ItemKey = EvoTC.Right(eventArgument, eventArgument.Length - 1); 
						_ItemID = 950; 
						//need>0 b/c rec 
						_DisplayMode = 0; 
						aDisplayMode = -1; 
						PostBackEventUsed = true; 
						break; 
					case "e": //edit 
						_DisplayMode = 1; 
						if (eventArgument.Length > 1)
						{ 
							_ItemID = Convert.ToInt32(EvoTC.Right(eventArgument, eventArgument.Length - 1)); 
							ToDo = 1; 
						} 
						break;
					case "c": //customize (designer)
						_ShowDesigner = _DBAllowDesign; 
						if (eventArgument.Length > 2)
						{
							_DisplayMode = Convert.ToInt32(eventArgument.Substring(2)); 
							//1=edit, 3=search, 4=advsearch, 110=listall 
							ToDo = -1; 
							return; 
						} 
						break; 
					case "q": //query (selection) 
						_DisplayMode = 102;
						queryUrlParam = eventArgument.Substring(2); 
						break; 
					case "a": //sort ASC 
					case "d": //sort DESC 
						_DisplayMode = 105; 
						if (eventArgument.Length > 2) 
							newOrderBy = eventArgument.Substring(2);
						if (eventType.Equals("a"))
							newOrderBy += " ASC "; // must keep the space after
						else 
							newOrderBy += " DESC"; 
						PostBackEventUsed = true; 
						ToDo = -1; 
						break; 
					case "n": //next page in list 
						_DisplayMode = 105; 
						newOrderBy = string.Empty; 
						pageID = Convert.ToInt32(EvoTC.Right(eventArgument, eventArgument.Length - 1)); 
						break;
					default: 
						if (_SecurityModel != EvolSecurityModel.Single_User && _UserID < 1)
						{ 
							_UserID = GetUserID(); 
							if (_UserID < 1) 
							{ 
								if (string.IsNullOrEmpty(Page.Request[EvoUI.evoName + "login"])) 
									_DisplayMode = 50;    //login 
								else if (!_ShowDesigner)
									_DisplayMode = 0;  
							} 
						} 
						break; 
				} 
			} 
		}

		public override void Dispose()
		{
			myDOM = null;
			nsManager = null;
			ds = null;
			ds2 = null;
			def_Data = null;
			base.Dispose();
		}

#endregion 

//### Actions ######################################################################################### 
#region "Actions" 

		private string CheckLogin() 
		{ 
			//Check if login/password is valid 
			//Returns ID, Login, Welcome message 
			string username = string.Empty; 
			string aSQL;

#if DB_MySQL
			MySqlDataReader dr = null; 
			MySqlConnection cn = new MySqlConnection(SqlConnection); 
#else
			SqlDataReader dr = null; 
			SqlConnection cn = new SqlConnection(SqlConnection); 
#endif

			if (string.IsNullOrEmpty(ErrorMsg)) 
			{ 
				username = GetPageRequest(EvoUI.fNameLogin); 
				if (string.IsNullOrEmpty(def_Data.splogin))
					aSQL = EvoDB.BuildSQL("ID", def_Data.dbtableusers, "login=@login AND password=@password", String.Empty, 1); 
				else 
					aSQL = EvoDB.SQL_EXEC + def_Data.splogin.Replace("@application", _DBApplicationKey); 
				cn.Open();
#if DB_MySQL
				MySqlCommand cmd = new MySqlCommand(aSQL, cn);
				cmd.Parameters.Add(new MySqlParameter("@login", username));
				cmd.Parameters.Add(new MySqlParameter("@password", GetPageRequest(EvoUI.fNamePassword))); 
#else
				SqlCommand cmd = new SqlCommand(aSQL, cn);
				cmd.Parameters.Add(new SqlParameter("@login", username));
				cmd.Parameters.Add(new SqlParameter("@password", GetPageRequest(EvoUI.fNamePassword))); 
#endif
				try 
				{ 
					dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
				} 
				catch (Exception DBerror) 
				{ 
					AddError(EvoUI.HTMLtextMore(EvoLang.err_NoQuery, DBerror.Message)); 
				}
				if (string.IsNullOrEmpty(ErrorMsg) && dr != null) 
				{ 
					if (dr.Read()) 
						_UserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID"))); 
					else 
						_UserID = -1; 
					if (_UserID > 0) 
					{ 
						SetUserID(_UserID); 
						try 
						{ 
							HeaderMsg = (string)dr.GetValue(dr.GetOrdinal("welcome")); 
						} 
						catch 
						{ 
							HeaderMsg = string.Format(EvoLang.Welcome, EvoTC.ToUpperLowers(username)); 
						} 
						OnCredentialChange(new CredentialEventArgs(CredentialAction.Login, _UserID, username, _DBApplicationKey, HeaderMsg)); 
					} 
					else 
					{ 
						AddError(EvoLang.InvalidLogin); 
						HeaderMsg = EvoLang.InvalidLogin2; 
						username = string.Empty; 
						OnCredentialChange(new CredentialEventArgs(CredentialAction.Invalid_Login, _UserID, username, _DBApplicationKey, HeaderMsg)); 
					} 
				} 
				else 
				{ 
					AddError("Invalid login stored procedure."); 
					username = string.Empty; 
					OnCredentialChange(new CredentialEventArgs(CredentialAction.DB_ERROR, 0, String.Empty, _DBApplicationKey, ErrorMsg)); 
				} 
				cmd.Dispose(); 
				cn.Close(); 
			} 
			return username; 
		} 

		private string UploadDoc(Int32 PixID, string fieldName, bool Pix, bool thumbnail) 
		{ 
			// save doc in server directory
			System.Web.HttpPostedFile objFile; 
			bool DocValid = true; 
			string strFileName = null; 
			string fullname = String.Empty; 
			string strFileExtension = null; 
		    
			if (Page.Request[fieldName + "_dp"] == s1)
				strFileName = string.Empty; 
			//bug? should delete physical file on server 
			else 
			{ 
				try 
				{ 
					//Saving picture file to server 
					objFile = System.Web.HttpContext.Current.Request.Files[PixID]; 
					strFileName = System.IO.Path.GetFileName(objFile.FileName); 
					if (strFileName != string.Empty) 
					{ 
						strFileExtension = System.IO.Path.GetExtension(strFileName).ToLower(); 
						if (Pix) 
							DocValid = strFileExtension.Equals(".gif") || strFileExtension.Equals(".jpg") || strFileExtension.Equals(".png"); 
						if (DocValid) 
						{ 
							if (_UserID > 0)
								strFileName = String.Format("{0}_{1}", _UserID, strFileName); 
							else 
								strFileName = EvoTC.Right(DateTime.Now.Ticks.ToString(), 5) + strFileName; 
							fullname = Page.MapPath(_PathPix) + strFileName; 
							fullname = fullname.Replace("/", "\\"); 
							objFile.SaveAs(fullname); 
						} 
						else
							HeaderMsg += string.Format("{0} {1}", EvoLang.NoUpload, EvoLang.NoUpload2); 
					} 
					else 
						strFileName = Tilda; 
				} 
				catch (Exception ex) 
				{ 
					//use headermag and not errmsg b/c want to run db upload 
					if (string.IsNullOrEmpty(fullname)) 
					{ 
						HeaderMsg += EvoLang.NoUpload + " The form may not have the attribute enctype = \"multipart/form-data\"."; 
						// or the directory is not accessible." 
						strFileName = Tilda; 
					} 
					else 
						HeaderMsg += string.Format("{0}\n{1}",EvoLang.NoUpload, ex.Message); 
					strFileName = string.Empty; 
				} 
			} 
			return strFileName; 
		} 

		private string GenerateExport(string outputType) 
		{ 
			StringBuilder sb = new StringBuilder();
			string sql, sqlw, sqlob = String.Empty; 
			int tableIndex = 0; 
			string separator = coma; 
			bool hideID = true; 
			int maxRow, maxColBL, minCol, maxCol = 0; 
			bool yesNo = true, yesNo2; 
			string fieldValue; 
			string buffer = String.Empty, buffer1, buffer2 = null; 
			string Signature = "Exported by ";
			const string cTDcrlf = "</td>\n"; 
			const string vbCrLf2 = "\n\n"; 
		    
			if (GetPageRequest(selFieldID) == s0) 
			{ 
				buffer = GetCacheKey(def_Data.dbtable); 
				sqlw = Convert.ToString(Page.Cache[String.Format("{0}_W", buffer)]);
				sqlob = Convert.ToString(Page.Cache[String.Format("{0}_O", buffer)]); 
			} 
			else 
			{ 
				sqlw = def_Data.dbwhere; 
				buffer = BuildSQLwhereSecurity(); 
				if (buffer != string.Empty)
					sqlw = EvoTC.CondiConcat(sqlw, buffer, SQL_and); 
			} 
			if (string.IsNullOrEmpty(sqlob))
			{
				if (string.IsNullOrEmpty(def_Data.dbcolumnpk))
					sqlob = "t.ID";
				else
					sqlob = string.Format("t.{0}", def_Data.dbcolumnpk);
			}
            sql = BuildSQLselect(true, 100, _FormID, def_Data.dbtable, string.Empty, sqlw, sqlob, String.Empty, 100000, String.Empty);
			//single record, maybe details 
			if (_DisplayMode == 72) 
			{ 
				//If outputType = xptCSV Or outputType = xptXML Then 
				sql += BuildSQLDetails(false); 
				//End If 
			}
			ds = EvoDB.GetData(sql, SqlConnection, ref ErrorMsg); 
			if (ds != null) 
			{ 
				maxRow = ds.Tables[0].Rows.Count - 1; 
				maxColBL = ds.Tables[0].Columns.Count - 1; 
				maxCol = maxColBL; 
				minCol = 0; 
				hideID = Page.Request["showID"] != s1; 
				if (hideID) 
					minCol = 1; 
				Signature += EvoUI.evoName + EvoLang.ucOn + EvoTC.HTMLDateFormated(EvoDB.t_datetime, DateTime.Now.ToString(), string.Empty); 
				switch (outputType) 
				{ 
					case xptXML: 
						ds.DataSetName = def_Data.entities;
						sb.Append(xQuery.XMLHeader);
						sb.AppendFormat("<!-- {0}{1}{2} --> \n", def_Data.dbtable, HTML_Sep, Signature); 
						for (tableIndex = 0; tableIndex < ds.Tables.Count; tableIndex++) 
						{ 
							DataTable t = ds.Tables[tableIndex];
							if (tableIndex == 0)
							{ 
								t.TableName = def_Data.entity; 
								buffer1 = GetPageRequest("evoRoot"); 
								if (buffer1 != string.Empty) 
									buffer1 = buffer1.Replace("<", string.Empty).Replace(">", string.Empty).Replace("\"", "'"); 
								yesNo = Page.Request["evoxpC2X"] == s2; 
								if (maxCol != maxColBL) 
								{ 
									for (int i = maxCol + 1; i <= maxColBL; i++) 
									{ 
										t.Columns[i].ColumnMapping = MappingType.Hidden; 
									} 
								} 
							} 
							else 
								t.TableName = string.Format("{0} details {1}", def_Data.entity, tableIndex); 
							if (yesNo) 
							{ 
								for (int i = minCol; i <= maxCol; i++) 
								{ 
									t.Columns[i].ColumnMapping = MappingType.Attribute; 
								} 
							} 
							if (hideID)
								t.Columns[0].ColumnMapping = MappingType.Hidden;
						} 
						sb.Append(ds.GetXml()); 
						break; 
					case xptHTML:
						sb.Append("<html>\n<head><style>table{width:100%}table,table tr td{border-collapse:collapse;border:solid 1 #C9D6E9;vertical-align:top}</style></head>\n");
						sb.Append("<table width=\"100%\" ID=\"Evolutility_Export\">\n"); 
						sb.Append(EvoUI.HTMLtrColor(Page.Request["evoColRCT"])).Append(vbCrLf); 
						//header 
						DataTable t2 = ds.Tables[0];
						for (int j = minCol; j <= maxCol; j++) 
						{
							sb.Append(" <th>").Append(t2.Columns[j].ColumnName).Append("</th>\n"); 
						}
						sb.Append("</tr>\n"); 
						//body 
						buffer1 = EvoUI.HTMLtrColor(Page.Request["evoColRCO"]) + vbCrLf; 
						buffer2 = EvoUI.HTMLtrColor(Page.Request["evoColRCE"]) + vbCrLf; 
						for (int i = 0; i <= maxRow; i++) 
						{ 
							if (yesNo) 
								sb.Append(buffer1);                        
							else 
								sb.Append(buffer2); 
							DataRow r = t2.Rows[i];
							for (int j = minCol; j <= maxCol; j++)
							{ 
								sb.Append(" <td>"); 
								try 
								{ 
									sb.Append(r[j].ToString()); 
								} 
								catch 
								{ } 
								sb.Append(cTDcrlf); 
							} 
							sb.Append(cTDcrlf); 
							yesNo = !yesNo; 
						} 
						sb.Append("</table>\n").Append(SMALL_tag).Append(Signature).Append(SMALL_tagClose); 
						sb.Append("</html>");
						break; 
					case xptSQL:
						sb.Append("\n/*** ").Append(def_Data.dbtable).Append(HTML_Sep).Append(Signature).Append(" ***/\n\n"); 
						yesNo = Page.Request["evoxpTRS"] != string.Empty;
						if (yesNo)
							sb.Append(EvoDB.SQL_BEGIN_TRANS); 
						yesNo2 = Page.Request["evoxpTRS2"] != string.Empty;
						if (yesNo2)
							sb.AppendFormat(EvoDB.SQL_ID_INSERT, def_Data.dbtable, " ON"); 
						StringBuilder sbBuff;
						for (tableIndex = 0; tableIndex < ds.Tables.Count; tableIndex++) 
						{ 
							DataTable t = ds.Tables[tableIndex];
							if (tableIndex > 0)
							{ 
								maxCol = t.Columns.Count - 1; 
								maxRow = t.Rows.Count - 1; 
							}
							sbBuff = new StringBuilder();
							sbBuff.Append(EvoDB.SQL_INSERT); 
							if (tableIndex == 0)
								sbBuff.AppendFormat("{0} (", def_Data.dbtable); 
							else 
							{
								sbBuff.AppendFormat("{0}Details{1} (", def_Data.dbtable, tableIndex); 
								maxCol = t.Columns.Count - 1; 
								maxRow = t.Rows.Count - 1; 
							} 
							for (int i = minCol; i <= maxCol; i++) 
							{ 
								sbBuff.Append(t.Columns[i].ColumnName).Append( ", "); 
							}
							sbBuff.Remove(sbBuff.Length - 2, 2);
							sbBuff.Append(")\n VALUES (\n");
							buffer = sbBuff.ToString();
							for (int i = 0; i <= maxRow; i++)
							{ 
								DataRow r = t.Rows[i];
								sb.Append(buffer); 
								for (int j = minCol; j <= maxCol; j++) 
								{ 
									try 
									{ 
										fieldValue = Convert.ToString(r[j]); 
									} 
									catch 
									{ 
										fieldValue = string.Empty; 
									} 
									sb.Append(EvoDB.dbformat2(fieldValue, Convert.ToString(t.Columns[j].DataType), _Language)); 
									if (j < maxCol)
										sb.Append(", "); 
								}
								sb.Append(");\n"); 
							} 
						} 
						sb.Append(vbCrLf2); 
						if (yesNo2)
							sb.AppendFormat(EvoDB.SQL_ID_INSERT, def_Data.dbtable, "OFF");
						if (yesNo)
							sb.Append(EvoDB.SQL_COMMIT_TRANS);
						break;
					case xptJSON: 
						sb.Append(def_Data.dbtable).Append("=["); 
						t2 = ds.Tables[0]; 
						for (int i = 0; i <= maxRow; i++) 
						{ 
							DataRow r = t2.Rows[i];
							sb.Append("\n{");  
							for (int j = minCol; j <= maxCol; j++)
							{ 
								sb.Append(t2.Columns[j].ColumnName).Append(":'"); 
								try 
								{
									sb.Append(EvoJSON.JSONEncode(r[j].ToString())); 
								} 
								catch 
								{ } 
								sb.Append("', ");
							}
							sb.Remove(sb.Length - 2, 2);
							sb.Append("},\n");  
						} 
						sb.Remove(sb.Length - 2, 2);
						sb.Append("\n]\n");
						break; 
					default: //"CSV", "TAB", "TXT", "XLS" 
						if (outputType.Equals(xptTAB))
							separator = "\t"; 
						else 
							separator = GetPageRequest("FLS_evol", coma); 
						for (tableIndex = 0; tableIndex < ds.Tables.Count; tableIndex++) 
						{ 
							DataTable t = ds.Tables[tableIndex];
							//db header 
							if (tableIndex == 0)
							{ 
								//need label from metadata                             
								if (_DisplayMode == 71)//many recs 
									yesNo = GetPageRequest(UID + "FLH") == s1; //FLH=First Line Header 
								else //1 rec + details
									yesNo = true; 
							} 
							else 
							{ 
								maxCol = t.Columns.Count - 1; 
								maxRow = t.Rows.Count - 1; 
							} 
							if (tableIndex == 0 || (tableIndex > 0 && maxRow > -1)) 
							{ 
								//- xport header 
								//first line as header 
								if (yesNo) 
								{ 
									for (int j = minCol; j <= maxCol; j++) 
									{ 
										try 
										{ 
											fieldValue = t.Columns[j].ColumnName; 
										} 
										catch 
										{ 
											fieldValue = string.Empty; 
										} 
										if (fieldValue.IndexOf(separator) > -1)
											sb.Append(EvoUI.inQuote(fieldValue)); 
										else 
											sb.Append(fieldValue); 
										if (j < maxCol) 
											sb.Append(separator); 
									} 
									sb.Append(vbCrLf); 
								} 
								//- body 
								for (int i = 0; i <= maxRow; i++) 
								{
									DataRow r = t.Rows[i];
									for (int j = minCol; j <= maxCol; j++)
									{ 
										fieldValue = Convert.ToString(r[j]);
										if (!String.IsNullOrEmpty(fieldValue)) 
										{ 
											if (fieldValue.IndexOf(separator) > -1) 
												sb.Append(EvoUI.inQuote(fieldValue)); 
											else 
											{ 
												if (fieldValue.IndexOf(vbCrLf) > -1)
													fieldValue = fieldValue.Replace(vbCrLf, "|"); 
												sb.Append(fieldValue); 
											} 
										} 
										if (j < maxCol)
											sb.Append(separator); 
									} 
									sb.Append(vbCrLf); 
								} 
							} 
							sb.Append(vbCrLf); 
						} 
						break; 
				} 
				sb.Append(vbCrLf); 
			} 
			else 
				sb.Append(EvoLang.err_NoData); 
			return sb.ToString(); 
		} 

#endregion 

//### Dico ############################################################################################ 
#region "Dico" 

		internal bool dicoLoadXML(string XML) 
		{
			// Loads metadata from XML or EvoDico DB
			bool LoadResult = true; 
			string XMLfileName = null; 
			XmlNode aNode; 
		    
			nsManager = new XmlNamespaceManager(new NameTable());
			nsManager.AddNamespace("evo", xQuery.evoNameSpace); 
			if (_FormID > 0) 
			{
				XML = EvoDico.dicoDB2XML(_FormID, _UserID, true, SqlConnectionDico);
				if (XML.Length < 50)
					XML = string.Empty;
			} 
			else 
				DBAllowDesign = false; 
			if (!string.IsNullOrEmpty(XML))
			{ 
				try
				{ 
					myDOM.LoadXml(XML); 
					LoadResult = true; 
				} 
				catch 
				{ 
					LoadResult = false; 
					AddError("EvoDico incorrect or unavailable."); 
				} 
			} 
			else
			{ 
				if (!string.IsNullOrEmpty(_XMLfile)) 
				{
					if (string.IsNullOrEmpty(SqlConnectionDico))
					{ 
						AddError("No database connection specified."); 
						LoadResult = false; 
					} 
					else 
					{ 
						try 
						{ 
							XMLfileName = FileNameWithMask(_XMLfile); 
							myDOM.Load(XMLfileName); 
							LoadResult = true; 
						} 
						catch 
						{ 
							LoadResult = false;
							AddError(EvoUI.HTMLtextMore(EvoLang.err_DefDico, string.Format("The file \"{0}\" is unaccessible or the XML is invalid.", XMLfileName))); 
						} 
					} 
				} 
				else
				{
					AddError(EvoUI.HTMLtextMore(EvoLang.err_DefDico, "Invalid Application ID or missing XML file.")); 
					LoadResult = false; 
				} 
			} 
			if (LoadResult)
			{
				foreach (XmlNode cNode in myDOM.ChildNodes)
				{
					if (cNode.NodeType == XmlNodeType.Element && cNode.Name == xElement.form) 
					{
						if (cNode.NamespaceURI != xQuery.evoNameSpace) 
						{ 
							LoadResult = false;
							AddError(EvoUI.HTMLtextMore(EvoLang.err_DefDico, string.Format("Namespace \"{0}\" is not set in file \"{1}\".", xQuery.evoNameSpace, XMLfileName))); 
						} 
						break; 
					} 
				} 
				if (LoadResult)
				{ 
					try 
					{ 
						aNode = myDOM.SelectSingleNode(xQuery.data, nsManager); 
						//must limit to text fields 
					} 
					//ex As Exception 
					catch 
					{ 
						aNode = null; 
					} 
					if (aNode == null) 
					{ 
						LoadResult = false; 
						AddError(EvoUI.HTMLtextMore(EvoLang.err_DefDico, "The element \"data\" is not set.")); 
					} 
					else 
					{ 
						try 
						{
							def_Data = Data.Deserialize(aNode);
						} 
						catch //(Exception ex) 
						{ 
							AddError(EvoUI.HTMLtextMore(EvoLang.err_XML, "The element \"data\" is missing attributes.")); 
						} 
						aNode = null; 
						if (def_Data == null) 
						{ 
							LoadResult = false; 
							AddError(EvoLang.err_XML); 
						} 
						else 
						{ 
							if (def_Data.dbcommentsformid<1) 
								def_Data.dbcommentsformid = _FormID;
							if (string.IsNullOrEmpty(def_Data.entity)) 
								def_Data.entity = EvoLang.entity;
							if (string.IsNullOrEmpty(def_Data.entities))
								def_Data.entities = EvoLang.entities;
							EvoLang.allEntities = string.Format(EvoLang.AllEntities, def_Data.entities); 
							if (!string.IsNullOrEmpty(def_Data.icon)) 
								icon = EvoUI.HTMLIcon(_PathPixToolbar, def_Data.icon); 
							if (!string.IsNullOrEmpty(def_Data.dbcolumnicon)) 
								dbcolumnicon = def_Data.dbcolumnicon; 
						} 
						IEbrowser = Page.Request.Browser.Browser == "IE"; 
						if (IEbrowser) 
							IEbrowserVersion = Page.Request.Browser.MajorVersion; 
					} 
				} 
			}
			if(_ShowDesigner)
				_ShowDesigner = _FormID > 0; 
			XMLloaded = LoadResult; 
			return LoadResult; 
		} 

#endregion 

//### Misc ############################################################################################ 
#region "Misc" 

		private string FileNameWithMask(string XMLFileName) 
		{ 
			string PhysicalPathXML = null; 
			string PhysicalPath = string.Empty; 
		    
			//absolute path 
			if (XMLFileName.IndexOf(":") > -1)
				PhysicalPathXML = XMLFileName; 
			else 
			{ 
				if (XMLFileName.Length > 14)
				{ 
					//assembly path 
					if (XMLFileName.StartsWith("<assemblypath>")) 
					{ 
						PhysicalPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\\"; 
						if (PhysicalPath.Length > 6) 
							PhysicalPath = PhysicalPath.Substring(6); 
						PhysicalPathXML = PhysicalPath + XMLFileName.Substring(14); 
					} 
					//web path 
					else 
						PhysicalPathXML = Page.MapPath(XMLFileName); 
				} 
				//web path 
				else
					PhysicalPathXML = Page.MapPath(XMLFileName); 
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
		private string GetPageRequest(string Key, string DefaultValue) 
		{
			string Buffer = GetPageRequest(Key).Trim();
			if (Buffer == string.Empty)
				return DefaultValue;
			else
				return Buffer;
		} 

		private string GetCacheKey(string TableName)
		{
			return string.Format("sql{0}{1}{2}", _UserID, TableName, _FormID); 
		} 

		//maybe want to have another session or cookie variable w/ checksum 
		private int GetUserID() 
		{ 
			return EvoTC.String2Int(Convert.ToString(Page.Session[_DBApplicationKey + mauid])); 
		} 

		private void SetUserID(int ID) 
		{
			Page.Session[_DBApplicationKey + mauid] = ID.ToString();
		} 

		private void AddError(string msg) 
		{
			if (ErrorMsg.IndexOf(msg, 0) < 0)
			{
				if (string.IsNullOrEmpty(ErrorMsg))
					ErrorMsg = msg;
				else if (ErrorMsg.IndexOf(msg) == -1)
					ErrorMsg += vbCrLf + msg;
			}
		}

		private void GetSqlConnections()
		{
			if (string.IsNullOrEmpty(SqlConnection))
				SqlConnection = EvoUI.GetAppSetting("SQLConnection");
			if (string.IsNullOrEmpty(SqlConnectionDico))
			{
				SqlConnectionDico = EvoUI.GetAppSetting("SQLConnectionDico");
				if (string.IsNullOrEmpty(SqlConnectionDico))
					SqlConnectionDico = SqlConnection;
			}
		}

		private string getSQLw() 
		{
			string sqlw = Convert.ToString(Page.Cache[String.Format("{0}_W", GetCacheKey(def_Data.dbtable))]); 
			if (string.IsNullOrEmpty(sqlw))
			{
				sqlw = def_Data.dbwhere;
				string buffer = BuildSQLwhereSecurity();
				if (buffer != string.Empty)
					sqlw = EvoTC.CondiConcat(sqlw, buffer, SQL_and);
			}
			return sqlw;
		}

		//private bool showLicInfo() 
		//{
		//    string ln = EvoUI.GetAppSetting("EvolutilityLicense").Trim();
		//    if (ln.Length==6)
		//    {
		//        try
		//        {
		//            int i = EvoTC.String2Int(ln);
		//            Random r = new Random();
		//            string ln2 = (i * r.Next(1, 7)).ToString();
		//            ln2 = ln2 + ln2;
		//            return ln2.IndexOf(ln)<0;
		//        }
		//        catch 
		//        {
		//            return true;
		//        }
		//    }
		//    return true;
		//}

#endregion 

//### Events ########################################################################################## 
#region "Events" 

		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection) 
		{
			return true; // BUG ???
		} 

		public void RaisePostDataChangedEvent() 
		{ 
		} 

		public enum DBAction 
		{ 
			INSERT = 1, 
			UPDATE = 2, 
			DELETE = 3, 
			DB_ERROR = 4 
		} 

		public enum CredentialAction 
		{ 
			Login = 1, 
			Invalid_Login = 2, 
			Logout = 3, 
			DB_ERROR = 4 
		} 

///// DB Events /////////////////////////////////////////////////////////////////////////////////// 
#region "DB Events" 

		public class DatabaseEventArgs : EventArgs 
		{ 
			private DBAction pAction; 
			private int pID; 
		    
			//Constructor 
			public DatabaseEventArgs(DBAction Action, int ID) 
			{ 
				this.pAction = Action; 
				this.pID = ID; 
			} 
			public int ID { 
				get { return pID; } 
			} 
			public DBAction Action { 
				get { return pAction; } 
			} 
		} 

		public delegate void DBChangeEventHandler(object sender, DatabaseEventArgs e);

        [Category("Database"), Description("Raised when a record is Inserted, Updated, or Deleted.")] 
		public event DBChangeEventHandler DBChange;

        protected virtual void OnDBChange(DatabaseEventArgs e)
		{
			if(DBChange != null)
				DBChange(this, e);
        }

#endregion 

///// Login Events /////////////////////////////////////////////////////////////////////////////////// 
#region "Login Events" 

		public class CredentialEventArgs : EventArgs 
		{ 
			private CredentialAction pAction; 
			private int pUserID; 
			private string pUserName; 
			private string pDBApplicationKey; 
			private string pDescription; 
		    
			//Constructor 
			public CredentialEventArgs(CredentialAction Action, int UserID, string UserName, string DBApplicationKey, string Description) 
			{ 
				this.pAction = Action; 
				this.pUserID = UserID; 
				this.pUserName = UserName; 
				this.pDBApplicationKey = DBApplicationKey; 
				this.pDescription = Description; 
			} 
			public CredentialAction Action 
			{ 
				get { return pAction; } 
			} 
			public int UserID 
			{ 
				get { return pUserID; } 
			} 
			public string UserName 
			{ 
				get { return pUserName; } 
			} 
			public string DBApplicationKey
			{ 
				get { return pDBApplicationKey; } 
			} 
			public string Description 
			{ 
				get { return pDescription; } 
			} 
		} 

		public delegate void CredentialChangeEventHandler(object sender, CredentialEventArgs e);

		[Category("Database"), Description("Raised when the user logs in/out.")]
		public event CredentialChangeEventHandler CredentialChange;

		protected virtual void OnCredentialChange(CredentialEventArgs e)
		{
			if (CredentialChange != null)
				CredentialChange(this, e);
		}

#endregion 

#endregion 

	}
	
}
