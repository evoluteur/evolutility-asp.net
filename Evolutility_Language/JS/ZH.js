//   Evolutility Localization Library CHINESE SIMPLIFIED
//   (c) 2013 Olivier Giulieri - www.evolutility.org
//   Translation by Sam Zhou

var EvolLang={

    LOCALE:"ZH",    // CHINESE (SIMPLIFIED) - CHS 

    // validation
    intro: '无法为您保存：',//'You are not finished yet:',
    empty: '"{0}"不能为空。', //'"{0}" must have a value.',
    email: '{0}必须是合法的邮箱地址。',//'"{0}" must be a valid email.',
    integer: '{0}必须填入数字。',//'"{0}" must only use numbers.',
    decimal: '{0}必须填入十进制小数。',//'"{0}" must be a valid decimal numbers.',
    date: '{0}必须是一个合法的日期，日期的格式必须是"YYYY-MM-DD"如："2010-12-24"。',//'"{0}" must be a valid date, format must be "MM/DD/YYYY" like "12/24/2005".',
    datetime: '{0}必须是一个合法的日期时间，时间的格式必须是"YYYY-MM-DD hh:mm:ss"如："2010-12-24 18:12:55"。',//'"{0}" must be a valid date/time, format must be "MM/DD/YYYY hh:mm am/pm" like "12/24/2005 10:30 am".',
    time: '{0}必须是一个合法的时间，时间的格式必须是"hh:mm:ss"如："18:12:55"。',//'"{0}" must be a valid date/time, format must be "hh:mm am/pm" like "10:30 am".',
    max: '{0}必须小于或等于{1}。',//'"{0}" must be smaller or equal to {1}.',
    min: '{0}必须大于或等于{1}。',//'"{0}" must be greater or equal to {1}.',
    reg: '{0}必须能通过正则表达式{1}验证。',//'"{0}" must match the regular expression pattern "{1}".',

    // msg
    comments:'我的批注',// 'My comments',
    post: '发布',//'Post',
    del: '确定要删除这条记录{0}', //'Delete this {0}?',
    close: '关闭', // 'Close',
    ok: '确认', //'OK',
    cancel: '取消', //'Cancel',

    // export
    xpColors: '标题行颜色-奇数行颜色-偶数行颜色',//'Header color-Color odd rows-Color even rows',
    xpColMap: '列映射到',//'Columns map to',
    xpXMLroot: 'XML根元素名称',//'Root element name',
    xpXMLAttr: '属性',//'Attributes',
    xpXMLElem: '元素',//'Elements',
    xpSQL: 'SQL选项',//'SQL Options',
    xpSQLTrans: '事务方式',//'Inside Transaction',
    xpSQLId: '允许标识插入' //'Enable identity insert'

}

// date picker
var defaultDateFormat = "mdy";
var weekBegin = 0, weekEnd = 6;
var todayIs = '今天是'; //"Today is ";
var thisMonth = "这个月"; // babelfish
var dayArrShort = ['日', '一', '二', '三', '四', '五', '六']; //['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'];
var dayArrMed = ['周日', '周一', '周二', '周三', '周四', '周五', '周六']; //['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
//var dayArrLong = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
//var monthArrShort = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
var monthArrMed = ['一月', '二月', '三月', '四月', '五月', '六月','七月', '八月', '九月', '十月', '十一月', '十二月']; //['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
var monthArrLong = ['一月', '二月', '三月', '四月', '五月', '六月','七月', '八月', '九月', '十月', '十一月', '十二月']; //['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
