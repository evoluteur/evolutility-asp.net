//   Evolutility Localization Library JAPANESE
//   www.evolutility.org - (c) 2009 Olivier Giulieri
//   Translation by Kazue Watanabe

var EvolLang={

	LOCALE:"JP",    // JAPANESE
	
// validation	
	intro:'未だ終了できません:',
	empty:'"{0}"　空間の部分を記入してください.',
	email:'"{0}" メールアドレスを記入して下さい.',
	integer:'"{0}" 数字のみ記入して下さい.',
	decimal:'"{0}" 小数点以下の数字を記入してください.',
	date:'"{0}" 正しい日付を "月/日/年" の順番に記入してください。例 "12/24/2005".',
	datetime:'"{0}" 正しい日付と時間を "月/日/年　　何時:何分　午前/午後" の順番に記入してください。例 "12/24/2005 10:30 am".',
	time:'"{0}" 正しい日付と時間を "月/日/年　　何時:何分　午前/午後" の順番に記入してください。例 "10:30 am".',
	max:'"{0}" {1}を含む１以下の数字を記入して下さい.',
	min:'"{0}" {1}を含む１以上の数字を記入して下さい.',
	reg:'"{0}" must match the regular expression pattern "{1}".',
	
// msg
	comments:'私のコメント',
	post:'掲載',
	del:'この{0}を削除しますか?',			
	close:'閉じる',
	ok:'OK',
	cancel:'キャンセル',
	
// export
	xpColors:'ヘッダ 色-色 奇数の行-色 偶数の行',
	xpColMap:'マップするコラム',
	xpXMLroot:'ルート要素の名前',
	xpXMLAttr:'特性',
	xpXMLElem:'要素',
	xpSQL:'SQLのオプション',
	xpSQLTrans:'中身 取引',
	xpSQLId:'可能 アイデンティテ 挿入'
	 
}
 
// date picker
var defaultDateFormat = "dmy";  // valid values are "mdy", "dmy", and "ymd"
var weekBegin=1,weekEnd=0;
var todayIs = "今日 "; // babelfish
var thisMonth = "この月"; // babelfish
var dayArrShort = ['月','火','水','木','金','土','日'];
var dayArrMed = ['月','火','水','木','金','土','日'];
//var dayArrLong = ['日曜日','月曜日','火曜日','水曜日','木曜日','金曜日','土曜日'];
//var monthArrShort = ['一','二','三','四','五','六','七','八','九','十','十一','十二'];
var monthArrMed = ['一','二','三','四','五','六','七','八','九','十','十一','十二'];
var monthArrLong = ['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'];
