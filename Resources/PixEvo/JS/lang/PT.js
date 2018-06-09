//   Evolutility Localization Library PORTUGUESE
//   (c) 2011 Olivier Giulieri - www.evolutility.org
//   Translation by Gilberto Botaro

var EvolLang={

	LOCALE:"PT",    // PORTUGUESE
	
// validation
	intro:'A operação não foi finalizada:',
	empty:'"{0}" deve conter um valor.',
	email:'"{0}" deve ser um email válido.',
	integer:'"{0}" deve usar somente números.',
	decimal:'"{0}" deve ser um valor decimal válido.',
	date:'"{0}" deve ser uma data válida, formato deve ser "DD/MM/YYYY" tal como "25/10/2005".',
	datetime:'"{0}" deve ser uma data/hora válida, formato deve ser "DD/MM/YYYY hh:mm am/pm" tal como "25/10/2005 10:30 am".',
	time:'"{0}" deve ser uma hora válida, formato deve ser "hh:mm am/pm" tal como "10:30 am".',
	max:'"{0}" deve ser menor ou igual a {1}.',
	min:'"{0}" deve ser maior ou igual a {1}.',
	reg:'"{0}" deve corresponder com o padrão de expressão regular "{1}".',
	
// msg
	comments:'Mis comentarios',
	post:'Post',
	del:'Excluir este {0}?',
	close:'Fechar',
	ok:'Ok',
	cancel:'Cancela',
	
// export
	xpColors: 'Cor do título-Cor linhas Impares-Cor linhas Pares',
	xpColMap:'Mapear colunas para',
	xpXMLroot:'Nome elemento Raiz',
	xpXMLAttr:'Atributos',
	xpXMLElem:'Elementos',
	xpSQL:'SQL',
	xpSQLTrans:'Dentro das opções de transações',
	xpSQLId:'Permitir inserir a identidade'

}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayIs = "Hoje é ";
var thisMonth = "Este mês";
var dayArrShort = ['Se','Te','Qa','Qi','Se','Sa','Do'];
var dayArrMed = ['Seg','Ter','Qua','Qui','Sex','Sab','Dom'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['Jan','Fev','Mar','Abr','Mai','Jun','Jul','Ago','Set','Out','Nov','Dez'];
var monthArrLong = ['Janeiro','Fevereiro','Março','Abril','Maio','Junho','Julho','Agosto','Setembro','Outubro','Novembro','Dezembro'];
