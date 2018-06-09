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

using System.Web.UI.Design;
using System.Text;

namespace Evolutility
{

	public class WizardDesigner : ControlDesigner
	{

		public override string GetDesignTimeHtml()
		{
			StringBuilder myHTML = new StringBuilder();
			Wizard ctl = (Wizard)this.Component;

			myHTML.Append("<div class=\"evo\" style=\"padding:5px;font:normal 12 Verdana\"><b>Evolutility.Wizard</b> \"");
			myHTML.Append(ctl.ID);
			myHTML.Append("\" (No rendering at design-time)</div>");

			return myHTML.ToString(); 
		}

	}
}