//   Evolutility Localization Library CATALAN
//   www.evolutility.org - (c) 2009 Olivier Giulieri
//   Translation by Oscar Benadi

var EvolLang={

	LOCALE:"CA",    // CATALA
	
// validation
	intro:'Encara no has acabat::',
	empty:'"{0}" ha de tenir valor.',
	email:'"{0}" ha de ser una adreça de correu electrònic vàlida.',
	integer:'"{0}" ha d\'utilitzar només números.',
	decimal:'"{0}" ha de ser un número decimal vàlid.',
	date:'"{0}" ha de ser una data vàlida, el format ha de ser "DD/MM/YYYY" com "24/12/2005".',
	datetime:'"{0}" ha de ser una data/hora vàlida, el format ha de ser "DD/MM/YYYY hh:mm am/pm" com "24/12/2005 10:30 am".',
	time:'"{0}" ha de ser una hora vàlida, el format ha de ser "hh:mm am/pm" com "10:30 am".',
	max:'"{0}" ha de ser menor o igual a {1}.',
	min:'"{0}" ha de ser major o igual a {1}.',
	reg:'"{0}" ha de coincidir amb el patrón d\'expressió regular "{1}".',

// msg
	comments:'Els meus comentaris',
	post:'Post',
	del:'Eliminar aquest {0}?',
	close:'Tancar',
	ok:'OK',
	cancel:'Cancel·lar',

// export
	xpColors:'Color de la capçalera-Color files imparells-Color files parells',
	xpColMap:'Mapa de les columnes',
	xpXMLroot:'Nombre d\'elements arrel',
	xpXMLAttr:'Atributs',
	xpXMLElem:'Elements',
	xpSQL:'SQL',
	xpSQLTrans:'Dins de les opcions de transacció',
	xpSQLId:'Permetre inserir l\'entitat'
	
}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayIs = "Avui és ";
var thisMonth = "This month"; // no babelfish
var dayArrShort = ['Dl','Dm','Dx','Dj','Dv','Ds','Dg'];
var dayArrMed = ['Dl','Dm','Dx','Dj','Dv','Ds','Dg'];
//var dayArrLong = ['','Lunes','Martes','Miercoles','Jueves','Viernes','Sabado'];
//var monthArrShort = ['Ene','Feb','Mar','Abr','May','Jun','Jul','Ago','Sep','Oct','Nov','Dic'];
var monthArrMed = ['Gen','Feb','Mar','Abr','Mai','Jun','Jul','Ago','Set','Oct','Nov','Dec'];
var monthArrLong = [ 'Gener', 'Febrer', 'Març', 'Abril', 'Maig', 'Juny', 'Juliol', 'Agost', 'Setembre', 'Octubre', 'Novembre', 'Desembre '];
