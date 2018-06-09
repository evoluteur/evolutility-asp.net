//   Evolutility Localization Library SPANISH
//   (c) 2011 Olivier Giulieri - www.evolutility.org
//   Translation by Gilberto Botaro

var EvolLang={

	LOCALE:"SP",    // SPANISH
	
// validation
	intro:'Usted no se ha terminado aún::',
	empty:'"{0}" debe tener un valor.',
	email:'"{0}" debe ser dirección de correo electrónico válida.',
	integer:'"{0}" debe utilizar sólo números.',
	decimal:'"{0}" debe ser un número decimal válido.',
	date:'"{0}" debe ser una fecha válida, el formato debe ser "DD/MM/YYYY" como "24/12/2005".',
	datetime:'"{0}" debe ser válida una fecha/hora, el formato debe ser "DD/MM/YYYY hh:mm am/pm" como "24/12/2005 10:30 am".',
	time:'"{0}" debe ser válida una fecha/hora, el formato debe ser "hh:mm am/pm" como "10:30 am".',
	max:'"{0}" debe ser menor o igual a {1}.',
	min:'"{0}" debe ser mayor o igual a {1}.',
	reg:'"{0}" debe coincidir con el patrón "{1}".',

// msg
	comments:'Mis comentarios',
	post:'Post',
	del:'Eliminar este {0}?',
	close:'Cerrar',
	ok:'OK',
	cancel:'Cancelar',

// export
	xpColors:'Color de la cabecera-Y color de las filas impares-Y color de las filas pares',
	xpColMap:'Mapa de las columnas',
	xpXMLroot:'Nombre de elemento raíz',
	xpXMLAttr:'Atributos',
	xpXMLElem:'Elementos',
	xpSQL:'SQL',
	xpSQLTrans:'Dentro de las opciones de transacción',
	xpSQLId:'Permitir insertar la identidad'
	
}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayIs = "Hoy es ";
var thisMonth = "Fecha actual";
var dayArrShort = ['Lu','Ma','Mi','Ju','Vi','Sa','Do'];
var dayArrMed = ['Lun','Mar','Mie','Jue','Vie','Sab','Dom'];
//var dayArrLong = ['Domingo','Lunes','Martes','Miercoles','Jueves','Viernes','Sabado'];
//var monthArrShort = ['Ene','Feb','Mar','Abr','May','Jun','Jul','Ago','Sep','Oct','Nov','Dic'];
var monthArrMed = ['Ene','Feb','Mar','Abr','May','Jun','Jul','Ago','Sep','Oct','Nov','Dic'];
var monthArrLong = [ 'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre '];
