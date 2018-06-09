//   Evolutility Localization Library FRENCH
//   (c) 2011 Olivier Giulieri - www.evolutility.org
//   Translation from Eddy Boels - http://www.ed-design.org/

var EvolLang={

	LOCALE:"FR",    // FRENCH
	
// validation
	intro:'Vous n\'avez pas encore termin\351:',  
	empty:'Vous devez entrer une valeur pour le champs {0}.',
	email:'"{0}" doit avoir la forme "identifiant@domaine.com".',
	integer:'"{0}" doit \351tre un nombre entier.', 
	decimal:'"{0}" doit \351tre un nombre d\351cimal.',
	date:'"{0}" doit s\'\351crire sous la forme Jour/Mois/Ann\351e, par exemple: 24/12/2005.',
	datetime:'"{0}" doit s\'\351crire sous la forme Jour/Mois/Ann\351e heure:minutes am/pm, par exemple : 24/12/2005 10:30 am.', 
	time:'"{0}" doit s\'\351crire sous la forme heure:minutes am/pm, par exemple : 10:30 am.',
	max:'"{0}" doit \352tre inf\351rieur ou \351gal \340 {1}.',
	min:'"{0}" doit \352tre sup\351rieur ou \351gal \340 {1}.',
	reg:'"{0}" doit v\351rifier l\'expression r\351guli\232;re "{1}".',
	
// msg
	comments:'Mes commentaires',
	post:'Envoyer',
	del:'Supprimer ce {0}?',			
	close:'Fermer',
	ok:'OK',
	cancel:'Annuler',

// export
	xpColors:'Couleur des titres-Couleur lignes impaires-Couleur lignes paires',
	xpColMap:'Colonnes connectées à',
	xpXMLroot:'Nom de l\'élément racine',
	xpXMLAttr:'Attributs',
	xpXMLElem:'Eléments',
	xpSQL:'SQL Options',
	xpSQLTrans:'Utiliser transaction',
	xpSQLId:'Forcer IDs'
	
}

// date picker
var defaultDateFormat = "dmy";  // valid values are "mdy", "dmy", and "ymd"
var weekBegin=1,weekEnd=0;
var todayIs = "Aujourd'hui est ";
var thisMonth = "Ce mois-ci";
var dayArrShort = ['Lu','Ma','Me','Je','Ve','Sa','Di'];
var dayArrMed = ['Lun','Mar','Mer','Jeu','Ven','Sam','Dim'];
//var dayArrLong = ['Lundi','Mardi','Mercredi','Jeudi','Vendredi','Samedi','Dimanche'];
//var monthArrShort = ['Jan', 'Fev', 'Mar', 'Avr', 'Mai', 'Jun', 'Jui', 'Aou', 'Sep', 'Oct', 'Nov', 'Dec'];
var monthArrMed = ['Jan', 'Fev', 'Mar', 'Avr', 'Mai', 'Juin', 'Juil', 'Aout', 'Sept', 'Oct', 'Nov', 'Dec'];
var monthArrLong = ['Janvier', 'Fevrier', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Aout', 'Septembre', 'Octobre', 'Novembre', 'Decembre'];
