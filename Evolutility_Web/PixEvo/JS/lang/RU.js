//   Evolutility Localization Library RUSSIAN
//   Translation by Konstantin Pautina
//   www.evolutility.org - (c) 2018 Olivier Giulieri


var EvolLang={

	LOCALE:"RU",    // RUSSIAN
	
// validation
	intro:'Вы еще не закончили:',
	empty:'Поле "{0}" должно иметь значение.',
	email:'Поле "{0}" должно соответствовать адресу email.',
	integer:'Поле "{0}" должно быть целым числом.',
	decimal:'Поле "{0}" должно быть числом.',
	date:'Поле "{0}" должно быть датой в формате "ДД/ММ/ГГГГ", например "24/12/2005".',
	datetime:'Поле "{0}" должно быть датой/временем в формате "ДД/ММ/ГГГГ чч:мм am/pm", например "24/12/2005 10:30 am".',
	time:'Поле "{0}" должно быть временем в формате "чч:мм am/pm", например "10:30 am".',
	max:'Поле "{0}" должно быть меньше или равно {1}.',
	min:'Поле "{0}" должно быть больше или равно {1}.',
	reg:'Поле "{0}" должно соответствовать рег. выражению "{1}".',
	
// msg
	comments:'Мои комментарии',
	post:'Опубликовать',
	del:'Удалить этот {0}?',
	close:'Закрыть',
	ok:'OK',
	cancel:'Отмена',
	
// export
	xpColors:'Цвет заголовка-Цвет четных строк-Цвет нечетных строк',
	xpColMap:'Столбцы соответствуют',
	xpXMLroot:'Имя корневого элемента',
	xpXMLAttr:'Аттрибуты',
	xpXMLElem:'Элементы',
	xpSQL:'Опции SQL',
	xpSQLTrans:'Внутри Транзакции',
	xpSQLId:'Включить вставку ID'

}	

// date picker
var defaultDateFormat = "dmy";
var weekBegin=1,weekEnd=0;
var todayIs = "Сегодня ";
var thisMonth = "Этот месяц";
var dayArrShort = ['Вс','Пн','Вт','Ср','Чт','Пт','Сб'];
var dayArrMed = ['Вс.','Пн.','Вт.','Ср.','Чт.','Пт.','Сб.'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['Янв','Февр','Март','Апр','Май','Июнь','Июль','Авг','Сент','Окт','Нояб','Дек'];
var monthArrLong = ['Январь','Февраль','Март','Апрель','Май','Июнь','Июль','Август','Сентябрь','Октябрь','Ноябрь','Декабрь'];
