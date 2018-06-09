//	TURKISH translation from Davut Engin

//	Copyright (c) 2003-2009 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU General Public License for more details.

//	You should have received a copy of the GNU General Public License
//	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutility
{
	static partial class EvoLang // TURKISH translation from Davut Engin
	{
				static internal void SetLocale_TR(string LanguageKey)
		{
			if (_LocaleCode != "TR")
			{
				_LocaleCode = "TR";
				_LocaleEN = "Turkish";
				_Locale = "Türkçe";

				Welcome = "Hoşgeldin {0}"; //{0}=login 

				entity = "öğe";
				entities = "öğeler";
				AllEntities = "Tüm {0}"; // {0}=entities 

				InsertEntity = "yeni {0} ekle."; // {0}=entity 
				ModifyEntity = "{0} düzenle."; // {0}=entity 
				DownloadEntity = "{0} indir"; // {0}=entity 
				NoEntity = "Hiç bir öğe bulunamadı."; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "{0} dışa aktar"; // {0}=entity 
				ExportHeader = "Başlık";
				ExportSeparator = "Ayraç";
				ExportFirstLine = "İlk satır alan isimleri için";
				ExportFormat = "Dışa aktarma kalıbı";
				ExportFields = "Dışa aktarılacak alanlar";
				IDkey = "ID (Birincil Anahtar)";
				AllFields = "Tüm alanları göster";
				ExportFormats = "Virgül ile ayrılmış (CSV, TXT, XLS...)-HTML-SQL Ekleme sorguları olarak (SQL)-Tab ile ayrılmış değerler (TXT)-XML";

				// --- errors & warnings --- 
				err_NoPermission = "İzniniz yok ";
				err_NoDataDisp = "Gösterilecek veri yok.";
				err_NoData = "Veri yok.";
				err_NoQuery = "Veritabanı sorgusu koşulamadı(execute).";
				err_Update = "#{1}. {0} güncellenemiyor"; // {0}=entity {1}=ID 
				err_Delete = "#{1}. {0} silinemiyor"; // {0}=entity {1}=ID 
				MHValidValue = "{0} geçerli bir değere sahip olmalı."; //{0}=field 
				NA = "YOK";
				NoUpload = "Dosya gönderilemedi.";
				NoUpload2 = "Sadece GIF, JPG ve PNG türünde resim gönderilebilir.";

				// --- status --- 
				NewSave = "Yeni {0}, kayıt zamanı : {1}."; // {0}=entity {1}=now 
				NoUpdate = "Güncellemeye gerek kalmadı.";
				DeleteOK = "#{0} isimli kaydın silinme zamanı : {1:t}."; // {0}=ID {1}=time 
				Updated = "{0}, güncelleme zamanı : {1:t}."; // {0}=entity {1}=time 
				DetailsUpdate = "Detaylar güncellendi.";

				// --- login --- 
				PleaseLogin = "Lütfen oturum açın.";
				Logout = "Oturumu Kapat";
				Login = "Oturum Aç";
				LoginB = "Oturum Aç";
				Password = "Şifre";
				InvalidLogin = "Hatalı KullanıcıAdı/Şifre.";
				InvalidLogin2 = "Lütfen tekrar deneyin.";
				//Remember = "Remember me" 

				// --- grid --- 
				AddRow = "Satır ekle";
				DelRow = "Satır sil";
				Customize = "Özelleştir";

				// --- Search & LOVs ---
				wPix = " resimleri içer";
				wDoc = " ek dosyaları içer";
				wComments = "With User comments";
				yes = "Evet";
				no = "Hayır";
				any = "Hepsi";
				anyof = "Seçilenlerden biri";
				PubMine = "Tüm genele açık olanlar ve benimkiler";
				//MyEntities = "My ~ENTITIES~" 

				// --- toolbar --- 
				View = "Göster";
				Edit = "Düzenle";
				// Login = "Login" 
				New = "Yeni";
				NewItem = "Yeni Öğe";
				NewUpload = "Yeni gönderi(Upload)";
				Search = "Ara";
				AdvSearch = "Gelişmiş Arama";
				NewSearch = "Yeni Arama";
				Selections = "Seçimler";
				Selection = "Seçim";
				Export = "Dışa Aktar(Export)";
				SearchRes = "Arama Sonucu";
				Delete = "Sil";
				ListAll = "Hepsini Listele";
				Print = "Yazdır";
				DeleteEntity = "{0} Silinsin mi?"; // {0}=entity 
				Back2SearchResults = "Arama sonuçlarına dön";

				// --- navigation --- 
				pFirst = "İlk";
				pPrev = "Önceki";
				pNext = "Sonraki";
				pLast = "Son";

				sBefore = "Önce";
				sAfter = "Sonra";

				sDateRangeLast = " önceki ";
				sDateRangeNext = " sonraki ";
				sDateRangeWithin = " içinde ";
				sDateRangeAny = " herhangi bir zaman ";
				sDateRange = "day|24 saat,week|1 hafta,month|1 ay,year|1 yıl";
				sEquals = "Eşittir";

				// --- search form dropdown ---
				sStart = "Bununla başlayanlar";
				sContain = "İçerecek";
				sFinish = "Bununla bitenler";
				sIsNull = "Boş ise";
				sIsNotNull = "Boş değil ise";
				qEquals = " eşit ise ";
				qStart = " başı ";
				qInList = " listeden ";
				qNot = " değil ";
				qWith = " beraber ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " \"{0}\" ile başlayanlar"; //{0}= FieldValue 
				lFinish = " \"{0}\" ile bitenler"; //{0}= FieldValue 
				lContain = " \"{0}\" içeriyor"; //{0}= FieldValue 
				lIsNull = "\"{0}\" boş"; //{0}= FieldValue 
				lIsNotNull = "\"{0}\" boş değil"; //{0}= FieldValue 

				opAnd = " ve ";
				opOr = " veya ";

				/*
				cAt,sOn,sOf not ready.
				*/
				cAt = "At";
				sOn = "On";
				sOf = " of ";
				Checked = "İşaretlenmiş";
				Save = "Kaydet";
				SaveAdd = "Kaydet ve Bir Başkasını Ekle";
				Cancel = "İptal";

				// --- user comments --- 
				ucPostedOn = "Yorumların gönderildiği zaman : {0:t}."; //{0}=time 
				ucPost = "Kendi yorumlarını gönder";
				ucAdd = "Sana ait yorumları ekle";
				ucNoComments = "{0} ile ilgili yorum henüz yapılmamış."; //{0}=entity 
				ucNb = "{1} ile ilgili {0} adet yorum yapılmış."; //{0}=NB {1}=entity 
				ucMissing = "Bazı yorumlar kayıp.";
				ucFrom = "Gönderen ";
				ucOn = " zaman : ";

			}
		} 
	}
}
