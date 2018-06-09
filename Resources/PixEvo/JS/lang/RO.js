//   Evolutility Localization Library ROMANIAN
//   (c) 2011 Olivier Giulieri - www.evolutility.org
//   Translation by Cosmin Munteanu

var EvolLang={

	LOCALE:"RO",    // ROMANIAN
	
// validation
	intro:'Încă nu ai terminat:',
	empty:'"{0}" trebuie să aibă o valoare.',
	email:'"{0}" trebuie să fie o adresă de email validă.',
	integer:'"{0}" trebuie folosite numai numere.',
	decimal:'"{0}" trebuie folosite numai numere zecimale.',
	date:'"{0}" trebuie sa fie o dată validă de forma "Ziua/Luna/Anul", de exemplu "24/12/2005".',
	datetime:'"{0}" trebuie sa fie o dată/timp valid de forma "Ziua/Luna/Anul ora:minutul am/pm", de exemplu "24/12/2005 10:30 am".',
	time:'"{0}" trebuie sa fie un timp valid de forma "ora:minutul am/pm", de exemplu "10:30 am".',
	max:'"{0}" trebuie să fie mai mic sau egal cu {1}.',
	min:'"{0}" trebuie să fie mai mare sau egal cu {1}.',
	reg:'"{0}" trebuie să se potrivească cu modelul descris de expresia regulată "{1}".',
	
// msg
	comments:'Comentariile mele',
	post:'Postare',
	del:'Şterg acest {0}?',
	close:'Închide',
	ok:'OK',
	cancel:'Anulează',
	
// export
	xpColors:'Culoarea titlului-Culoarea liniilor impare-Culoarea liniilor pare',
	xpColMap:'Coloanele se corelează cu',
	xpXMLroot:'Numele elementului rădăcină',
	xpXMLAttr:'Atribute',
	xpXMLElem:'Elemente',
	xpSQL:'SQL Opţiuni',
	xpSQLTrans:'Tranzacţie internă',
	xpSQLId:'Permite inserarea identităţii'

}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayIs = "Astăzi este ";
var thisMonth = "În această lună"; // google translate
var dayArrShort = ['Lu','Ma','Mi','Jo','Vi','Sa','Du'];
var dayArrMed = ['Lun','Mar','Mie','Joi','Vin','Sam','Dum'];
//var dayArrLong = ['Luni','Marţi','Miercuri','Joi','Vineri','Sâmbătă','Duminică'];
//var monthArrShort = ['Ian','Feb','Mar','Apr','Mai','Iun','Iul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['Ian','Feb','Mar','Apr','Mai','Iun','Iul','Aug','Sep','Oct','Nov','Dec'];
var monthArrLong = ['Ianuarie','Februarie','Martie','Aprilie','Mai','Iunie','Iulie','August','Septembrie','Octombrie','Noiembrie','Decembrie'];
