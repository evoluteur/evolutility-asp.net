//   Evolutility Localization Library GERMAN
//   www.evolutility.org - (c) 2009 Olivier Giulieri
//   Translation by Joachim Seidel

var EvolLang={

	LOCALE:"DE",    // GERMAN  (DEUTSCH)
	
// validation
	intro:'Sie sind noch nicht fertig:',
	empty:'"{0}" muss einen Wert besitzen.',
	email:'"{0}" muss eine korrekte eMail Adresse sein.',
	integer:'"{0}" muss numerisch sein.',
	decimal:'"{0}" muss eine Dezimalzahl sein.',
	date:'"{0}" muss ein korrektes Datum enthalten, Format: "DD.MM.JJJJ", wie "24.12.2009".',
	datetime:'"{0}" muss ein korrektes Datum/Zeit Format enthalten, Format: "DD.MM.JJJJ hh:mm", wie "24.12.2009 22:30".',
	time:'"{0}" muss eine korrekte Zeit enthalten, Format: "hh:mm", wie "22:30".',
	max:'"{0}" muss kleiner oder gleich {1} sein.',
	min:'"{0}" muss grösser oder gleich {1} sein.',
	reg:'"{0}" muss dem Regulären Ausdruck "{1}" entsprechen.',
	
// msg
	comments:'Meine Kommentare',
	post:'Senden',
	del:'Löschen von {0}?',
	close:'Schliessen',
	ok:'OK',
	cancel:'Abbrechen',
	
// export
	xpColors:'Farbe Spaltenkopf-Farbe ungerade Zeilen- Farbe gerade Zeilen',
	xpColMap:'Spalten abbilden nach',
	xpXMLroot:'Wurzel Elementname',
	xpXMLAttr:'Attribute',
	xpXMLElem:'Elemente',
	xpSQL:'SQL Optionen',
	xpSQLTrans:'Innere Transaktion',
	xpSQLId:'Einfügen von Identitäten aktivieren'

}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayName = "Heute ist ";
var thisMonth = "diesen Monat";
var dayArrShort = ['Mo','Di','Mi','Do','Fr','Sa', 'So'];
var dayArrMed = ['Mon','Die','Mit','Don','Fre','Sam', 'Son'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['Jan','Feb','Mär','Apr','Mai','Jun','Jul','Aug','Sept','Okt','Nov','Dez'];
var monthArrLong = ['Januar','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember'];
