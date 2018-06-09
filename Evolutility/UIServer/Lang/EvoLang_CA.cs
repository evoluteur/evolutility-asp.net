//	CATALAN - Translation from Oscar Benadi

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
	static partial class EvoLang // CATALAN - Translation from Oscar Benadi
	{
		static internal void SetLocale_CA(string LanguageKey)
		{
			if (_LocaleCode != "CA")
			{
				_LocaleCode = "CA";
				_LocaleEN = "Catalan"; // do not translate this line - this is the english name for the language
				_Locale = "Catala";

				Welcome = "Benvingut {0}"; //{0}=login 

				entity = "entitat";
				entities = "entitats";
				AllEntities = "Tots els {0}"; // {0}=entidades

				InsertEntity = "inserir nova {0}."; // {0}=entidades
				ModifyEntity = "modificar {0}."; // {0}=entidades
				DownloadEntity = "Descarregar {0}"; // {0}=entidades
				NoEntity = "No s'han trobat entitats."; // no {0}=entitdade b/c en panel de detalles

				// --- export --- 
				ExportEntity = "Exportar aquesta {0}"; // {0} = entidad 
				ExportHeader = "Capçalera";
				ExportSeparator = "separador";
				ExportFirstLine = "Primera línea de noms de camp";
				ExportFormat = "Format d'exportació";
				ExportFields = "Camps a incluoure en l'exportació";
				IDkey = "ID (clau primària)";
				AllFields = "Tots els camps"; // googletranslate
				ExportFormats = "Separats per comes (CSV, TXT, XLS...)-HTML-INSERT SQL-Valors separats per tabuladors (TXT)-XML-JSON";

				// --- errors & warnings --- 
				err_NoPermission = "No t'està permès";
				err_NoDataDisp = "No hi ha dades a mostrar";
				err_NoData = "No es disposa de dades";
				err_NoQuery = "No es pot executar la consulta de base de dades";
				err_Update = "No es pot actualitzar {0} #{1}."; // {0}=entidad {1}=ID 
				err_Delete = "No es pot eliminar {0} #{1}."; // {0}=entidad {1}=ID 
				MHValidValue = "{0} ha de tenir un valor vàlid."; // {0}=campo 
				NA = "N/A";
				NoUpload = "No es pot carregar l'arxiu";
				NoUpload2 = "Només es permeten GIF, JPG, PNG i formats d'imatge";

				// --- status --- 
				NewSave = "Nova {0} guardada en {1}."; // {0}=entidad {1}=ahora 
				NoUpdate = "No s'actualiza la informació necessària";
				DeleteOK = "Registre # {0} eliminat en {1: t}"; // {0}=ID {1}=tiempo 
				Updated = "{0} actualitzat a {1: t}"; // {0}=entidad {1}=tiempo 
				DetailsUpdated = "Informació actualitzada";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

				// --- login --- 
				PleaseLogin = "Si us plau, valida't";
				Logout = "Tancar sessió";
				Login = "Validar-se";
				LoginB = "Validar-se";
				Password = "Contrasenya";
				InvalidLogin = "Usuari/clau no vàlida.";
				InvalidLogin2 = "Si us plau, intenta-ho de nou.";

				// --- grid --- 
				AddRow = "Afegir fila";
				DelRow = "Eliminar fila";
				Customize = "Personalitzar";

				// --- Search & LOVs ---
				wPix = "amb imatge";
				wDoc = "amb l'arxiu adjunt";
				wComments = "Amb els comentaris del usuari";
				yes = "Sí";
				no = "No";
				any = "Tots";
				anyof = "Qualsevol de";
				PubMine = "tots els públics i les meves";

				// --- toolbar --- 
				View = "Veure";
				Edit = "Editar";
				New = "Nou";
				NewItem = "Nou element";
				NewUpload = "Nova Pujada";
				Search = "Cercar";
				AdvSearch = "Cerca avançada";
				NewSearch = "Nova Cerca";
				Selections = "Seleccions";
				Selection = "Selecció";
				Export = "Exportar";
				Charts = "Charts"; // "Charts" MUST BE TRANSLATED
				SearchRes = "Resultats de la cerca";
				Charts = "Gràfics";// googletranslate
				MassUpdate = "Missa d'actualització"; //"Mass Update" googletranslate
				Delete = "Borrar";
				ListAll = "Tots";
				Print = "Imprimir";
				DeleteEntity = "Eliminar aquest {0}?"; // {0}=entidad 
				Back2SearchResults = "Tornar als resultats de la cerca";

				// --- navigation --- 
				pFirst = "Primer";
				pPrev = "Anterior";
				pNext = "Següent";
				pLast = "Últim";

				sBefore = "Abans";
				sAfter = "Després de";

				sDateRangeLast = "en l'última";
				sDateRangeNext = "en el pròxim";
				sDateRangeWithin = "dintre";
				sDateRangeAny = "qualsevol moment";
				sDateRange = "dia|24 hores,la setmana|1 de la setmana,el mes|1 mes i any|1 any";
				sEquals = "Igual";

				// --- search form dropdown ---
				sStart = "Comença amb";
				sContain = "contingut";
				sFinish = "acaba amb";
				sIsNull = "está vacía";
				sIsNotNull = "no está vacío";
				qEquals = "iguals";
				qStart = "comença amb";
				qInList = "en la lista";
				qNot = "no";
				qWith = "amb";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " comença amb \"{0}\""; //{0}= FieldValue 
				lFinish = " acaba amb \"{0}\""; //{0}= FieldValue 
				lContain = " conté \"{0}\""; //{0}= FieldValue 
				lIsNull = " está vacía \"{0}\"";
				lIsNotNull = " no está vacío \"{0}\"";

				opAnd = " i ";
				opOr = " o ";

				cAt = "A";
				sOn = "On";
				sOf = " de ";
				Checked = "Comprovat";
				Save = "Guardar";
				SaveAdd = "Guardar i afegir un altre";
				Cancel = "Cancel·lar";
				NoX = "Sense {0}"; // googletranslate
				NoChange = "Sense canvi"; //"No Change" googletranslate
				NoGraph = "No hi ha gràfics disponibles."; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}";

				// --- user comments --- 
				ucPostedOn = "Comentari publicat en {0:t}."; //{0}=tiempo
				ucPost = "Publicar els comentaris propis";
				ucAdd = "Afegir les observaciones pròpies";
				ucNoComments = "No hi ha comentaris d'usuaris per aquesta {0} encara"; // {0}=entidad 
				ucNb = "{0} comentaris d'usuaris per aquesta {1}."; // {0}=NB {1}=entidad 
				ucMissing = "Alguns comentaris han desaparegut .";
				ucFrom = "De ";
				ucOn = " on ";

			}
		}
	}
}

