//   Evolutility Localization Library ENGLISH
//   (c) 2013 Olivier Giulieri - www.evolutility.org


var EvolLang={

	LOCALE:"EN",    // ENGLISH
	
// validation
	intro:'You are not finished yet:',
	empty:'"{0}" must have a value.',
	email:'"{0}" must be a valid email.',
	integer:'"{0}" must only use numbers.',
	decimal:'"{0}" must be a valid decimal numbers.',
	date:'"{0}" must be a valid date, format must be "MM/DD/YYYY" like "12/24/2005".',
	datetime:'"{0}" must be a valid date/time, format must be "MM/DD/YYYY hh:mm am/pm" like "12/24/2005 10:30 am".',
	time:'"{0}" must be a valid date/time, format must be "hh:mm am/pm" like "10:30 am".',
	max:'"{0}" must be smaller or equal to {1}.',
	min:'"{0}" must be greater or equal to {1}.',
	reg:'"{0}" must match the regular expression pattern "{1}".',
	
// msg
	comments:'My comments',
	post:'Post',
	del:'Delete this {0}?',
	close:'Close',
	ok:'OK',
	cancel:'Cancel',
	
// export
	xpColors:'Header color-Color odd rows-Color even rows',
	xpColMap:'Columns map to',
	xpXMLroot:'Root element name',
	xpXMLAttr:'Attributes',
	xpXMLElem:'Elements',
	xpSQL:'SQL Options',
	xpSQLTrans:'Inside Transaction',
	xpSQLId:'Enable identity insert'

}	

// date picker
var defaultDateFormat = "mdy";
var weekBegin=0,weekEnd=6;
var todayIs = "Today is ";
var thisMonth = "This month";
var dayArrShort = ['Su','Mo','Tu','We','Th','Fr','Sa'];
var dayArrMed = ['Sun','Mon','Tue','Wed','Thu','Fri','Sat'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['Jan','Feb','Mar','Apr','May','June','July','Aug','Sept','Oct','Nov','Dec'];
var monthArrLong = ['January','February','March','April','May','June','July','August','September','October','November','December'];
