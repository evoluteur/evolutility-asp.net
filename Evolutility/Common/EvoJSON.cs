//	Copyright (c) 2011 Olivier Giulieri - olivier@evolutility.org 

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

namespace Evolutility
{
	// ==================   JSON (JavaScript Object Notation)   ==================   
	/* 
	This library is a dependency of : 
	 * Evolutility.UIServer 
	*/

	static class EvoJSON
	{

//### Constants ######################################################################################## 
#region "Constants"

		static internal string propString = ",{0}:'{1}'"; 
		static internal string propInt = ",{0}:{1}"; 
		
#endregion


//### JSON ########################################################################################### 
#region "JSON"

		static internal string StringProp(String Name, String Value)
		{
			return String.Format(propString, Name, JSONEncode(Value));
		}

		static internal string IntProp(String Name, String Value)
		{
			if (EvoTC.isInteger(Value))
				return String.Format(propInt, Name, Value);
			return String.Empty;
		}

		static internal string JSONEncode(String Value)
		{
			if(string.IsNullOrEmpty(Value))
				return string.Empty;
			else
				return HttpUtility.HtmlEncode(Value.Replace("\n\r", "\\n").Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
		}

#endregion 
	
	}

}