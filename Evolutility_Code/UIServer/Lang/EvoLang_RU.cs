//	RUSSIAN translation from Konstantin Pautina

//	Copyright (c) 2003-2017 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is open source software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the open source software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed WITHOUT ANY WARRANTY;
//	without even the implied warranty of MERCHANTABILITY
//	or FITNESS FOR A PARTICULAR PURPOSE.
//	See the GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.

//  Commercial license may be purchased at www.evolutility.org <http://www.evolutility.org/product/Purchase.aspx>.


using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutility
{
	static partial class EvoLang // RUSSIAN - Translation from Konstantin Pautina 
	{
 
		static internal void SetLocale_RU(string LanguageKey)
		{
			if (_LocaleCode != "RU")
			{
				_LocaleCode = "RU";
				_LocaleEN = "Russian";
				_Locale = "Русский";

                Welcome = "Приветствуем {0}"; //{0}=login 

                entity = "запись";
                entities = "записи";
                AllEntities = "Все {0}"; // {0}=entities 

                InsertEntity = "добавить {0}."; // {0}=entity 
                ModifyEntity = "изменить {0}."; // {0}=entity 
                DownloadEntity = "Скачать {0}"; // {0}=entity 
                NoEntity = "Записей не найдено."; // not {0}=entity b/c of panel details 

                // --- export --- 
                ExportEntity = "Экспорт {0}"; // {0}=entity 
                ExportHeader = "Заголовок";
                ExportSeparator = "Разделитель";
                ExportFirstLine = "Первая строка для имен полей";
                ExportFormat = "Формат экспорта";
                ExportFields = "Поля для добавления в экспорт";
                IDkey = "ID (Первичный ключ)";
                AllFields = "Показать все поля";
                ExportFormats = "Comma separated (CSV, TXT, XLS...)-HTML-SQL Insert Statements (SQL)-Tab separated values (TXT)-XML-Javascript Object Notation (JSON)";

                // --- errors & warnings --- 
                err_NoPermission = "У вас недостаточно прав для ";
                err_NoDataDisp = "Нет данных для отображения.";
                err_NoData = "Нет данных.";
                err_NoQuery = "Невозможно выполнить запрос к Базе данных.";
                err_Update = "Невозможно обновить {0} #{1}."; // {0}=entity {1}=ID 
                err_Delete = "Невозможно удалить {0} #{1}."; // {0}=entity {1}=ID 
                MHValidValue = "Значение поля {0} должно быть корректным."; //{0}=field 
                NA = "Н/Д";
                NoUpload = "Файл загрузить невозможно.";
                NoUpload2 = "Допустимые форматы изображений: GIF, JPG, PNG.";

                // --- status --- 
                NewSave = "Новая запись {0} сохранена в {1}."; // {0}=entity {1}=now 
                NoUpdate = "Обновление не требуется.";
                DeleteOK = "Запись #{0} была удалена в {1:t}."; // {0}=ID {1}=time 
                Updated = "Запись {0} обновлена в {1:t}."; // {0}=entity {1}=time 
                DetailsUpdated = "Детали обновлены.";
                MassUpdated = "{1}:{0} записей обновлено в {2:t}."; // {0}=nb rec {1}=entities  {2}=time 

                // --- login --- 
                PleaseLogin = "Пожалуйста представьтесь.";
                Logout = "Выход";
                Login = "Логин";
                LoginB = "Вход";
                Password = "Пароль";
                InvalidLogin = "Неверные Логин/Пароль.";
                InvalidLogin2 = "Пожалуйста попробуйте еще раз.";
                //Remember = "Remember me" 

                // --- grid --- 
                AddRow = "Добавить";
                DelRow = "Удалить";
                Customize = "Настроить";

                // --- Search & LOVs ---
                wPix = " имеет изображение";
                wDoc = " имеет вложение";
                wComments = "имеет комментарии";
                yes = "Да";
                no = "Нет";
                any = "Любой";
                anyof = "Любой из";
                PubMine = "Все общие и мои";
                //MyEntities = "My ~ENTITIES~" 

                // --- toolbar --- 
                View = "Просмотр";
                Edit = "Правка";
                // Login = "Login" 
                New = "Новая(ый)";
                NewItem = "Новая Запись";
                NewUpload = "Новая Загрузка";
                Search = "Поиск";
                AdvSearch = "Продвинутый Поиск";
                NewSearch = "Новый поиск";
                Selections = "Выбранные";
                Selection = "Выбранный";
                Export = "Экспорт";
                SearchRes = "Результаты Поиска";
                MassUpdate = "Обновить Несколько";
                Delete = "Удалить";
                ListAll = "Показать Все";
                Print = "Печать";
                DeleteEntity = "Удалить эту запись {0}?"; // {0}=entity 
                Back2SearchResults = "Назад к результатам поиска";

                // --- navigation --- 
                pFirst = "Первый";
                pPrev = "Пред.";
                pNext = "След.";
                pLast = "Последний";

                sBefore = "До";
                sAfter = "После";

                sDateRangeLast = " в последнем ";
                sDateRangeNext = " в следующем ";
                sDateRangeWithin = " между ";
                sDateRangeAny = " любое время ";
                sDateRange = "день|24 часа,неделя|1 неделя,месяц|1 месяц,год|1 год";
                sEquals = "Равно";

                // --- search form dropdown ---
                sStart = "Начинается с";
                sContain = "Содержит";
                sFinish = "Оканчивается на";
                sIsNull = "пусто";
                sIsNotNull = "Не пусто";
                qEquals = " равно ";
                qStart = " начинается с ";
                qInList = " находится в ";
                qNot = " не ";
                qWith = " с ";

                // --- search result conditions --- 
                lEquals = " = \"{0}\""; //{0}= FieldValue 
                lStart = " начинается с \"{0}\""; //{0}= FieldValue 
                lFinish = " оканчивается на \"{0}\""; //{0}= FieldValue 
                lContain = " содержит \"{0}\""; //{0}= FieldValue 
                lIsNull = "\"{0}\" пусто"; //{0}= FieldValue 
                lIsNotNull = "\"{0}\" не пусто"; //{0}= FieldValue 

                opAnd = " и ";
                opOr = " или ";

                cAt = "От";
                sOn = "На";
                sOf = " из ";
                Checked = "Отмечено";
                Save = "Сохранить";
                SaveAdd = "Сохранить и добавить еще";
                Cancel = "Отмена";
                NoChange = "Без изменений";
                NoX = "Не {0}";

                // --- user comments --- 
                ucPostedOn = "Комментарии были опубликованы в {0:t}."; //{0}=time 
                ucPost = "Опубликовать комментарий";
                ucAdd = "Добавьте ваш комментарий";
                ucNoComments = "Комментариев для этой записи {0} пока нет."; //{0}=entity 
                ucNb = "{0} комментарий(ев) для этой записи {1}."; //{0}=NB {1}=entity 
                ucMissing = "Некоторые комментарии пропущены.";
                ucFrom = "От ";
                ucOn = " на ";

			}
		} 
	
	}
}
