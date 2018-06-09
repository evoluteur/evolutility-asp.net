'    (c) 2003-2008 Olivier Giulieri - olivier@evolutility.com 

'    This program is open source software: you can redistribute it and/or modify
'    it under the terms of the GNU Affero General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.

'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU Affero General Public License for more details.

'    You should have received a copy of the GNU Affero General Public License
'    along with this program.  If not, see <http://www.gnu.org/licenses/>.


Option Explicit On
Option Strict On

Namespace Evolutility.WebControls

    Module EvoLang

        '### Variables - Language ###
#Region "Language Variables"

        Friend LG_entity As String
        Friend LG_entities As String

        Friend entity As String, entities As String, allEntities As String
        Friend LG_AllEntities As String
        Friend LG_Welcome As String 
        Friend LG_CommentsPostedOn As String
        Friend LG_InsertEntity As String, LG_ModifyEntity As String, LG_DownloadEntity As String, LG_NoEntity As String

        Friend LG_ExportEntity As String
        Friend LG_ExportHeader As String
        Friend LG_ExportSeparator As String
        Friend LG_ExportFirstLine As String
        Friend LG_XMLroot As String, LG_XMLAttr As String, LG_XMLElem As String
        Friend LG_ExportFormat As String
        Friend LG_ExportFields As String
        Friend LG_IDkey As String
        Friend LG_ColMap As String
        Friend LG_ExportFormats As String
        Friend LG_ExportColors As String
        Friend LG_ExportSQL As String

        Friend LG_MHValidValue As String
        Friend LG_JSNoWay As String
        Friend LG_NoDataDisp As String
        Friend LG_NoData As String
        Friend LG_NA As String
        Friend LG_NoUpload As String, LG_NoUpload2 As String
        Friend LG_NoQuery As String
        Friend LG_Customize As String
        Friend LG_NewSave As String
        Friend LG_CannotUpdate As String
        Friend LG_NoUpdate As String
        Friend LG_CannotDelete As String
        Friend LG_DeleteOK As String
        Friend LG_Updated As String
        Friend LG_DetailsUpdate As String
        Friend LG_PleaseLogin As String
        Friend LG_Logout As String
        Friend LG_Login As String
        Friend LG_LoginB As String
        Friend LG_Password As String
        Friend LG_InvalidLogin As String, LG_InvalidLogin2 As String
        'friend LG_Remember As String = "Remember me"
        Friend LG_AddRow As String
        Friend LG_DelRow As String

        Friend LG_wPix As String
        Friend LG_wDoc As String
        Friend LG_wComments As String
        Friend LG_yes As String
        Friend LG_no As String
        Friend LG_any As String
        Friend LG_anyof As String
        Friend LG_PubMine As String
        'friend LG_MyEntities As String = "My ~ENTITIES~"

        Friend LG_AdvSearch As String
        Friend LG_View As String
        Friend LG_Edit As String
        Friend LG_Post As String
        '   friend LG_Login As String = "Login"
        Friend LG_New As String
        Friend LG_NewItem As String
        Friend LG_NewUpload As String
        Friend LG_Search As String
        Friend LG_NewSearch As String
        Friend LG_Selection As String, LG_Selections As String
        Friend LG_Export As String
        Friend LG_SearchRes As String
        Friend LG_Delete As String
        Friend LG_ListAll As String
        Friend LG_Print As String
		Friend LG_DeleteEntity As String
		Friend LG_Back2SearchResults As String

        Friend LG_pFirst As String, LG_pPrev As String, LG_pNext As String, LG_pLast As String
        Friend LG_sBefore As String, LG_sAfter As String

        Friend LG_sDateRangeLast As String
        Friend LG_sDateRangeNext As String
        Friend LG_sDateRangeWithin As String
        Friend LG_sDateRangeAny As String
        Friend LG_sDateRange As String
        ' {0}=entity
        Friend LG_cEquals As String
        Friend LG_sStart As String
        Friend LG_sContain As String
		Friend LG_sFinish As String
		'Friend LG_sIsNull As String
		'Friend LG_sIsNotNull As String
        Friend LG_qEquals As String
        Friend LG_qStart As String
        Friend LG_qInList As String
        Friend LG_qNot As String
        Friend LG_qWith As String
        Friend LG_lEquals As String
        Friend LG_lStart As String
        Friend LG_lFinish As String
        Friend LG_lContain As String

        Friend LG_cAt As String
        Friend LG_sOn As String
        Friend LG_sOf As String
        Friend LG_Checked As String
        Friend LG_Save As String
        Friend LG_SaveAdd As String
        Friend LG_Cancel As String

        Friend LG_cmPost As String
        Friend LG_cmAdd As String
        Friend LG_cmMy As String
        Friend LG_cmNo As String
        Friend LG_cmNb As String
        Friend LG_cmMissing As String
        Friend LG_cmFrom As String
        Friend LG_cmOn As String

        'Friend LG_JS_provideValid As String = "You must provide a value for field ""{0}""."
        'Friend LG_JS_email As String = "Invalid email format for field ""{0}""." ' Use must be formatted like ""name@domain.com""."
        'Friend LG_JS_integer As String = "Field ""{0}"" must only use numbers."
        'Friend LG_JS_decimal As String = "Field ""{0}"" must be a valid decimal numbers."
        'Friend LG_JS_date As String = "Invalid date format for field ""{0}"". Format must be ""MM/DD/YYYY"" like ""12/24/2005""."
        'Friend LG_JS_datetime As String = "Invalid date/time format for field ""{0}"". Valid format is ""MM/DD/YYYY hh:mm am/pm"" like ""12/24/2005 10:30 am""."
        'Friend LG_JS_time As String = "Invalid time format for field ""{0}"". Valid format is ""hh:mm am/pm"" like ""10:30 am""."
        'Friend LG_JS_min As String = "Field ""{0}"" must not be less than {1}."
        'Friend LG_JS_max As String = "Field ""{0}"" must not be more than {1}."

#End Region

        Friend Function LoadLanguage(ByVal LanguageKey As String) As Boolean
            Select Case LanguageKey
                Case "EN" 'English
                    If LG_entity <> "item" Then
                        LG_entity = "item"
                        LG_entities = "items"

                        entity = LG_entity
                        entities = LG_entities
                        LG_Welcome = "Welcome {0}" '{0}=login
                        LG_AllEntities = "All {0}" ' {0}=entities
                        LG_CommentsPostedOn = "Comments posted on {0:t}."  '{0}=time
                        LG_InsertEntity = "insert new {0}."  ' {0}=entity
                        LG_ModifyEntity = "modify {0}."   ' {0}=entity
                        LG_DownloadEntity = "Download {0}" ' {0}=entity
                        LG_NoEntity = "No item found."  ' not {0}=entity b/c of panel details

                        LG_ExportEntity = "Export this {0}"  ' {0}=entity
                        LG_ExportHeader = "Header"
                        LG_ExportSeparator = "Separator"
                        LG_ExportFirstLine = "First line for field names"
                        LG_XMLroot = "Root element name"
                        LG_XMLAttr = "Attributes"
                        LG_XMLElem = "Elements"
                        LG_ExportFormat = "Export Format"
                        LG_ExportFields = "Fields to include in the export"
                        LG_IDkey = "ID (Primary Key)"
                        LG_ColMap = "Columns map to"
                        LG_ExportFormats = "Comma separated (CSV, TXT, XLS...)-HTML-SQL Insert Statements (SQL)-Tab separated values (TXT)-XML"
                        LG_ExportColors = "#D5D5D5-Header color-#EDEDED-Color odd rows-#F3F3F3-Color even rows"
                        LG_ExportSQL = "SQL Options-Inside Transaction-Enable identity insert"

                        LG_MHValidValue = "{0} must have a valid value."  '{0}=field
                        LG_JSNoWay = "You are not allowed to "
                        LG_NoDataDisp = "No Data to display."
                        LG_NoData = "No data available."
                        LG_NA = "N/A"
                        LG_NoUpload = "Cannot upload file."
                        LG_NoUpload2 = "Only GIF, JPG, and PNG image formats are allowed."
                        LG_NoQuery = "Cannot execute Database query."

                        LG_NewSave = "New {0} saved at {1}."  ' {0}=entity {1}=now
                        LG_CannotUpdate = "Cannot update {0} #{1}."   ' {0}=entity {1}=ID
                        LG_NoUpdate = "No update necessary."
                        LG_CannotDelete = "Cannot delete {0} #{1}."   ' {0}=entity {1}=ID
                        LG_DeleteOK = "Record #{0} deleted at {1:t}."   ' {0}=ID {1}=time
                        LG_Updated = "{0} updated at {1:t}."   ' {0}=entity {1}=time
                        LG_DetailsUpdate = "Details updated."
                        LG_PleaseLogin = "Please log in."
                        LG_Logout = "Logout"
                        LG_Login = "Login"
                        LG_LoginB = "Login"
                        LG_Password = "Password"
                        LG_InvalidLogin = "Invalid Login/Password."
                        LG_InvalidLogin2 = "Please, try again."
                        'LG_Remember = "Remember me"
                        LG_AddRow = "Add row"
                        LG_DelRow = "Delete row"
                        LG_Customize = "Customize"

                        LG_wPix = " with picture"
                        LG_wDoc = " with attachment"
                        LG_wComments = "With User comments"
                        LG_yes = "Yes"
                        LG_no = "No"
                        LG_any = "Any"
                        LG_anyof = "Any of"
                        LG_PubMine = "All public and mine"
                        'LG_MyEntities = "My ~ENTITIES~"

                        LG_AdvSearch = "Advanced Search"
                        LG_View = "View"
                        LG_Edit = "Edit"
                        LG_Post = "Post"
                        '   LG_Login = "Login"
                        LG_New = "New"
                        LG_NewItem = "New Item"
                        LG_NewUpload = "New Upload"
                        LG_Search = "Search"
                        LG_NewSearch = "New Search"
                        LG_Selections = "Selections"
                        LG_Selection = "Selection"
                        LG_Export = "Export"
                        LG_SearchRes = "Search Result"
                        LG_Delete = "Delete"
                        LG_ListAll = "List All"
                        LG_Print = "Print"
                        LG_DeleteEntity = "Delete this {0}?"   ' {0}=entity
						LG_Back2SearchResults = "Back to search results"

                        LG_pFirst = "First"
                        LG_pPrev = "Previous"
                        LG_pNext = "Next"
                        LG_pLast = "Last"
                        LG_sBefore = "Before"
                        LG_sAfter = "After"

                        LG_sDateRangeLast = " in the last "   ' "P|in the last,F|in the next"
                        LG_sDateRangeNext = " in the next "
                        LG_sDateRangeWithin = " within "
                        LG_sDateRangeAny = " any time "
                        LG_sDateRange = "day|24 hours,week|1 week,month|1 month,year|1 year"
                        ' {0}=entity
                        LG_cEquals = "Equals"
                        LG_sStart = "Starts with"
                        LG_sContain = "Contains"
						LG_sFinish = "Finishes with"
						'LG_sIsNull = "Is empty"
						'LG_sIsNotNull = "Is not empty"
                        LG_qEquals = " equals "
                        LG_qStart = " starts with "
                        LG_qInList = " in list "
                        LG_qNot = " not "
                        LG_qWith = " with "
                        LG_lEquals = " = ""{0}""" '{0}= FieldValue 
                        LG_lStart = " starts with ""{0}""" '{0}= FieldValue 
                        LG_lFinish = " finishes with ""{0}""" '{0}= FieldValue 
                        LG_lContain = " contains ""{0}""" '{0}= FieldValue 

                        LG_cAt = "At"
                        LG_sOn = "On"
                        LG_sOf = " of "
                        LG_Checked = "Checked"
                        LG_Save = "Save"
                        LG_SaveAdd = "Save and Add Another"
                        LG_Cancel = "Cancel"

                        LG_cmPost = "Post your comments"
                        LG_cmAdd = "Add your own comments"
                        LG_cmMy = "My comments"
                        LG_cmNo = "No user comments for this {0} yet."  '{0}=entity
                        LG_cmNb = "{0} user comments for this {1}."  '{0}=NB {1}=entity
                        LG_cmMissing = "Some comments are missing."
                        LG_cmFrom = "From "
                        LG_cmOn = " on "

                        'LG_JS_provideValid = "You must provide a value for field ""{0}""."
                        'LG_JS_email = "Invalid email format for field ""{0}""." ' Use must be formatted like ""name@domain.com""."
                        'LG_JS_integer = "Field ""{0}"" must only use numbers."
                        'LG_JS_decimal = "Field ""{0}"" must be a valid decimal numbers."
                        'LG_JS_date = "Invalid date format for field ""{0}"". Format must be ""MM/DD/YYYY"" like ""12/24/2005""."
                        'LG_JS_datetime = "Invalid date/time format for field ""{0}"". Valid format is ""MM/DD/YYYY hh:mm am/pm"" like ""12/24/2005 10:30 am""."
                        'LG_JS_time = "Invalid time format for field ""{0}"". Valid format is ""hh:mm am/pm"" like ""10:30 am""."
                        'LG_JS_min = "Field ""{0}"" must not be less than {1}."
                        'LG_JS_max = "Field ""{0}"" must not be more than {1}."
                    End If
                    Return True

                Case "FR" 'French 
                    If LG_entity <> "article" Then
                        LG_entity = "article"
                        LG_entities = "articles"
                        LG_Welcome = "Bienvenue {0}" ' {0}=login
                        LG_AllEntities = "Toutes les fiches {0}" ' {0}=entities
                        LG_CommentsPostedOn = "Commentaires ajoutés le {0:t}." ' {0}=time
                        LG_InsertEntity = "insérer une nouvelle fiche {0}." ' {0}=entity
                        LG_ModifyEntity = "modifier la fiche {0}." ' {0}=entity
                        LG_DownloadEntity = "Télécharger {0}" ' {0}=entity
                        LG_NoEntity = "Aucune fiche trouvée." ' {0}=entity

                        LG_ExportEntity = "Exporter cette fiche {0}" ' {0}=entity
                        LG_ExportHeader = "Entête"
                        LG_ExportSeparator = "Séparateur"
                        LG_ExportFirstLine = "Première ligne pour les titres de champs"
                        LG_XMLroot = "Nom de l'élément racine"
                        LG_XMLAttr = "Attributs"
                        LG_XMLElem = "Eléments"
                        LG_ExportFormat = "Format d'export"
                        LG_ExportFields = "Champs à inclure dans l'export"
                        LG_IDkey = "ID (clée primaire)"
                        LG_ColMap = "Colonnes connectées à"
                        LG_ExportFormats = "Séparés par une virgule (CSV, TXT, XLS...)-HTML-SQL script (SQL)-Valeurs séparées par des tabulations (TXT)-XML"
                        LG_ExportColors = "#D5D5D5-Couleur des titres-#EDEDED-Couleur lignes impaires-#F3F3F3-Couleur lignes paires"
                        LG_ExportSQL = "SQL Options-Utiliser transaction-Forcer IDs"

                        LG_MHValidValue = "{0} doit avoir une valeur valide." ' {0}=field
                        LG_JSNoWay = "Vous n'êtes pas autorisé à "
                        LG_NoDataDisp = "Aucune donnée à afficher."
                        LG_NoData = "Aucune donnée disponible."
                        LG_NA = "N/A"
                        LG_NoUpload = "Impossible d'importer le fichier."
                        LG_NoUpload2 = "Seuls les formats GIF, JPG et PNG sont autorisés !"
                        LG_NoQuery = "Impossible d'exécuter la requête base de donnée."

                        LG_NewSave = "Nouvelle fiche {0} sauvegardée à {1}." ' {0}=entity {1}=now
                        LG_CannotUpdate = "Impossible de mettre à jour la fiche {0} #{1}." ' {0}=entity {1}=ID
                        LG_NoUpdate = "Aucune mise à jour nécessaire"
                        LG_CannotDelete = "Impossible de supprimer la fiche {0} #{1}." ' {0}=entity {1}=ID
                        LG_DeleteOK = "Fiche {0} supprimée à {1:t}." ' {0}=ID {1}=time
                        LG_Updated = "Fiche {0} mise à jour à {1:t}."
                        LG_DetailsUpdate = "Détails mis à jour."
                        LG_PleaseLogin = "Merci de vous identifier."
                        LG_Logout = "Déconnexion"
                        LG_Login = "Nom d'utilisateur"
                        LG_LoginB = "Entrer"
                        LG_Password = "Mot de passe"
                        LG_InvalidLogin = "Identifiant/mot de passe invalides"
                        LG_InvalidLogin2 = "Merci de réessayer."
                        'LG_Remember = ""
                        LG_AddRow = "Ajouter une ligne"
                        LG_DelRow = "Supprimer ligne"
                        LG_Customize = "Personaliser"

                        LG_wPix = " avec image"
                        LG_wDoc = " avec fichier joint"
                        LG_wComments = "Avec commentaires de l'utilisateur"
                        LG_yes = "Oui"
                        LG_no = "Non"
						LG_any = "Tout"
                        LG_anyof = "Parmi "
                        LG_PubMine = "Fiches publiques ou personnelles"
                        'LG_MyEntities = "Mes fiches ~ENTITIES~"

                        LG_AdvSearch = "Recherche avancée"
                        LG_View = "Lecture"
                        LG_Edit = "Edition"
                        LG_Post = "Envoyer"
                        LG_New = "Nouveau"
                        LG_NewItem = "Nouvelle entrée"
                        LG_NewUpload = "Nouveau fichier joint"
                        LG_Search = "Recherche"
                        LG_NewSearch = "Nouvelle recherche"
                        LG_Selections = "Selections"
                        LG_Selection = "Selection"
                        LG_Export = "Exporter"
                        LG_SearchRes = "Résultats de la recherche"
                        LG_Delete = "Supprimer"
                        LG_ListAll = "Liste complete"
                        LG_Print = "Impression"
						LG_DeleteEntity = "Supprimer cette fiche {0}?" '{0}=entity
						LG_Back2SearchResults = "Retours au Résultats de recherche"

                        LG_pFirst = "Premier"
                        LG_pPrev = "Précédent"
                        LG_pNext = "Suivant"
                        LG_pLast = "Dernier"
                        LG_sBefore = "Avant"
                        LG_sAfter = "Après"

                        LG_sDateRangeLast = " pendant le dernier " ' "P|in the last,F|in the next"
                        LG_sDateRangeNext = " pendant le prochain "
                        LG_sDateRangeWithin = " pendant le dernier/prochain"
                        LG_sDateRangeAny = " toute date "
                        LG_sDateRange = "day|24 heures,week|1 semaine,month|1 mois,year|1 année"
                        '"day|24 hours,week|1 week,month|1 month,year|1 year"

                        LG_cEquals = "Egale"
                        LG_sStart = "Commence par"
                        LG_sContain = "Contient"
						LG_sFinish = "Termine par"
						'LG_sIsNull = "Est vide"
						'LG_sIsNotNull = "N'est pas vide"
                        LG_qEquals = " égale "
                        LG_qStart = " commence par "
                        LG_qInList = " dans la liste "
                        LG_qNot = " sans "
                        LG_qWith = " avec "
                        LG_lEquals = " = ""{0}""" '{0}= FieldValue 
                        LG_lStart = " commence par ""{0}""" '{0}= FieldValue 
                        LG_lFinish = " fini par ""{0}""" '{0}= FieldValue 
                        LG_lContain = " contient ""{0}""" '{0}= FieldValue 

                        LG_cAt = "à"
                        LG_sOn = "Le"
                        LG_sOf = " de "
                        LG_Checked = "cochée"
                        LG_Save = "Sauvegarder"
                        LG_SaveAdd = "Sauvegarder et nouvel ajout"
                        LG_Cancel = "Annuler"

                        LG_cmPost = "Ajouter vos commentaires"
                        LG_cmAdd = "Ajouter vos propres commentaires"
                        LG_cmMy = "Mes commentaires"
                        LG_cmNo = "Aucun commentaire pour cette fiche {0}." '{0}=entity
                        LG_cmNb = "{0} commentaires pour cette fiche {1}." '{0}=nb {1}=entity
                        LG_cmMissing = "Il manque des commentaires."
                        LG_cmFrom = " - De "
                        LG_cmOn = " à "

                        'LG_JS_provideValid = "Vous devez entrer correctement le champ {0}."
                        'LG_JS_email = "{0} doit avoir la forme ""identifiant@domaine.com""."
                        'LG_JS_integer = "{0} (chiffres uniquement)."
                        'LG_JS_decimal = "{0} doit etre un nombre decimal."
                        'LG_JS_date = "{0} écrire sous la forme Mois/jour/année, par exemple: 12/24/2005."
                        'LG_JS_datetime = "{0} écrire sous la forme Mois/jour/année heure:minutes am/pm, par exemple : 12/24/2005 10:30 am."
                        'LG_JS_time = "{0} écrire sous la forme heure:minutes am/pm, par exemple : 10:30 am."
                        'LG_JS_min = "{0} ne doit pas inferieur être à {1}."
                        'LG_JS_max = "{0} ne doit pas être superieur à {1}."

                        'LG_Edit = "Modifier la fiche"
                        'LG_Save = "Sauvegarder les modifications"
                        'LG_SaveAdd = "Sauvegarder les modifications et nouvel ajout"
                        'LG_View = "Fermer les modifications"
                    End If
                    Return True
                Case Else
                    Return False
            End Select
        End Function

    End Module

End Namespace
