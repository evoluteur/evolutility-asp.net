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
using System.Collections.Generic;
using System.Text;
using System.Data; 
//using System.Data.SqlClient;

namespace Evolutility
{
	public enum EvolCommentsMode
	{
		None = -1,
		Read_Only = 0,
		Logged_Users = 1,
		Anonymous = 2
	}
	
	partial class UIServer //  Comments & Rating
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
		    
			myHTML.Append("<table class=\"PanelComments\" width=\"100%\" cellpadding=\"5\" cellspacing=\"0\"><tr><td><p>"); 
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
							myHTML.Append("<hr>");
							DataRow r = t.Rows[i];
							myHTML.Append(EvoUI.PixCommentUser);
							try
							{
								//myHTML.Append(EvoLang.ucFrom);
								if (String.IsNullOrEmpty(def_Data.userpage))
									myHTML.Append(r["login"]);
								else
									myHTML.Append("<a href=\"").Append(def_Data.userpage).Append("?ID=").Append(r["userid"]).Append("\">").Append(r["login"]).Append("</a>");
								myHTML.Append(EvoLang.ucOn).Append(EvoTC.formatedDateTime((System.DateTime)r["creationdate"]));
								myHTML.Append(".<br/><span class=\"FieldComments\">");
								myHTML.Append(EvoTC.Text2HTMLwBR(Convert.ToString(r["message"])));
								myHTML.Append("<br/></span>");
							}
							catch
							{
								myHTML.Append("<hr><span class=\"FieldReadOnly\">");
								myHTML.Append(EvoLang.ucMissing).Append("</span>");
								break;
							}
						}
					} 
				} 
			} 
			myHTML.Append(TdTrTableEnd); 
			return myHTML.ToString(); 
		}

		private string FormCommentPost(string linkLabel)
		{
			StringBuilder myHTML = new StringBuilder();
			myHTML.Append("<a id=\"evoCOMcfzlink\" href=\"Javascript:Evol.commentsForm()\">");
			myHTML.AppendFormat("{0}</a>", EvoUI.PixCommentAdd + linkLabel);
			myHTML.Append(EvoUI.HTMLDiv("evoCOMcfz", false)).Append("</div>");
			return myHTML.ToString(); 
		}

		private void PostUserComments() 
		{ 
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
					aSQL += EvoDB.sqlUPDATE(def_Data.dbtable, string.Format(EvoDB.SQL_INCREMENT, SQLColNbComments), string.Format("ID={0}", _ItemID.ToString())); 
					if(_UserID>0 && !String.IsNullOrEmpty(def_Data.dbtableusers))
						aSQL += EvoDB.sqlUPDATE(def_Data.dbtableusers, string.Format(EvoDB.SQL_INCREMENT, SQLColNbComments), string.Format("ID={0}", _UserID.ToString())); 
					buffer = EvoDB.RunSQL(aSQL, _SqlConnection, true); 
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

		// maybe AJAX w/ DataServer to handle DB for ratings (and comments)

#endregion 

	}
}
