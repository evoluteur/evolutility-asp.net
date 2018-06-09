//   Evolutility Localization Library TURKISH - TÜRKÇE
//   (c) 2011 Olivier Giulieri - www.evolutility.org
//   Translation from Davut Engin

var EvolLang={

	LOCALE:"TR",    // TÜRKÇE
	
// validation
	intro:'Henüz bitirmediniz:',
	empty:'"{0}" boş bırakılamaz.',
	email:'"{0}" geçerli bir eposta adresi değil.',
	integer:'"{0}" sadece sayı içerebilir.',
	decimal:'"{0}" sadece virgüllü sayı içerebilir.',
	date:'"{0}" geçerli bir tarih içerebilir, "GG/AA/YYYY" kalıbında olmalı, yani "17/01/1981".',
	datetime:'"{0}" geçerli bir tarih/zaman içerebilir, "GG/AA/YYYY ss:dd am/pm" kalıbında olmalı, yani "17/01/1981 05:30 am".',
	time:'"{0}"  geçerli bir tarih içerebilir, "ss:dd am/pm" kalıbında olmalı, yani "05:30 am".',
	max:'"{0}", {1} dan/den küçük veya eşit olmalı.',
	min:'"{0}", {1} dan/den büyük veya eşit olmalı.',
	reg:'"{0}", "{1}" regular expressine eşit olmalı.',
	
// msg
	comments:'Yorumlarım',
	post:'Gönder',
	del:'{0} silinsin mi?',
	close:'Kapat',
	ok:'TAMAM',
	cancel:'İptal',
	
// export
	xpColors:'Başlık rengi-Tek satırların rengi-Çift satırların rengi',
	xpColMap:'Eşlenecek sütunlar',
	xpXMLroot:'Kök(root) elemanın adı',
	xpXMLAttr:'Nitelikler(Attributes)',
	xpXMLElem:'Öğeler(Elements)',
	xpSQL:'SQL Seçenekleri',
	xpSQLTrans:'İşlem içinde',
	xpSQLId:'Kimlik olarak ekleme açık'

}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=0,weekEnd=6;
var todayIs = "Bugün ";
var thisMonth = "bu ay";
var dayArrShort = ['Pz','Pt','Sl','Çr','Pr','Cu','Ct'];
var dayArrMed = ['Pz','Pzt','Sl','Çrş','Prş','Cum','Cmt'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['Ock','Şbt','Mrt','Nsn','May','Haz','Tmmz','Ağu','Eyl','Ekm','Ksm','Arl'];
var monthArrLong = ['Ocak','Şubat','Mart','Nisan','Mayıs','Haziran','Temmuz','Ağustos','Eylül','Ekim','Kasım','Aralık'];
