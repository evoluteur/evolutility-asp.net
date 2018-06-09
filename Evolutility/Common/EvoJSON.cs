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
using System.Text;
using System.Web;

namespace Evolutility
{
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