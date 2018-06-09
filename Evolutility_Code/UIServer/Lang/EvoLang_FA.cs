// PERSIAN (Farsi) translation from Sohail Abbasi

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
	static partial class EvoLang // PERSIAN (Farsi) - Translated by Sohail Abbasi - http://sohail.khoshfekri.com/
	{
        static internal void SetLocale_FA(string LanguageKey)
		{
			if (_LocaleCode != "FA")
			{
				_LocaleCode = "FA";
				_LocaleEN = "Persian";
				_Locale = "فارسی";

				Welcome = "{سلام {0"; //{0}=login 

				entity = "مورد";
                entities = "موارد";
                AllEntities = "همه {0}"; // {0}=entities 

                InsertEntity = "درج جدید {0}."; // {0}=entity 
                ModifyEntity = "ویرایش {0}."; // {0}=entity 
                DownloadEntity = "دریافت {0}"; // {0}=entity 
                NoEntity = "چیزی یافت نشد."; // not {0}=entity b/c of panel details 

                // --- export --- 
                ExportEntity = "صدور این {0}"; // {0}=entity 
				ExportHeader = "سربرگ";
				ExportSeparator = "جداکننده";
				ExportFirstLine = "خط اول برای نام فیلدها";
				ExportFormat = "قالب صدور";
				ExportFields = "فیلدهایی که باید صادر شوند";
				IDkey = "شناشه(کلید اصلی)";
				AllFields = "نمایش همه فیلدها";
				ExportFormats = "با کاما جدا شده (CSV, TXT, XLS...)-HTML-SQL نویسه های درج شده (SQL)- جدا شدهTab با (TXT)-XML-Javascript اشیاء (JSON)";
                
                // --- errors & warnings --- 
                err_NoPermission = "شماره اجازه ندارید ";
                err_NoDataDisp = "چیزی برای نمایش وجود ندارد.";
                err_NoData = "داده ای موجود نیست.";
                err_NoQuery = "کاوش روی پایگاه داده قابل اجرا نیست";
                err_Update = "را به روز کرد {0} #{1}  نمی توان"; // {0}=entity {1}=ID 
                err_Delete = "را حذف کرد{0} #{1}. نمی توان"; // {0}=entity {1}=ID 
                MHValidValue = "{0} نباید خالی باشد."; //{0}=field 
                NA = "ممکن نیست";
                NoUpload = "نمی توان فایل را ارسال کرد.";
                NoUpload2 = "تنها GIF, JPG, و PNG مجاز هستند.";

                // --- status --- 
                NewSave = " {0} در {1}."; // {0}=entity {1}=now 
                NoUpdate = "لازم به بروزرسانی نیست.";
                DeleteOK = "رکورد #{0} حذف شد در {1:t}."; // {0}=ID {1}=time 
                Updated = "{0} به روز رسانی شد {1:t}."; // {0}=entity {1}=time 
                DetailsUpdated = "جزئیات بروز رسانی";
                MassUpdated = "{0} {1} به روز رسانی شد در {2:t}."; // {0}=nb rec {1}=entities  {2}=time 

                // --- login --- 
                PleaseLogin = "لطفا وارد سیستم شوید.";
                Logout = "خروج";
                Login = "نام کاربری";
                LoginB = "ورود";
                Password = "کلمه عبور";
                InvalidLogin = "نام کاربری/کلمه عبور نادرست است";
                InvalidLogin2 = "لطفا دوباره سعی کنید";
                //Remember = "Remember me" 

                // --- grid --- 
                AddRow = "افزودن سطر";
                DelRow = "حذف سطر";
                Customize = "سفارشی کردن";

                // --- Search & LOVs ---
                wPix = " با عکس";
                wDoc = " با پیوست";
                wComments = "با نظر کاربران";
                yes = "بلی";
                no = "خیر";
                any = "هرچه";
                anyof = "هرکدام از";
                PubMine = "همه عمومی ها و مال خودم";
                //MyEntities = "My ~ENTITIES~" 

                // --- toolbar --- 
                View = "مشاهده";
                Edit = "ویرایش";
                // Login = "Login" 
                New = "جدید";
                NewItem = "مورد جدید";
                NewUpload = "ارسال جدید";
                Search = "جستجو";
                AdvSearch = "جستجوی پیشرفته";
                NewSearch = "جستجوی جدید";
                Selections = "انتخاب ها";
                Selection = "انتخاب";
                Export = "صدور";
                SearchRes = "نتیجه جستجو";
				Charts = "نمودار"; // googletranslate
                MassUpdate = "به روزرسانی کلی";
                Delete = "حذف";
                ListAll = "لیست همه";
                Print = "چاپ";
                DeleteEntity = "حذف این {0}?"; // {0}=entity 
                Back2SearchResults = "برگشت به نتیجه جستجو";

                // --- navigation --- 
                pFirst = "اولی";
                pPrev = "قبلی";
                pNext = "بعدی";
                pLast = "آخری";
                
				// --- search form dropdown ---
				sBefore = "قبل از";
				sAfter = "پس از";
				sEquals = "برابراست";
				sStart = "آغاز می شود در";
				sContain = "شامل";
				sFinish = "پایان می پذیرد در";
				sIsNull = "خالی است";
				sIsNotNull = "خالی نیست";
				sDateRangeLast = " در آخر ";
				sDateRangeNext = " بعد از ";
				sDateRangeWithin = " درمیان ";
				sDateRangeAny = " هرزمانی ";
				sDateRange = "روز|24 ساعت,هفته|1 هفته,ماه|1 ماه,سال|1 سال";

				qEquals = " برابراست ";
				qStart = " آغاز می شود در ";
				qInList = " در لیست ";
				qNot = " غیر ";
				qWith = " با ";

                // --- search result conditions --- 
                lEquals = " = \"{0}\""; //{0}= FieldValue 
                lStart = " شروع می شود با \"{0}\""; //{0}= FieldValue 
                lFinish = " پایان می پذیرد در \"{0}\""; //{0}= FieldValue 
                lContain = " شامل \"{0}\""; //{0}= FieldValue 
                lIsNull = "\"{0}\" خالی است"; //{0}= FieldValue 
                lIsNotNull = "\"{0}\" خالی نیست"; //{0}= FieldValue 

                opAnd = " و ";
                opOr = " یا ";

                cAt = "در";
                sOn = "بر";
                sOf = " از ";
                Checked = "تیک خورده";
                Save = "ذخیره";
                SaveAdd = "ذخیره و افزودن یکی دیگر";
                Cancel = "لغو";
				NoX = "بدون {0}";
                NoChange = "بدون تغییر";
				NoGraph = "نمودار در دسترس نیست."; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}"; // to be reviewed

                // --- user comments --- 
                ucPostedOn = "نظر ارسال شده برای {0:t}."; //{0}=time 
                ucPost = "نظرتان را ارسال کنید";
                ucAdd = "نظر خودتان را بفرستید";
                ucNoComments = "کسی برای  {0} نظری نگذاشته است."; //{0}=entity 
                ucNb = "{0} نظر برای {1}."; //{0}=NB {1}=entity 
                ucMissing = "تعدادی از نظرات گم شده اند";
                ucFrom = "از ";
                ucOn = " بر ";

			}
		} 
	}
}
