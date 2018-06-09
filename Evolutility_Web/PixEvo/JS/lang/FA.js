//   Evolutility Localization Library ENGLISH
//   www.evolutility.org - (c) 2013 Olivier Giulieri
//   PERSIAN (Farsi) - Translated by Sohail Abbasi - http://sohail.khoshfekri.com/

var EvolLang={

	LOCALE:"FA",   // PERSIAN (Farsi) 
	
// validation
	intro:'هنوز کار شما نمام نشده است:',
	empty:'"{0}" باید یک مقداری داشته باشد.',
	email:'"{0}" ایمیل نادرست است.',
	integer:'"{0}" باید حتما عدد باشد.',
	decimal:'"{0}" باید یک مقدار ده دهی باشد.',
	date:'"{0}" باید به صورت تاریخ باشد, قالب باید به شکل "ماه/روز/سال" باشد مانند "12/24/2005".',
	datetime:'"{0}" باید زمان/تاریخ باشد , باید به شکل "ماه/روز/سال ساعت:دقیقه ق.ظ/ب.ظ" باشد مانند "12/24/2005 10:30 am".',
	time:'"{0}" باید به قالب زمان و تاریخ باشد, قالب مانند "ساعت:دقیقه am/pm" مانند "10:30 am".',
	max:'"{0}" باید کوچکتر یا برابر باشد با {1}.',
	min:'"{0}" باید بزرگتر یا برابر باشد با {1}.',
	reg:'"{0}" باید قالب عبارت باقاعده را رعایت کند "{1}".',
	
// msg
	comments:'نظرات من',
	post:'ارسال',
	del:'حذف {0}?',
	close:'بستن',
	ok:'تایید',
	cancel:'لغو',
	
// export
	xpColors:'سربرگ رنگ-رنگ فرد سطر-رنگ زوج سطر',
	xpColMap:'ستون ها نگاشت می شوند به',
	xpXMLroot:'نام عنصر ریشه',
	xpXMLAttr:'صفات',
	xpXMLElem:'عناصر',
	xpSQL:'گزینه های SQL',
	xpSQLTrans:'درون تراکنش',
	xpSQLId:'فعال کردن درج شناسه'

}	

// date picker
var defaultDateFormat = "ymd";
var weekBegin=0,weekEnd=6;
var todayIs = "امروز ";
var thisMonth = "این ماه";
var dayArrShort = ['یکشنبه','دوشنبه','سه شنبه','چهارشنبه','پنجشنبه','جمعه','شنبه'];
var dayArrMed = ['یکشنبه','دوشنبه','سه شنبه','چهارشنبه','پنجشنبه','جمعه','شنبه'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['ژانویه','فوریه','مارس','آوریل','می','ژوئن','جولای','آگوست','سپتامبر','اکتبر','نوامبر','دسامبر'];
var monthArrLong = ['ژانویه','فوریه','مارس','آوریل','می','ژوئن','جولای','آگوست','سپتامبر','اکتبر','نوامبر','دسامبر'];
