//   Evolutility Localization Library HINDI
//   (c) 2013 Olivier Giulieri - www.evolutility.org
//   Translation from P.K.Agarwal -  http://www.nrldc.org/

var EvolLang={

	LOCALE:"HI",    // HINDI
	
// validation
	intro:'कार्य अभी पूरा नहीं हुआ है।:',  
	empty:'{0} खाली नहीं हो सकता।',
	email:'"{0}" एक मान्य ईमेल होनी चाहिए।".',
	integer:'"{0}" केवल नंबर का उपयोग करें।', 
	decimal:'"{0}" एक मान्य दशमलव संख्या होनी चाहिये।',
	date:'"{0}" एक मान्य दिनांक होनी चाहिये, प्रारूप "MM/DD/YYYY" उदाहरणतः "12/24/2005".',
	datetime:'"{0}" एक मान्य दिनांक/समय होना चाहिये, प्रारूप "MM/DD/YYYY hh:mm am/pm, उदाहरणतः "24/12/2005 10:30 am"', 
	time:'"{0}" एक मान्य समय होना चाहिये, प्रारूप "hh:mm am/pm, उदाहरणतः "10:30 am"',
	max:'"{0}" की योग्यता {1} के बराबर या कम होनी चाहिये।',
	min:'"{0}" की योग्यता {1} के बराबर या ज्यादा होनी चाहिये।',
	reg:'"{0}" नियमित अभिव्यक्ति पैटर्न से मैच होना चाहिये। "{1}".',
	
// msg
	comments:'मेरी टिप्पणीयाँ',
	post:'पोस्ट',
	del:'इसे  मिटायें {0}?',			
	close:'बन्द करें?',
	ok:'ठीक है',
	cancel:'रद्द करें',

// export
	xpColors:'हैडर कलर-कलर विषम पंक्तियाँ-कलर सम पंक्तियाँ',
	xpColMap:'कॉलम प्रतिचित्रित',
	xpXMLroot:'रूट तत्व का नाम',
	xpXMLAttr:'गुण',
	xpXMLElem:'तत्व',
	xpSQL:'SQL विकल्प',
	xpSQLTrans:'गतिविधि के तहत',
	xpSQLId:'आईडेन्टिटी ईन्सर्ट सक्षमता।'
	
}

// date picker
var defaultDateFormat = "dmy";  // valid values are "mdy", "dmy", and "ymd"
var weekBegin=1,weekEnd=0;
var todayIs = "आज है ";
var thisMonth = "इस मास";
var dayArrShort = ['र','सो','म','बु','बृ','शु','श'];
var dayArrMed = ['रवि','सोम','मंग','बुध','बृह','शुक्र','शनि'];
//var dayArrLong = ['Lundi','Mardi','Mercredi','Jeudi','Vendredi','Samedi','Dimanche'];
//var monthArrShort = ['Jan', 'Fev', 'Mar', 'Avr', 'Mai', 'Jun', 'Jui', 'Aou', 'Sep', 'Oct', 'Nov', 'Dec'];
var monthArrMed = ['जन', 'फर', 'मार्च', 'अप्र', 'मई', 'जून', 'जुला', 'अग', 'सैप', 'अक्टू', 'नव', 'दिस'];
var monthArrLong = ['जनवरी', 'फरवरी', 'मार्च', 'अप्रैल', 'मई', 'जून', 'जुलाई', 'अगस्त', 'सैपटैम्बर', 'अक्टूबर', 'नवम्बर', 'दिसम्बर'];
