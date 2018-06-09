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
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Evolutility
{
	// ==================   Objects to store metadata, serialization, deserialization   ==================   
	// UNDER CONSTRUCTION
	/* 
	This library is a dependency of : 
	 * Evolutility.UIServer 
	 * Evolutility.Wizard 
	*/

	//<XmlRootAttribute("form")> _ 
	//public class Form
	//{
	//    public int ID;

	//    //<XmlElement("data")> _ 
	//    public Data data;
	//    public ArrayList FormElements;

	//    public Form()
	//    {
	//        FormElements = new ArrayList();
	//    }

	//    [XmlElement("queries")]
	//    public Queries queries;

	//}

	[XmlRootAttribute("data", Namespace = "http://www.evolutility.com")]
	public class Data
	{

		[XmlAttribute()]
		//required 
		public string dbtable;
		[XmlAttribute()]
		public string dbcolumnlead;
		[XmlAttribute()]
		public string dbcolumnpk;
		[XmlAttribute()]
		public string dbtablecomments;
		[XmlAttribute()]
		public string dbtableusers;
		[XmlAttribute()]
		public string dbwhere;
		[XmlAttribute()]
		public string dborder;
		[XmlAttribute()]
		public int dbcommentsformid;

		[XmlAttribute()]
		public string sppaging;
		[XmlAttribute()]
		public string spget;
		[XmlAttribute()]
		public string splogin;
		[XmlAttribute()]
		public string spdelete;

		[XmlAttribute()]
		public string icon;
		[XmlAttribute()]
		public string entity;
		[XmlAttribute()]
		public string entities;
		[XmlAttribute()]
		public string script;

		//added 
		[XmlAttribute()]
		public string title="";
		[XmlAttribute()]
		public string js_script;
		[XmlAttribute()]
		public string dblockingcolumn;
		[XmlAttribute()]
		public string dbcolumncomments;
		[XmlAttribute()]
		public string userpage;
		[XmlAttribute()]
		public string dbcolumnuserid;
		[XmlAttribute()]
		public string dbcolumnicon;

		public static Data Deserialize(XmlNode aNode)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Data));
			XmlNodeReader r = new XmlNodeReader(aNode);
			Data d = (Data)serializer.Deserialize(r);
			d.SetDefaults();
			r.Close();
			return d;
		}

		public void SetDefaults()
		{
			if (string.IsNullOrEmpty(this.title) && !string.IsNullOrEmpty(this.entity))
				this.title = EvoTC.ToUpperLowers(this.entity);
			if (string.IsNullOrEmpty(this.dbcolumnpk))
				this.dbcolumnpk = "ID";
			if (string.IsNullOrEmpty(this.dbtableusers))
				this.dbtableusers = "EVOL_USER";
			if (string.IsNullOrEmpty(this.dbtablecomments))
				this.dbtablecomments = "evol_comment";
			if (string.IsNullOrEmpty(this.dbcolumncomments))
				this.dbcolumncomments = "itemid";
			if (string.IsNullOrEmpty(this.dbcolumnuserid))
				this.dbcolumnuserid = "userID"; 
		}

	}

	////<XmlInclude(GetType(Panel)), XmlInclude(GetType(Tab)), Serializable()> _ 
	//public class Holder
	//{
	//    [XmlAttribute()]
	//    public string ID;
	//    [XmlAttribute()]
	//    public string cssclass;
	//    [XmlAttribute()]
	//    public string cssclasslabel;
	//    [XmlAttribute()]
	//    //required 
	//    public string label;
	//}

	//[Serializable()]
	//public class Tab : Holder
	//{
	//    public Hashtable Panels;
	//    public Tab()
	//    {
	//        Panels = new Hashtable();
	//    }
	//}

	//[Serializable()]
	//public class Panel : Holder
	//{
	//    public Panel()
	//    {
	//        Fields = new Hashtable();
	//        type = "";
	//    }
	//    public Panel(string nid, string nType, string nlabel, string nwidth)
	//    {
	//        ID = nid;
	//        type = nType;
	//        label = nlabel;
	//        width = nwidth;
	//        Fields = new Hashtable();
	//    }
	//    [XmlAttribute()]
	//    public string type;
	//    [XmlAttribute()]
	//    public string width;
	//    [XmlAttribute()]
	//    public string img;
	//    [XmlAttribute()]
	//    public string optional;
	//    [XmlArrayItem("field")]
	//    public Hashtable Fields;

	//    //Public Shared Function Deserialize(ByVal aNode As XmlNode) As Panel 
	//    // Dim p As Panel 
	//    // Dim serializer As New XmlSerializer(GetType(Panel)) 
	//    // Dim r As XmlNodeReader = New XmlNodeReader(aNode) 
	//    // Try 
	//    // p = CType(serializer.Deserialize(r), Panel) 
	//    // Return p 
	//    // Catch ex As Exception 
	//    // 'ErrorMsg = "Invalid XML file. The element 'field' must have attributes." 
	//    // Return Nothing 
	//    // End Try 
	//    //End Function 
	//}

	//[Serializable()]
	//public class Queries
	//{
	//    [XmlAttribute()]
	//    public string label;
	//    //<XmlArray("queries"), XmlArrayItem("query")> Public query() As Query 
	//}

	//[Serializable()]
	//public class Query
	//{
	//    [XmlAttribute()]
	//    //required 
	//    public string url;
	//    [XmlAttribute()]
	//    //required 
	//    public string label;
	//    [XmlAttribute()]
	//    //required 
	//    public string dbwhere;
	//    [XmlAttribute()]
	//    public string dborder;
	//}

	//[XmlRootAttribute("field", Namespace = "http://www.evolutility.com")]
	//public class Field
	//{
	//    private string _ID;
	//    public string ID
	//    {
	//        get
	//        {
	//            if (string.IsNullOrEmpty(_ID))
	//            {
	//                return "EVOL" + this.dbcolumn;
	//            }
	//            else
	//            {
	//                return _ID;
	//            }
	//        }
	//        set { _ID = value; }
	//    }
	//    [XmlAttribute()]
	//    public string cssclass;
	//    [XmlAttribute()]
	//    public string dbcolumn; //required 
	//    [XmlAttribute()]
	//    public string dbcolumnimg;
	//    [XmlAttribute()]
	//    public string dbcolumnicon;
	//    [XmlAttribute()]
	//    public string dbcolumnread; //required
	//    [XmlAttribute()]
	//    public string dbcolumnreadlov;
	//    [XmlAttribute()]
	//    public string dbtablelov;
	//    [XmlAttribute()]
	//    public string dborderlov;
	//    [XmlAttribute()]
	//    public string dbwherelov;
	//    [XmlAttribute()]
	//    public string splistlov;
	//    [XmlAttribute()]
	//    public string format;
	//    [XmlAttribute()]
	//    public int height; //default="1" 
	//    [XmlAttribute()]
	//    public string help;
	//    [XmlAttribute()]
	//    public string img;
	//    [XmlAttribute()]
	//    public string imglist;
	//    [XmlAttribute()]
	//    public string label; //required
	//    [XmlAttribute()]
	//    public string labeledit;
	//    [XmlAttribute()]
	//    public string labellist;
	//    [XmlAttribute()]
	//    public string link;
	//    [XmlAttribute()]
	//    public string linklabel;
	//    [XmlAttribute()]
	//    public string linktarget;
	//    [XmlAttribute()]
	//    public string lookup;
	//    [XmlAttribute()]
	//    public string lovenumeration;
	//    [XmlAttribute()]
	//    public string maxlength;
	//    [XmlAttribute()]
	//    public string max;
	//    [XmlAttribute()]
	//    public string min;
	//    [XmlAttribute()]
	//    public string optional;
	//    [XmlAttribute()]
	//    public int @readonly;
	//    [XmlAttribute()]
	//    public int required;
	//    [XmlAttribute()]
	//    public string script; //UNDOCUMENTED
	//    [XmlAttribute()]
	//    public int search;
	//    [XmlAttribute()]
	//    public int searchlist;
	//    [XmlAttribute()]
	//    public int searchadv;
	//    [XmlAttribute()]
	//    public string type;
	//    [XmlAttribute()]
	//    public int width; //default="100" 
	//    [XmlAttribute()]
	//    public string validationrule;

	//    public static Field Deserialize(XmlNode aNode)
	//    {
	//        XmlSerializer serializer = new XmlSerializer(typeof(Field));
	//        XmlNodeReader r = new XmlNodeReader(aNode);
	//        try
	//        {
	//            Field f = (Field)serializer.Deserialize(r);
	//            return f;
	//        }
	//        catch //(Exception ex)
	//        {
	//            //ErrorMsg = "Invalid XML file. The element 'field' must have attributes." 
	//            return null;
	//        }
	//        finally
	//        {
	//            r.Close();
	//        }
	//    }
	//}
}