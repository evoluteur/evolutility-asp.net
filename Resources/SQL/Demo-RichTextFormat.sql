/* Rich Text Format demo database   */

/* www.evolutility.org - (c) 2009 Olivier Giulieri  */
/*
	This file is part of Evolutility CRUD Framework.
	Source link <http://www.evolutility.org/download/download.aspx>

	Evolutility is free software: you can redistribute it and/or modify
	it under the terms of the GNU Affero General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Evolutility is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU Affero General Public License
	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.
*/
 

CREATE TABLE [RTF_Sample](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[RichText] [nvarchar](max) NULL,
 CONSTRAINT [PK_RTF_Sample] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO RTF_Sample (Title, RichText)  VALUES ('With Colors', '<p><span style="background-color: #99cc00"><span style="font-size: x-large"><strong>Hello </strong></span></span><span style="font-size: x-large"><strong><span style="color: #ffff00"><span style="background-color: #0000ff">World</span></span></strong><span style="background-color: #ccffff"><strong> <span style="color: #ff0000">!</span><span style="color: #00ff00">!</span></strong><span style="color: #ffff00"><strong>! </strong></span></span></span></p>'); 
INSERT INTO RTF_Sample (Title, RichText)  VALUES ('With Pictures', '<p><strong>Evolutility is hosted on</strong> <img alt="" src="http://sflogo.sourceforge.net/sflogo.php?group_id=225915&amp;type=4" /></p>'); 
 
GO
  