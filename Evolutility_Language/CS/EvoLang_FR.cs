//	FRENCH - Translation from Eddy Boels 

//	Copyright (c) 2003-2013 Olivier Giulieri - olivier@evolutility.org 

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
	static partial class EvoLang // FRENCH - Translation from Eddy Boels - http://www.ed-design.org/
	{
		static internal void SetLocale_FR(string LanguageKey)
		{
			if (_LocaleCode != "FR")
			{
				_LocaleCode = "FR";
				_LocaleEN = "French"; // do not translate this line - this is the english name for the language
				_Locale = "Français";

				Welcome = "Bienvenue {0}"; // {0}=login 

				entity = "article";
				entities = "articles";
				AllEntities = "Toutes les fiches {0}"; // {0}=entities 

				InsertEntity = "insérer une nouvelle fiche {0}."; // {0}=entity 
				ModifyEntity = "modifier la fiche {0}."; // {0}=entity 
				DownloadEntity = "Télécharger {0}"; // {0}=entity 
				NoEntity = "Aucune fiche trouvée."; // {0}=entity 

				// --- export --- 
				ExportEntity = "Exporter cette fiche {0}"; // {0}=entity 
				ExportHeader = "Entête";
				ExportSeparator = "Séparateur";
				ExportFirstLine = "Première ligne pour les titres de champs";
				ExportFormat = "Format d'export";
				ExportFields = "Champs à inclure dans l'export";
				IDkey = "ID (clée primaire)";
				AllFields = "Tous les champs";
				ExportFormats = "Séparés par une virgule (CSV, TXT, XLS...)-HTML-SQL script (SQL)-Valeurs séparées par des tabulations (TXT)-XML-JSON";

				// --- errors & warnings --- 
				err_NoPermission = "Vous n'êtes pas autorisé à ";
				err_NoDataDisp = "Aucune donnée à afficher.";
				err_NoData = "Aucune donnée disponible.";
				err_NoQuery = "Impossible d'exécuter la requête base de donnée.";
				err_Update = "Impossible de mettre à jour la fiche {0} #{1}."; // {0}=entity {1}=ID 
				err_Delete = "Impossible de supprimer la fiche {0} #{1}."; // {0}=entity {1}=ID 
				MHValidValue = "{0} doit avoir une valeur valide."; // {0}=field 
				NA = "N/A";
				NoUpload = "Impossible d'importer le fichier.";
				NoUpload2 = "Seuls les formats GIF, JPG et PNG sont autorisés !";

				// --- status --- 
				NewSave = "Nouvelle fiche {0} sauvegardée à {1}."; // {0}=entity {1}=now 
				NoUpdate = "Aucune mise à jour nécessaire";
				DeleteOK = "Fiche {0} supprimée à {1:t}."; // {0}=ID {1}=time 
				Updated = "Fiche {0} mise à jour à {1:t}.";
				DetailsUpdated = "Détails mis à jour.";
				MassUpdated = "{0} {1} updated at {2:t}."; // {0}=nb rec {1}=entities  {2}=time // googletranslate

				// --- login --- 
				PleaseLogin = "Merci de vous identifier.";
				Logout = "Déconnexion";
				Login = "Nom d'utilisateur";
				LoginB = "Entrer";
				Password = "Mot de passe";
				InvalidLogin = "Identifiant/mot de passe invalides";
				InvalidLogin2 = "Merci de réessayer.";
				//Remember = "" 
				AddRow = "Ajouter une ligne";
				DelRow = "Supprimer ligne";
				Customize = "Personaliser";

				wPix = " avec image";
				wDoc = " avec fichier joint";
				wComments = "Avec commentaires de l'utilisateur";
				yes = "Oui";
				no = "Non";
				any = "Tout";
				anyof = "Parmi ";
				PubMine = "Fiches publiques ou personnelles";
				//MyEntities = "Mes fiches ~ENTITIES~" 

				// --- toolbar --- 
				View = "Lecture";
				Edit = "Edition";
				New = "Nouveau";
				NewItem = "Nouvelle entrée";
				NewUpload = "Nouveau fichier joint";
				Search = "Recherche";
				AdvSearch = "Recherche avancée";
				NewSearch = "Nouvelle recherche";
				Selections = "Selections";
				Selection = "Selection";
				Export = "Exporter";
				SearchRes = "Résultats de la recherche";
				Charts = "Graphs";
				MassUpdate = "Mise à jour de masse"; 
				Delete = "Supprimer";
				ListAll = "Liste complete";
				Print = "Impression";
				DeleteEntity = "Supprimer cette fiche {0}?"; //{0}=entity 
				Back2SearchResults = "Retours au Résultats de recherche";

				// --- navigation --- 
				pFirst = "Premier";
				pPrev = "Précédent";
				pNext = "Suivant";
				pLast = "Dernier";

				sBefore = "Avant";
				sAfter = "Après";

				sDateRangeLast = " pendant le dernier ";
				sDateRangeNext = " pendant le prochain ";
				sDateRangeWithin = " pendant le dernier/prochain";
				sDateRangeAny = " toute date ";
				sDateRange = "day|24 heures,week|1 semaine,month|1 mois,year|1 année"; //"day|24 hours,week|1 week,month|1 month,year|1 year" 
				sEquals = "Egale";

				// --- search form dropdown ---
				sStart = "Commence par";
				sContain = "Contient";
				sFinish = "Termine par";
				sIsNull = "Est vide";
				sIsNotNull = "N'est pas vide";
				qEquals = " égale ";
				qStart = " commence par ";
				qInList = " dans la liste ";
				qNot = " sans ";
				qWith = " avec ";

				// --- search result conditions --- 
				lEquals = " = \"{0}\""; //{0}= FieldValue 
				lStart = " commence par \"{0}\""; //{0}= FieldValue 
				lFinish = " fini par \"{0}\""; //{0}= FieldValue 
				lContain = " contient \"{0}\""; //{0}= FieldValue 
				lIsNull = "\"{0}\" est vide";
				lIsNotNull = "\"{0}\" n'est pas vide";

				opAnd = " et ";
				opOr = " ou ";

				cAt = "à";
				sOn = "Le";
				sOf = " de ";
				Checked = "cochée";
				Save = "Sauvegarder";
				SaveAdd = "Sauvegarder et nouvel ajout";
				Cancel = "Annuler";
				NoX = "Aucun(e) {0}"; // googletranslate
				NoChange = "Aucun changement";
				NoGraph = "Pas de graphiques disponibles.";
				chart_A_per_B = "{0} / {1}"; // to be reviewed

				// --- user comments --- 
				ucPostedOn = "Commentaires ajoutés le {0:t}."; // {0}=time 
				ucPost = "Ajouter vos commentaires";
				ucAdd = "Ajouter vos propres commentaires";
				ucNoComments = "Aucun commentaire pour cette fiche {0}."; //{0}=entity 
				ucNb = "{0} commentaires pour cette fiche {1}."; //{0}=nb {1}=entity 
				ucMissing = "Il manque des commentaires.";
				ucFrom = "De ";
				ucOn = " à ";

			}
		}
	}
}
