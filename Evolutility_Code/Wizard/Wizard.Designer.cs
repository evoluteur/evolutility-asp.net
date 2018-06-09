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