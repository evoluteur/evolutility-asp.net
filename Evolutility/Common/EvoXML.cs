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


using System.Xml;

namespace Evolutility
{
	// ==================   XML - constants for element and attribute names   ==================   
	/* 
	This library is a dependency of : 
	 * Evolutility.UIServer 
	 * Evolutility.Wizard 
	*/

	static class xElement
	{
		/// <summary>Elements for Evolutility metadata.</summary>

		internal const string form = "form";
		internal const string data = "data";
		internal const string tab = "tab";
		internal const string panel = "panel";
		internal const string field = "field";

	}

	static class xAttribute
	{
		/// <summary>Attributes for Evolutility metadata.</summary>
		
		internal const string id = "id";

		internal const string dbTable = "dbtable";
		internal const string dbWhere = "dbwhere";
		internal const string dbOrder = "dborder";

		internal const string type = "type";
		internal const string icon = "icon";
		internal const string help = "help";
		internal const string jsValidation = "jsvalidation";
		internal const string jsDependency = "jsdependency";
		internal const string dependency = "dependency";
		internal const string dbName = "name";
		internal const string dbColumn = "dbcolumn";
		internal const string dbColumnRead = "dbcolumnread";
		//internal const string dbColumnImg = "dbcolumnimg";
		internal const string dbColumnIcon = "dbcolumnicon";
		internal const string dbReadOnly = "readonly";
		internal const string required = "required";
		internal const string regExp = "regexp";
		internal const string min = "min", max = "max";
		internal const string maxLength = "maxlength";
		internal const string optional = "optional";
		internal const string url = "url";

		internal const string dbTableLOV = "dbtablelov";
		internal const string dbColumnReadLOV = "dbcolumnreadlov";
		internal const string dbWhereLOV = "dbwherelov";
		internal const string dbOrderLOV = "dborderlov";
		internal const string lovMany = "lovmany";
		internal const string lovEnumeration = "lovenumeration";
		internal const string lovSPlist = "splistlov";

		internal const string label = "label";
		internal const string labelEdit = "labeledit";
		internal const string labelList = "labellist";
		internal const string imgList = "imglist";
		internal const string format = "format";
		internal const string link = "link";
		internal const string linkTarget = "linktarget";
		internal const string linkLabel = "linklabel";
		internal const string cssClass = "cssclass";
		internal const string cssClassView = "cssclassview";
		internal const string cssClassLabel = "cssclasslabel";
		internal const string height = "height";
		internal const string width = "width";
		internal const string img = "img";
		internal const string search = "search";
		internal const string searchAdv = "searchadv";
		internal const string searchList = "searchlist";

		internal const string dbTableDetails = "dbtabledetails";
		internal const string dbColumnDetails = "dbcolumndetails";
		internal const string dbMaxRows = "dbmaxrows";
 
		public static string GetFieldLabel(XmlNode FieldNode)
		{
			string fieldLabel = FieldNode.Attributes[xAttribute.label].Value;
			if (string.IsNullOrEmpty(fieldLabel))
			{
				if (FieldNode.Attributes[xAttribute.labelEdit] != null)
					fieldLabel = FieldNode.Attributes[xAttribute.labelEdit].Value;
				if (string.IsNullOrEmpty(fieldLabel) && (FieldNode.Attributes[xAttribute.labelList] != null))
					fieldLabel = FieldNode.Attributes[xAttribute.labelList].Value;
			}
			return fieldLabel;
		}
 
		public static int GetFieldMaxLength(XmlNode FieldNode)
		{
			if (FieldNode.Attributes[xAttribute.maxLength] == null)
				return 0;
			else
				return EvoTC.String2Int(FieldNode.Attributes[xAttribute.maxLength].Value); 
		}
	}

	static class xQuery
	{
		/// <summary>Common XPath queries.</summary> 
		internal const string evoNameSpace = "http://www.evolutility.com";
		internal const string XMLHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

		internal const string tab = "//evo:tab";
		internal const string panel = "//evo:panel";
		internal const string panelField = "//evo:panel/evo:field";
		internal const string aggregableFields = "//evo:panel/evo:field[@type='boolean' or @type='lov']";
		internal const string panelDetails = "//evo:panel-details";
		internal const string panelDetailsField = "//evo:panel-details/evo:field";
		internal const string data = "//evo:form/evo:data";
		internal const string query = "//evo:queries/evo:query";
		internal const string queries = "//evo:queries";

		static internal string qPanelFields(string attribute)
		{
			return string.Format("{0}[@{1}>0]", panelField, attribute);
		}

		static internal string qEquals(string element, string attribute, string equalTo)
		{
			return string.Format("{0}[@{1}='{2}']", element, attribute, equalTo);
		}

	}
}
