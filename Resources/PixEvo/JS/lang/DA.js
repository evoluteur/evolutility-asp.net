//   Evolutility Localization Library DANISH
//   (c) 2013 Olivier Giulieri - www.evolutility.org
//   Translation from Henrik Holm

var EvolLang={

	LOCALE:"DA",    // DANISH
	
// validation
	intro:'Du er endnu ikke færdig:',
	empty:'"{0}" skal udfyldes.',
	email:'"{0}" skal være en korrekt email adresse.',
	integer:'"{0}" kun tal er tilladt.',
	decimal:'"{0}" skal være et korrekt decimal tal.',
	date:'"{0}" skal være en korrekt dato, formatet skal være "DD/MM/YYYY" eks "24/12/2005".',
	datetime:'"{0}" skal være korrekt dato/tid, formatet skal være "DD/MM/YYYY hh:mm am/pm" like "12/24/2005 10:30 am".',
	time:'"{0}" skal være korrekt dato/tid, formatet skal være "hh:mm am/pm" eks. "10:30 am".',
	max:'"{0}" skal være mindre ned eller lig med {1}.',
	min:'"{0}" skal være større end eller lig med {1}.',
	reg:'"{0}" skal matche udtryks format "{1}".',
	
// msg
	comments:'Kommentarer',
	post:'Post',
	del:'Slet {0}?',
	close:'Afslut',
	ok:'OK',
	cancel:'Fortryd',
	
// export
	xpColors:'Header farve-Farve ulige rækker-Farve lige rækker',
	xpColMap:'Columns map to',
	xpXMLroot:'Root element navn',
	xpXMLAttr:'Attributter',
	xpXMLElem:'Elementer',
	xpSQL:'SQL Options',
	xpSQLTrans:'Inside Transaction',
	xpSQLId:'Enable identity insert'

}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayIs = "Idag er ";
var thisMonth = "Idag"; // should be "this month"; // no babelfish
var dayArrShort = ['Ma','Ti','On','To','Fr','Lø','Sø'];
var dayArrMed = ['Man','Tir','Ons','Tor','Fre','Lør','Søn'];
//var dayArrLong = ['Søndag','Mandag','Tirsdag','Onsdag','Torsdag','Fredag','Lørdag'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','Maj','Jun','Jul','Aug','Sep','Okt','Nov','Dec'];
var monthArrMed = ['Jan','Feb','Mar','Apr','Maj','June','Juli','Aug','Sept','Okt','Nov','Dec'];
var monthArrLong = ['Januar','Februar','Marts','April','Maj','Juni','Juli','August','September','Oktober','November','December'];
