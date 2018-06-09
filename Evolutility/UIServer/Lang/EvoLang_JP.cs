//	JAPANESE translation from Kazue Watanabe

//	Copyright (c) 2003-2011 Olivier Giulieri - olivier@evolutility.org 

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
	static partial class EvoLang // JAPANESE - Translation from Kazue Watanabe
	{
		static internal void SetLocale_JP(string LanguageKey)
		{
			if (_LocaleCode != "JP")
			{
				_LocaleCode = "JP";
				_LocaleEN = "Japanese"; // do not translate this line - this is the english name for the language
				_Locale = "日本語";

				Welcome = "ようこそ{0}"; //{0}=login

				entity = "項目";
				entities = "項目";
				AllEntities = "すべての{0}"; // {0}=entities 

				InsertEntity = "新しい{0}を挿入してください。"; // {0}=entity 
				ModifyEntity = "{0}を修正してください."; // {0}=entity 
				DownloadEntity = "{0}をダウンロード"; // {0}=entity 
				NoEntity = "項目がみつかりません。"; // not {0}=entity b/c of panel details 

				// --- export --- 
				ExportEntity = "この{0}をエクスポートしてください。"; // {0}=entity 
				ExportHeader = "ヘッダ";
				ExportSeparator = "セパレータ";
				ExportFirstLine = "フィールド名の最初の行";
				ExportFormat = "エクスポートするフォーマット";
				ExportFields = "エクスポートに含めるフィールド";
				IDkey = "ID (Primary Key)";
				AllFields = "すべての分野"; // babelfish
				ExportFormats = "コンマで区分けされた(CSV, TXT, XLS...)-HTML-SQL ステートメントを挿入する(SQL)-タブ separated values (TXT)-XML-JSON";

				// --- errors & warnings --- 
				err_NoPermission = "許可されておりません。";
				err_NoDataDisp = "表示するデータはありません。";
				err_NoData = "データがありません。";
				err_NoQuery = "データベースクエリを実行することができません。";
				err_Update = "{0}をアップデートすることができません。 #{1}."; // {0}=entity {1}=ID 
				err_Delete = "{0} #{1}を削除することができません。."; // {0}=entity {1}=ID 
				MHValidValue = "{0}は有効な値をもっていなければいけません。"; //{0}=field 
				NA = "N/A";
				NoUpload = "ファイルをアップロードすることができません。";
				NoUpload2 = "GIF, JPG, とPNGだけの画像フォーマットだけが許可されております。";

				// --- status --- 
				NewSave = "{1}に保存された新しい{0} ."; // {0}=entity {1}=now 
				NoUpdate = "アップデートは必要とされません。";
				DeleteOK = "{1:t}でレコード　#{0}が削除されました。."; // {0}=ID {1}=time 
				Updated = "{1:t}で　{0}がアップデートされました。."; // {0}=entity {1}=time 
				DetailsUpdated = "詳細がアップデートされました";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

				// --- login --- 
				PleaseLogin = "ログインしてください。";
				Logout = "ログアウト";
				Login = "ログイン";
				LoginB = "ログイン";
				Password = "パスワード";
				InvalidLogin = "無効なログイン/パスワード";
				InvalidLogin2 = "もう一度お試しください。";
				//Remember = "記憶してください" 

				// --- grid --- 
				AddRow = "行を加える";
				DelRow = "行を削除する";
				Customize = "カスタマイズ";

				// --- Search & LOVs ---
				wPix = "画像付き";
				wDoc = "添付ファイル付き";
				wComments = "ユーザコメント付き";
				yes = "はい";
				no = "いいえ";
				any = "どれも";
				anyof = "のどれも";
				PubMine = "すべての公のものとわたしのもの";
				//MyEntities = "わたしの ~項目~" 

				// --- toolbar --- 
				View = "表示";
				Edit = "編集";
				// Login = "ログイン" 
				New = "新しい";
				NewItem = "新項目";
				NewUpload = "新規アップロード";
				Search = "検索";
				AdvSearch = "高度検索";
				NewSearch = "新規検索";
				Selections = "選択";
				Selection = "選択";
				Export = "エクスポート";
				SearchRes = "検索結果";
				Charts = "チャート"; // googletranslate
				MassUpdate = "大量更新"; // googletranslate
				Delete = "削除";
				ListAll = "全てを列挙";
				Print = "印刷";
				DeleteEntity = "この{0}を削除しますか?"; // {0}=entity 
				Back2SearchResults = "検索結果に戻る";

				// --- navigation --- 
				pFirst = "最初";
				pPrev = "前";
				pNext = "次";
				pLast = "最後";

				sBefore = "前";
				sAfter = "後";

				sDateRangeLast = " 最後の ";
				sDateRangeNext = " 次の ";
				sDateRangeWithin = " 以内 ";
				sDateRangeAny = " 随時 ";
				sDateRange = "日|２４時間,週|1 週,月|1 月,年|1 年";
				sEquals = "等しい";

				// --- search form dropdown --- 
				sStart = "で始まる。";
				sContain = "含む";
				sFinish = "で終わる";
				sIsNull = "空である";
				sIsNotNull = "空でない";
				qEquals = " 等しい ";
				qStart = " で始まる ";
				qInList = " リストにある ";
				qNot = " でない ";
				qWith = " と ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " \"{0}\"で始まる。"; //{0}= FieldValue 
				lFinish = "\"{0}\"で終わる。"; //{0}= FieldValue 
				lContain = "\"{0}\"を含む。"; //{0}= FieldValue 
				lIsNull = "\"{0}\"空である"; //{0}= FieldValue 
				lIsNotNull = "\"{0}\"空でない"; //{0}= FieldValue 

				opAnd = "および ";	// babelfish
				opOr = "か ";		// babelfish

				cAt = "で";
				sOn = "に";
				sOf = " の ";
				Checked = "チェック済み";
				Save = "保存";
				SaveAdd = "保存し他のものを追加しなさい。";
				Cancel = "キャンセル";
				NoX = "いいえ{0}"; // googletranslate
				NoChange = "変更なし"; //"No Change" googletranslate
				NoGraph = "利用可能なグラフはありません。"; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}"; // to be reviewed

				// --- user comments --- 
				ucPostedOn = "{0:t}に掲載されたコメント。"; //{0}=time 
				ucPost = "コメントを掲載してください。";
				ucAdd = "ご自身のコメントを掲載してください。";
				ucNoComments = "この{0}にはユーザのコメントはまだありません"; //{0}=entity 
				ucNb = " この{1}には{0}のユーザコメントがあります。."; //{0}=NB {1}=entity 
				ucMissing = "いくつかのコメントが欠けています。";
				ucFrom = "から ";
				ucOn = " に ";

			}
		}
	}
}
