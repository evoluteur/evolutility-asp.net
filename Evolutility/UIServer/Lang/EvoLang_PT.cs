//	PORTUGUESE translation from Gilberto Botaro

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
	static partial class EvoLang // PORTUGUESE translation from Gilberto Botaro
	{
		static internal void SetLocale_PT(string LanguageKey)
		{
			if (_LocaleCode != "PT")
			{
				_LocaleCode = "PT";
				_LocaleEN = "Portuguese"; // do not translate this line - this is the english name for the language
				_Locale = "Português";

				Welcome = "Bem Vindo {0}"; //{0}=login 

				entity = "entidade";
				entities = "entidades";
				AllEntities = "Todos os {0}"; // {0}=entidades

				InsertEntity = "inserir nova {0}."; // {0}=entidades
				ModifyEntity = "modificar {0}."; // {0}=entidades
				DownloadEntity = "Baixar {0}"; // {0}=entidades
				NoEntity = "Nenhuma {0} encontrada."; // no {0}=entidade b/c em panel de detalhes

				// --- export --- 
				ExportEntity = "Exportar esta {0}"; // {0} = entidade
				ExportHeader = "Título";
				ExportSeparator = "separador";
				ExportFirstLine = "Primeira linha de nome do campo";
				ExportFormat = "Formato de exportação";
				ExportFields = "Campos a incluir na exportação";
				IDkey = "ID (chave primária)";
				AllFields = "Todos os campos"; // babelfish
				ExportFormats = "separados por virgula (CSV, TXT, XLS...)-HTML-INSERT SQL (SQL)-Valores separados por tabulação (TXT)-XML-JSON";

				// --- errors & warnings --- 
				err_NoPermission = "Não é permitido";
				err_NoDataDisp = "Não há dados para exibir";
				err_NoData = "Não há dados disponíveis";
				err_NoQuery = "Não se pode executar a consulta de base de dados.";
				err_Update = "Não foi possível atualizar {0} #{1}."; // {0}=entidade {1}=ID 
				err_Delete = "Não foi possível excluir {0} #{1}."; // {0}=entidade {1}=ID 
				MHValidValue = "{0} deve ter um valor válido."; // {0}=campo 
				NA = "N/A";
				NoUpload = "Não se pode carregar o arquivo";
				NoUpload2 = "Somente GIF, JPG, PNG e formatos de imagen são permitidos.";

				// --- status --- 
				NewSave = "Nova {0} incluída às {1}."; // {0}=entidade {1}=hora 
				NoUpdate = "Não é necessário atualizar a informação.";
				DeleteOK = "Registro # {0} eliminado em {1: t}"; // {0}=ID {1}=tempo 
				Updated = "{0} atualizado às {1: t}"; // {0}=entidade {1}=tempo 
				DetailsUpdated = "Informação atualizada";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

				// --- login --- 
				PleaseLogin = "Por favor, faça log in";
				Logout = "Finalizar Sessão";
				Login = "Login";
				LoginB = "Login";
				Password = "Senha";
				InvalidLogin = "Usuário/Senha Inválido.";
				InvalidLogin2 = "Por favor, tente novamente.";

				// --- grid --- 
				AddRow = "Adicionar linha";
				DelRow = "Excluir linha";
				Customize = "Personalizar";

				// --- Search & LOVs ---
				wPix = "com imagen";
				wDoc = "com arquivo anexo";
				wComments = "Com os comentários do usuário.";
				yes = "Sim";
				no = "Não";
				any = "Todo";
				anyof = "Qualquer de";
				PubMine = "todos os públicos e as minhas.";

				// --- toolbar --- 
				View = "Ver";
				Edit = "Editar";
				New = "Novo";
				NewItem = "Nova partida";
				NewUpload = "Novo Upload";
				Search = "Buscar";
				AdvSearch = "Busca avançada";
				NewSearch = "Nova Busca";
				Selections = "Seleções";
				Selection = "Seleção";
				Export = "Exportar";
				SearchRes = "Resultados da busca";
				Charts = "Gráficos"; // googletranslate
				MassUpdate = "Mass Update";  // googletranslate
				Delete = "Excluir";
				ListAll = "Todos";
				Print = "Imprimir";
				DeleteEntity = "Eliminar este {0}?"; // {0}=entidade
				Back2SearchResults = "Voltar aos resultados da busca";

				// --- navigation --- 
				pFirst = "Primeira";
				pPrev = "Anterior";
				pNext = "Seguinte";
				pLast = "Último";

				sBefore = "Antes de ";
				sAfter = "Depois de ";

				sDateRangeLast = "na última";
				sDateRangeNext = "no próximo";
				sDateRangeWithin = "dentro";
				sDateRangeAny = "qualquer momento";
				sDateRange = "día|24 horas, semana|1 semana, mes|1 mes e ano|1 año";
				sEquals = "Igual";

				// --- search form dropdown ---
				sStart = "Inicia com ";
				sContain = "contendo";
				sFinish = "termina com";
				sIsNull = "está vazia";
				sIsNotNull = "não está vazia";
				qEquals = " igual ";
				qStart = " começa com ";
				qInList = " em lista ";
				qNot = " não ";
				qWith = " com ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " começa com \"{0}\""; //{0}= FieldValue 
				lFinish = " termina com \"{0}\""; //{0}= FieldValue 
				lContain = " contém \"{0}\""; //{0}= FieldValue 
				lIsNull = " está vazia \"{0}\"";
				lIsNotNull = " não está vazia \"{0}\"";

				opAnd = " e ";	//babelfish
				opOr = " ou ";	//babelfish

				cAt = "A";
				sOn = "On";
				sOf = " de ";
				Checked = "Comprovado";
				Save = "Salvar";
				SaveAdd = "Guardar e adicionar outro";
				Cancel = "Cancelar";
				NoX = "Nenhum {0}"; // googletranslate
				NoChange = "Sem alteração"; //"No Change" googletranslate
				NoGraph = "Não gráficos disponíveis."; // "No graphs available." googletranslate
				chart_A_per_B = "{0} / {1}"; // to be reviewed

				// --- user comments --- 
				ucPostedOn = "Comentário publicado em {0:t}."; //{0}=tempo
				ucPost = "Publicar seus comentários";
				ucAdd = "Adicionar suas próprias observações";
				ucNoComments = "Não há comentários de usuários para esta {0}"; // {0}=entidad 
				ucNb = "{0} comentários de usuários para esta {1}."; // {0}=NB {1}=entidad 
				ucMissing = "Alguns comentários estão desaparecidos.";
				ucFrom = "De ";
				ucOn = " em ";

			}
		}
	}
}

