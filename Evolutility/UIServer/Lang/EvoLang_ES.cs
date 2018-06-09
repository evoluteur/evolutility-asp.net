//	SPANISH translation from Gilberto Botaro

//	Copyright (c) 2003-2009 Olivier Giulieri - olivier@evolutility.org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.Text;

namespace Evolutility
{
	static partial class EvoLang // SPANISH translation from Gilberto Botaro
	{
		static internal void SetLocale_ES(string LanguageKey)
		{
			if (_LocaleCode != "ES")
			{
				_LocaleCode = "ES";
				_LocaleEN = "Spanish"; // do not translate this line - this is the english name for the language
				_Locale = "Español";

				Welcome = "Bienvenido {0}"; //{0}=login 

				entity = "entidad";
				entities = "entidades";
				AllEntities = "Todos los {0}"; // {0}=entidades

				InsertEntity = "insertar nueva {0}."; // {0}=entidades
				ModifyEntity = "modificar {0}."; // {0}=entidades
				DownloadEntity = "Descargar {0}"; // {0}=entidades
				NoEntity = "No hay entidade encontrada."; // no {0}=entitdade b/c en panel de detalles

				// --- export --- 
				ExportEntity = "Exportar esta {0}"; // {0} = entidad 
				ExportHeader = "Encabezado";
				ExportSeparator = "separador";
				ExportFirstLine = "Primera línea de nombres de campo";
				ExportFormat = "Formato de exportación";
				ExportFields = "Campos a incluir en la exportación";
				IDkey = "ID (clave primaria)";
				AllFields = "Todos los campos"; // babelfish
				ExportFormats = "Separados por comas (CSV, TXT, XLS...)-HTML- INSERT SQL-Valores separados por tabuladores (TXT)-XML";

				// --- errors & warnings --- 
				err_NoPermission = "No se le permite";
				err_NoDataDisp = "No hay datos para mostrar";
				err_NoData = "No se dispone de datos";
				err_NoQuery = "No se puede ejecutar la consulta de bases de datos";
				err_Update = "No se puede actualizar {0} #{1}."; // {0}=entidad {1}=ID 
				err_Delete = "No se puede eliminar {0} #{1}."; // {0}=entidad {1}=ID 
				MHValidValue = "{0} debe tener un valor válido."; // {0}=campo 
				NA = "N/A";
				NoUpload = "No se puede cargar el archivo";
				NoUpload2 = "Sólo GIF, JPG, PNG y formatos de imagen se les permite";

				// --- status --- 
				NewSave = "Nueva {0} guardado en {1}."; // {0}=entidad {1}=ahora 
				NoUpdate = "No se actualiza la información necesaria";
				DeleteOK = "Registro # {0} eliminado en {1: t}"; // {0}=ID {1}=tiempo 
				Updated = "{0} actualizado a {1: t}"; // {0}=entidad {1}=tiempo 
				DetailsUpdate = "Información actualizada";

				// --- login --- 
				PleaseLogin = "Por favor, log in";
				Logout = "Cerrar sesión";
				Login = "Login";
				LoginB = "Login";
				Password = "Contraseña";
				InvalidLogin = "Invalid usuario/contraseña.";
				InvalidLogin2 = "Por favor, inténtalo de nuevo.";
				AddRow = "Añadir fila";
				DelRow = "Eliminar fila";
				Customize = "Personalizar";

				wPix = "con imagen";
				wDoc = "con el archivo adjunto";
				wComments = "Con los comentarios del usuario";
				yes = "Sí";
				no = "No";
				any = "Todo";
				anyof = "Cualquiera de";
				PubMine = "todos los públicos y las minas";

				// --- toolbar ---  
				View = "Ver";
				Edit = "Editar";
				New = "Nuevo";
				NewItem = "Nueva partida";
				NewUpload = "Nueva Subir";
				Search = "Buscar";
				AdvSearch = "Búsqueda avanzada";
				NewSearch = "Nueva Búsqueda";
				Selections = "Selecciones";
				Selection = "Selección";
				Export = "Exportar";
				SearchRes = "Resultados de la búsqueda";
				Delete = "Borrar";
				ListAll = "Todos";
				Print = "Imprimir";
				DeleteEntity = "Eliminar este {0}?"; // {0}=entidad 
				Back2SearchResults = "Volver a los resultados de la búsqueda";

				// --- navigation --- 
				pFirst = "Primera";
				pPrev = "Anterior";
				pNext = "Siguiente";
				pLast = "Último";

				sBefore = "Antes";
				sAfter = "Después de";

				sDateRangeLast = "en la última";
				sDateRangeNext = "en el próximo";
				sDateRangeWithin = "dentro";
				sDateRangeAny = "cualquier momento";
				sDateRange = "día | 24 horas, la semana | 1 de la semana, el mes | 1 mes y año | 1 año";
				sEquals = "Igual";

				// --- search form dropdown ---
				sStart = "Empieza con";
				sContain = "contenido";
				sFinish = "termina con";
				sIsNull = "está vacía";
				sIsNotNull = "no está vacío";
				qEquals = "iguales";
				qStart = "comienza con";
				qInList = "en la lista";
				qNot = "no";
				qWith = "con";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " comienza con \"{0}\""; //{0}= FieldValue 
				lFinish = " termina con \"{0}\""; //{0}= FieldValue 
				lContain = " contiene \"{0}\""; //{0}= FieldValue 

				opAnd = " y ";		// babelfish
				opOr = " o ";		// babelfish

				cAt = "A";
				sOn = "On";
				sOf = " de ";
				Checked = "Comprobado";
				Save = "Guardar";
				SaveAdd = "Guardar y añadir otro";
				Cancel = "Cancelar";

				// --- user comments --- 
				ucPostedOn = "Comentario publicado en {0:t}."; //{0}=tiempo
				ucPost = "Publicar sus comentarios";
				ucAdd = "Añadir sus propias observaciones";
				ucNoComments = "No hay comentarios de usuarios para esta {0} todavía"; // {0}=entidad 
				ucNb = "{0} comentarios de usuarios para este {1}."; // {0}=NB {1}=entidad 
				ucMissing = "Algunos comentarios están desaparecidos.";
				ucFrom = "De ";
				ucOn = " on ";

			}
		}
	}
}
