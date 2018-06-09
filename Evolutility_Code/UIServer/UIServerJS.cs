//	Copyright (c) 2003-2013 Olivier Giulieri - olivier@evolutility.org 

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
using System.Text;
using System.Web;
using System.Xml;

namespace Evolutility
{
	//   ==================   JavaScript   ==================   
	// generate custom JS based on metadata, register scripts

	partial class UIServer 
	{

		private bool JSDetailsDone = false;
		private StringBuilder genJS;
		private bool JSinDetails;

//### JavaScript ###################################################################################### 
#region "JavaScript"

		private string JSFields2Validate()
		{
			/// <summary>fields validation for edit.</summary>
			/// <remarks>rules = required + formats email, date... + image upload.</remarks>
			StringBuilder allFields = new StringBuilder();
			bool NeedValidation = false;

			XmlNodeList aNodeList = myDOM.DocumentElement.SelectNodes(xQuery.panelField, nsManager);
			allFields.Append("[");
			int maxLoop = aNodeList.Count;
			for (int i = 0; i < maxLoop; i++)
			{
				XmlNode cn = aNodeList[i];
				//--- not ReadOnly
				if (cn.Attributes[xAttribute.dbReadOnly] != null)
					NeedValidation = JSFieldNeedValidation(cn.Attributes[xAttribute.dbReadOnly].Value);
				else
					NeedValidation = true;
				if (NeedValidation)
				{
					StringBuilder aField = new StringBuilder();
					String fieldType = cn.Attributes[xAttribute.type].Value;
					String fieldLabel = xAttribute.GetFieldLabel(cn);
					if (fieldLabel.IndexOf("'") > -1)
						fieldLabel = fieldLabel.Replace("'", "\\'");
					if (fieldType != EvoDB.t_bool)
					{
						//--- Required
						if (cn.Attributes[xAttribute.required] == null)
							NeedValidation = false;
						else
							NeedValidation = cn.Attributes[xAttribute.required].Value == s1;
						if (NeedValidation)
							aField.Append(",r:1");
					}
					//        //--- Custom validation 
					//        //If Not .Attributes(Attr.Script) Is Nothing Then 
					//        // aField.Append(",c:").Append(.Attributes(Attr.Script).Value) 
					//        // myScript.Append("if(").Append(.Attributes(Attr.Script).Value.Replace("@fieldid", """" & fieldID & """").Replace("@fieldlabel", """" & fieldLabel & """")).Append(")return false;") 
					//        //End If 
					//--- Type Validation (after b/c null OK)
					switch (fieldType)
					{
						case EvoDB.t_date:// to force type validation
						case EvoDB.t_time:
						case EvoDB.t_datetime:
						case EvoDB.t_email:
						case EvoDB.t_html:// to flag for rich text editor
							aField.Append(" ");	
							break;
						case EvoDB.t_int:
						case EvoDB.t_dec:
							//min value
							if (cn.Attributes[xAttribute.min] != null)
								aField.Append(EvoJSON.IntProp(xAttribute.min, cn.Attributes[xAttribute.min].Value));
							//max value
							if (cn.Attributes[xAttribute.max] != null)
								aField.Append(EvoJSON.IntProp(xAttribute.max, cn.Attributes[xAttribute.max].Value));
							break;
					}
					//--- Dependant drop downs
					if (cn.Attributes[xAttribute.jsDependency] != null)
						aField.Append(EvoJSON.StringProp("jsdep", cn.Attributes[xAttribute.jsDependency].Value));
					if (cn.Attributes[xAttribute.dependency] != null)
						aField.Append(EvoJSON.StringProp("dep", cn.Attributes[xAttribute.dependency].Value));
					//--- Custom validation
					if (cn.Attributes[xAttribute.jsValidation] != null)
						aField.Append(EvoJSON.StringProp("jsv", cn.Attributes[xAttribute.jsValidation].Value));
					if (cn.Attributes[xAttribute.regExp] != null)
						aField.Append(EvoJSON.StringProp("rg", cn.Attributes[xAttribute.regExp].Value.Replace("\\", "\\\\")));
					if (aField.Length > 0)
					{
						String fieldID = cn.Attributes[xAttribute.dbColumn].Value;
						allFields.Append("\n{").AppendFormat("id:'{0}',l:'{1}',t:'{2}'", fieldID, fieldLabel, fieldType);
						//if (listAllFields)
						//{
						//    if (cn.Attributes[xAttribute.search] != null && Convert.ToInt32(cn.Attributes[xAttribute.search].Value) > 0)
						//        allFields.Append(",s:1");
						//    if (cn.Attributes[xAttribute.searchAdv] != null && Convert.ToInt32(cn.Attributes[xAttribute.searchAdv].Value) > 0)
						//        allFields.Append(",sa:1");
						//    if (fieldType == EvoDB.t_lov)
						//        allFields.Append(",\nlst:").Append(HTMLlov(cn, "", s0, LOVFormat.JSON, 0));
						//    else
						//    {
						//        int ml = xAttribute.GetFieldMaxLength(cn);
						//        if (ml > 0)
						//            allFields.AppendFormat(",ml:{0}", ml);
						//    }
						//}
						allFields.Append(aField).Append("},");
					}
				}
			}
			if (allFields.Length > 3)
				allFields.Remove(allFields.Length - 1, 1);
			allFields.Append("]");
			return allFields.ToString();
		}

		private string JSEditDetails(XmlNodeList aNodeList, int gridID)
		{
			/// <summary>Generates JS for grid details editing.</summary>
			/// <remarks>Can be used several times for several detail grids on the same form.</remarks>
			StringBuilder js2 = new StringBuilder();
			bool fieldReadOnly = false;

			//already checked _DBAllowUpdateDetails and _DBAllowInsertDetails before calling function ! 
			js2.AppendFormat("\n{0}:", gridID).Append("{flds:[ ");
			nbFieldEditable = 0; 	//nbFieldEditable is a global variable 
			int nbFields = aNodeList.Count;
			for (int i = 0; i < nbFields; i++)
			{
				XmlNode cn = aNodeList[i];
				if (cn.NodeType == XmlNodeType.Element)
				{ 
					if (cn.Attributes[xAttribute.dbReadOnly] == null)
						fieldReadOnly = false;
					else
						fieldReadOnly = EvoTC.String2Int(cn.Attributes[xAttribute.dbReadOnly].Value) > 0;
					if (!fieldReadOnly)
					{
						nbFieldEditable += 1;
						String fieldType = cn.Attributes[xAttribute.type].Value;
						js2.Append("{").AppendFormat("i:{0},t:'{1}'", i + 1, fieldType);
						if (cn.Attributes[xAttribute.required] != null)
							js2.AppendFormat(",r:{0}", EvoTC.String2Int(cn.Attributes[xAttribute.required].Value));
						switch (fieldType)
						{
							case EvoDB.t_lov:
								js2.AppendFormat(",lov:{0}", HTMLlov(cn, String.Empty, s0, LOVFormat.JSON, 0));
								break;
							case EvoDB.t_bool:
								String fieldFormat = EvoUI.PixCheck;
								if (cn.Attributes[xAttribute.img] != null)
									fieldFormat = cn.Attributes[xAttribute.img].Value.Replace("'", "\\'");
								if (String.IsNullOrEmpty(fieldFormat))
									fieldFormat = EvoUI.PixCheck;
								js2.AppendFormat(",pix:'{0}'", fieldFormat);
								break;
							case EvoDB.t_text:
							case EvoDB.t_txtm:
							case EvoDB.t_email:
							case EvoDB.t_html:
							case EvoDB.t_url:
								int ml = xAttribute.GetFieldMaxLength(cn);
								if (ml > 0)
									js2.AppendFormat(",ml:{0}", ml);
								break;
						}
						js2.Append("},");
					}
				}
			}
			js2.Remove(js2.Length - 1, 1);
			js2.Append("]},");
			JSDetailsDone = (nbFieldEditable > 0);
			return js2.ToString();
		}

		private bool JSFieldNeedValidation(String dbReadOnly)
		{
			/// <summary>Determines if field needs client JS validation.</summary>
			if (dbReadOnly == s1)
				return false;
			else if (dbReadOnly == s2)
				return _ItemID < 1;
			else
				return true;
		}

		private void JSRegisterScripts(string id)
		{
			/// <summary>Registers necessary Scripts depending on current mode.</summary>
			if (_DisplayMode == 1)
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "nicEdit", string.Format("<script src=\"{0}nicEdit/nicEdit.js\" type=\"text/javascript\"></script>", _PathPixToolbar));
			if (_ShowDesigner)
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "EvoDico", string.Format("<script src=\"{0}JS/EvoDico.js\" defer=\"defer\" type=\"text/javascript\"></script>", _PathPixToolbar));
			if (!(def_Data == null || string.IsNullOrEmpty(def_Data.js_script)))
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "EvoCustom", string.Format("<script src=\"{0}{1}\" defer=\"defer\" type=\"text/javascript\"></script>", _PathPixToolbar, def_Data.js_script));
			StringBuilder sbJS = new StringBuilder();
			sbJS.Append(EvoUI.JSIncludeEvoScripts(_PathPixToolbar, _Language));
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Evolutility", sbJS.ToString());
			genJS = new StringBuilder();
			if (XMLloaded)
			{
				genJS.Append("\n\nEvPost=function(EvEvent){").Append(JSPostBack("%$#@").Replace("'%$#@'", "EvEvent")).Append("};\n");
				genJS.Append("if(typeof Evol=='undefined'){alert('Evolutility Javascript library is invalid or not found.')}else{EvoUI.adEvent(window,'load',Evol.setup)};\n");
				genJS.Append("EvoGen={id:'").Append(id).Append("',entity:'").Append(def_Data.entity.Replace("'", "\\'"));
				if (_DisplayMode == 70) //Export
					genJS.Append("',entities:'").Append(def_Data.entities.Replace("'", "\\'"));
				genJS.AppendFormat("',path:'{0}'", _PathPixToolbar.Replace("'", "\\'"));
				genJS.AppendFormat(",lang:'{0}'",  _Language);
				if (_DBAllowHelp && _DisplayMode > 0 && _DisplayMode < 5)
					genJS.AppendFormat(",\nformid:'{0}'", _XMLfile.Replace("'", "\\'"));
				if (_DisplayMode == 1)
				{
					genJS.AppendFormat(",\nfields:{0}", JSFields2Validate());
					genJS.Append(",\ndetails:{lst:{ "); // need 1 space here
					JSinDetails = true;
				}
				// genJS.Append("}};");   //STAYS UNCLOSED, flagged by JSinDetails
			}
		}

		private void JSWrite(string js)
		{
			Page.Response.Write(EvoUI.JSscript);
			Page.Response.Write(js);
			Page.Response.Write(EvoUI.JSscriptClose);
		}

		private string JSPostBack(string EventParam)
		{
			return Page.ClientScript.GetPostBackEventReference(this, EventParam);
		}

#endregion

	}
}
