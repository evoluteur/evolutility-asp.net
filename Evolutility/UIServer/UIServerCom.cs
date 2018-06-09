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


using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 
//using System.Data.SqlClient;

namespace Evolutility
{
	// ==================   User Comments & Rating  ==================

	public enum EvolCommentsMode
	{
		None = -1,
		Read_Only = 0,
		Logged_Users = 1,
		Anonymous = 2
	}
	
	partial class UIServer 
	{
		private EvolCommentsMode _DBAllowComments = EvolCommentsMode.None;
		private bool noCommentsHere = false;
		private EvolSecurityModel _SecurityModel = EvolSecurityModel.Single_User;
		private bool _UseCache = false;
		private const string SQLColNbComments = "CommentCount";
		private const string fieldComments = "EVOLComPost";
		
//### Comments ############################################################################### 
#region "Comments" 

		private string FormComments(int nbComments) 
		{ 
			//display comments list and new comment form 

			bool YesNo = false; 
			int PanelID = -1; 
			StringBuilder myHTML = new StringBuilder(); 
		    
			myHTML.Append("<div class=\"PanelComments\">"); 
			YesNo = _DBAllowComments.Equals(EvolCommentsMode.Logged_Users) && _UserID > 0; 
			if (YesNo) 
			{ 
				string linkLabel;
				if (nbComments > 0)
				{
					myHTML.AppendFormat(EvoLang.ucNb, nbComments.ToString(), def_Data.entity).Append(" ");
					linkLabel = EvoLang.ucAdd; 
				} 
				else 
				{ 
					myHTML.AppendFormat(EvoLang.ucNoComments, def_Data.entity).Append(" ");
					linkLabel = EvoLang.ucPost; 
				}
				myHTML.Append(FormCommentPost(linkLabel));
				//'list of comments 
				if (!noCommentsHere && nbComments > 0 && ds2 != null)
				{
					PanelID = ds2.Tables.Count - 1;
					if (PanelID > -1)
					{
						DataTable t = ds2.Tables[PanelID];
						if (t.Rows.Count < nbComments)
							nbComments = t.Rows.Count;
						for (int i = 0; i < nbComments; i++)
						{
                            myHTML.Append("<div class=\"evoSep\"></div>");
							DataRow r = t.Rows[i];
							myHTML.Append(EvoUI.HTMLPixCommentUser);
							try
							{
								//myHTML.Append(EvoLang.ucFrom);
								if (String.IsNullOrEmpty(def_Data.userpage))
									myHTML.Append(r["login"]);
								else
									myHTML.Append("<a href=\"").Append(def_Data.userpage).Append("?ID=").Append(r["userid"]).Append("\">").Append(r["login"]).Append("</a>");
								myHTML.Append(EvoLang.ucOn).Append(EvoTC.formatedDateTime((System.DateTime)r["creationdate"]));
								myHTML.Append(".<div class=\"FieldComments\">");
								myHTML.Append(EvoTC.Text2HTMLwBR(Convert.ToString(r["message"])));
								myHTML.Append("</div>");
							}
							catch
							{
								myHTML.Append("<div class=\"evoSep\"></div><div class=\"FieldReadOnly\">");
								myHTML.Append(EvoLang.ucMissing).Append("</div>");
								break;
							}
						}
					} 
				} 
			} 
			myHTML.Append("</div>"); 
			return myHTML.ToString(); 
		}

		private string FormCommentPost(string linkLabel)
		{
			// HTML for link to show the "post comments" 

			StringBuilder myHTML = new StringBuilder();
			myHTML.Append("<a id=\"evoCOMcfzlink\" href=\"Javascript:Evol.commentsForm()\">");
			myHTML.AppendFormat("{0}</a>", EvoUI.HTMLPixCommentAdd + linkLabel);
			myHTML.Append(EvoUI.HTMLDiv("evoCOMcfz", false)).Append("</div>");
			return myHTML.ToString(); 
		}

		private void PostUserComments() 
		{ 
			// generate SQL and executes it to post user comments

			const string CacheKey = "LastComment";
			string buffer = Page.Request[fieldComments]; 
			if (!string.IsNullOrEmpty(buffer)) 
			{				
				string aSQL = string.Format("{0},{1},{2}", _ItemID, _UserID, EvoDB.dbFormat(buffer, EvoDB.t_txtm, 1000, _Language));
				bool OK2post = Page.Cache[CacheKey] == null;
				if (!OK2post)
					OK2post = Convert.ToString(Page.Cache[CacheKey]) != aSQL;
				if (OK2post) 
				{
					Page.Cache[CacheKey] = aSQL; 
					string um = def_Data.dbcolumncomments + ",userid,message"; 
					if (def_Data.dbcommentsformid > 0)
						aSQL = EvoDB.sqlINSERT(def_Data.dbtablecomments, um + ",formid", String.Format("{0},{1}", aSQL, def_Data.dbcommentsformid)); 
					else
						aSQL = EvoDB.sqlINSERT(def_Data.dbtablecomments, um, aSQL);
					aSQL += EvoDB.sqlUPDATE(def_Data.dbtable, string.Format(EvoDB.SQL_INCREMENT, SQLColNbComments), EvoDB.IDequals(_ItemID)); 
					if(_UserID>0 && !String.IsNullOrEmpty(def_Data.dbtableusers))
						aSQL += EvoDB.sqlUPDATE(def_Data.dbtableusers, string.Format(EvoDB.SQL_INCREMENT, SQLColNbComments), EvoDB.IDequals(_UserID)); 
					buffer = EvoDB.RunSQL(aSQL, SqlConnection, true); 
					if (string.IsNullOrEmpty(buffer))
						HeaderMsg = EvoTC.CondiConcat(HeaderMsg, string.Format(EvoLang.ucPostedOn, EvoTC.TextNowTime()), vbCrLf);
					else 
						AddError(buffer); 
				} 
			} 
		} 

#endregion 

//### Users Rating ############################################################################### 
#region "Users Rating"

		// FEATURE REQUEST: AJAX to handle user ratings and comments

#endregion 

	}
}
