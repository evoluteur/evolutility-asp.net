//	CHINESE (simplified) translation from Sam Zhou

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
using System.Collections.Generic;
using System.Text;

namespace Evolutility
{
	static partial class EvoLang // Simplified Chinese translation from Sam Zhou
	{
        static internal void SetLocale_CHS(string LanguageKey)
        {
            if (_LocaleCode != "ZH" && _LocaleCode != "CHS") // CHS
            {
				_LocaleCode = "ZH";
                _LocaleEN = "Chinese (simplified)";
                _Locale = "汉语";

                Welcome = "欢迎{0}"; //"Welcome {0}"; //{0}=login

                entity = "记录";//"item";
                entities = "记录";//"items";
                AllEntities = "所有 {0}";// "All {0}"; // {0}=entities 

                InsertEntity = "插入新记录{0}"; //"insert new {0}."; // {0}=entity 
                ModifyEntity = "修改{0}";// "modify {0}."; // {0}=entity 
                DownloadEntity = "下载{0}"; //"Download {0}"; // {0}=entity 
                NoEntity = "没有找到记录。"; //"No item found."; // not {0}=entity b/c of panel details 

                // --- export --- 
                ExportEntity = "导出此{0}"; //"Export this {0}"; // {0}=entity 
                ExportHeader = "首行";// "Header";
                ExportSeparator = "分割符号";//"Separator";
                ExportFirstLine = "首行取字段名称";//"First line for field names";
                ExportFormat = "输出文件格式";//"Export Format";
                ExportFields = "哪些字段将被导出";//"Fields to include in the export";
                IDkey = "ID（主键）"; // "ID (Primary Key)";
                ExportFormats ="逗号分隔(CSV, TXT, XLS...)-HTML-SQL插入语句 (SQL)-Tab 分隔 (TXT)-XML-JSON";
				// "Comma separated (CSV, TXT, XLS...)-HTML-SQL Insert Statements (SQL)-Tab separated values (TXT)-XML";
                AllFields = "显示所有字段";// "Show all fields";

                // --- errors & warnings --- 
                err_NoPermission = "你不允许"; //"You are not allowed to ";
                err_NoDataDisp = "没有数据显示"; //"No Data to display.";
                err_NoData = "没有可用数据"; //"No data available.";
                err_NoQuery = "不能执行数据库查询。"; // "Cannot execute Database query.";
                err_Update = "不能更新#{1}{0}记录。"; // "Cannot update {0} #{1}."; // {0}=entity {1}=ID  
                err_Delete = "不能删除#{1}{0}记录。"; //"Cannot delete {0} #{1}."; // {0}=entity {1}=ID 
                MHValidValue = "{0}必须设置合法的值。";//"{0} must have a valid value."; //{0}=field 
                NA = "N/A";
                NoUpload = "不能上传文件。";//"Cannot upload file.";
                NoUpload2 = "只允许上传GIF,JPG,PNG等图片格式。";//"Only GIF, JPG, and PNG image formats are allowed.";

                // --- status --- 
                NewSave = "新的{0}记录在{1}被保存。"; //"New {0} saved at {1}."; // {0}=entity {1}=now 
                NoUpdate = "不需要更新。"; // "No update necessary.";
                DeleteOK = "新的{0}记录在{1:t}被删除。"; //"Record #{0} deleted at {1:t}."; // {0}=ID {1}=time 
                Updated = "新的{0}记录在{1:t}被更新。"; //"{0} updated at {1:t}."; // {0}=entity {1}=time 
				DetailsUpdated = "从表记录被更新。"; //"Details updated.";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

                // --- login --- 
                PleaseLogin = "请登录。"; //"Please log in.";
                Logout = "注销"; //"Logout";
                Login = "登录"; //"Login";
                LoginB = "登录"; //"Login";
                Password = "口令"; //"Password";
                InvalidLogin = "不正确的用户/口令。"; //"Invalid Login/Password.";
                InvalidLogin2 = "请再试。"; //"Please, try again.";
                //Remember = "Remember me" 

                // --- grid --- 
                AddRow = "新增子项"; //"Add row";
                DelRow = "删除子项"; //"Delete row";
                Customize = "定制"; // "Customize";

                // --- Search & LOVs ---
                wPix = "包含图片"; //" with picture";
                wDoc = "包含附件"; //" with attachment";
                wComments = "包含用户批注"; //"With User comments";
                yes = "是"; //"Yes";
                no = "不是"; //"No";
                any = "任何"; //"Any";
                anyof = "任何之一"; //"Any of";
                PubMine = "我输入的或者公开的"; //"All public and mine";
                //MyEntities = "My ~ENTITIES~" 

                // --- toolbar --- 
                View = "查看"; //"View";
                Edit = "编辑"; //"Edit";
                // Login = "Login" 
                New = "新增"; //"New";
                NewItem = "新增记录"; //"New Item";
                NewUpload = "新上传"; //"New Upload";
                Search = "查找"; //"Search";
                NewSearch = "新查询"; //"New Search";
                Selections = "预定义查询";//"Selections";
                Selection = "预定义查询";//"Selection";
				Export = "导出"; //"Export";
				SearchRes = "查询结果"; //"Search Result";
				Charts = "图表"; // "Charts" - googletranslate
				MassUpdate = "大规模更新"; // "Mass Update" - googletranslate
                Delete = "删除"; //"Delete";
                ListAll = "列出所有"; //"List All";
                Print = "打印"; //"Print";
                DeleteEntity = "你要删除记录{0}"; //"Delete this {0}?"; // {0}=entity 
                Back2SearchResults = "回到查询结果集"; //"Back to search results";


                // --- navigation --- 
                pFirst = "首条记录"; // "First";
                pPrev = "上一记录"; //"Previous";
                pNext = "下一记录"; //"Next";
                pLast = "末条记录"; //"Last";
                sBefore = "之前"; //"Before";
                sAfter = "之后"; //"After";


                sDateRangeLast = " 在上 ";//" in the last "; 
                sDateRangeNext = " 在下 ";//" in the next ";
                sDateRangeWithin = " 在 ";//" within ";
                sDateRangeAny = " 任何时间 ";//" any time ";
                sDateRange = "day|24 小时,week|1 周,month|1 月,year|1 年";//"day|24 hours,week|1 week,month|1 month,year|1 year";
                sEquals = "等于"; //"Equals";

                // --- search form dropdown ---
                sStart = "开头是"; //"Starts with";
                sContain = "包含"; //"Contains";
                sFinish = "结尾是"; //"Finishes with";
                sIsNull = "没有设置"; //"Is empty" ;
                sIsNotNull = "不为空"; //"Is not empty"; 
                qEquals = " 等于"; //" equals ";
                qStart = "开头是"; //" starts with ";
                qInList = "在列表中"; //" in list ";
                qNot = "不是"; //" not ";
                qWith = "包含"; //" with ";

                // --- search result conditions --- 
                lEquals = " = \"{0}\""; //{0}= FieldValue 
                lStart = " 以\"{0}\"开头";// " starts with \"{0}\""; //{0}= FieldValue 
                lFinish = " 以\"{0}\"结尾";//  " finishes with \"{0}\""; //{0}= FieldValue 
                lContain = " 包含{0}"; //" contains \"{0}\""; //{0}= FieldValue 
                lIsNull = "\"{0}\" 为空"; //"\"{0}\" is empty"; //{0}= FieldValue 
                lIsNotNull = "\"{0}\" 不为空"; //"\"{0}\" is not empty"; //{0}= FieldValue 

                opAnd = "并且"; //" and ";
                opOr = "或";//" or ";

                cAt ="在";// "At";
                sOn ="在";//  "On";
                sOf = "在";// " of ";
                Checked = "选中"; //"Checked";
                Save = "保存"; //"Save";
                SaveAdd = "保存并继续新增"; //"Save and Add Another";
				Cancel = "取消"; //"Cancel";
				NoX = "没有{0}"; // googletranslate
				NoChange = "没有变化"; //"No Change"; // googletranslate
				NoGraph = "没有可用的图形。"; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}"; // to be reviewed

                // --- user comments --- 
                ucPostedOn = "批注";// "Comments posted on {0:t}."; //{0}=time 
                ucPost = "添加您的批注";//"Post your comments";
                ucAdd ="添加自己批注";// "Add your own comments";
                ucNoComments = "这条记录（{0}）没有任何批注";//"No user comments for this {0} yet."; //{0}=entity 
                ucNb = "用户{0} 对 {1}记录的批注。";//"{0} user comments for this {1}."; //{0}=NB {1}=entity 
                ucMissing ="部分注释丢失。";// "Some comments are missing.";
                ucFrom = "从";//"From ";
                ucOn = "于";//" on ";

                
            }
            
        }
    }
}
