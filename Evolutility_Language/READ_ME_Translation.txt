Evolutility translation kit
version 4.1 - 2/12/2013

We are always looking to add support for more languages and reach more users.

If Evolutility is not available in your language of choice and you wish to translate it, 
here are the steps to follow: 
 1 - Extract Evolutility_Translation.zip.
 2 - Lookup the ISO 639-1 language code for your translation at http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
 3 - In the CS folder, translate one of the C# files, and save it as a new file "EvoLang_XX.cs" named after the abbreviation of the language (examples "SP.cs" for Spanish, or "FR.cs" for French).
 4 - In the JS folder, translate one of the Javascript files, and save it as "XX.js".
 5 - In the XML folder, for the file AddressBook.XML, translate all attributes called "label". Do not translate other attributes in these files.
 6 - If possible, test your translation with the latest download of Evolutility code.
 7 - Tell us about date formats in your language. 
 Do you write dates Month/Day/Year (like in American English) or Day/Month/Year (like in French)? 
 Does the week starts on Sunday (like in English) or Monday (like in French) on calendars?
 8 - Let us know about any specificity of your language.
 Is this language written from left to right or from right to left? 
 9 - Copy the following text (our short contributor license agreement) and add it to your email : 
 "I'm offering this translation to Olivier Giulieri free of charge, with full rights to do anything he wants with it, and I do not expect anything in return."
 10 - Send an email to Olivier@Evolutility.org with: 
  + the translated files (3), (4), and (5)
  + the date format information (7)
  + the intellectual property ownership release (8)

Technical note:
Some strings contain "{0}" or "{1}". These are placeholders for other words to be inserted at run-time.
For example: "Welcome {0}" could become "Welcome John" or "Welcome Mary" when the program is running.
We use this placeholder scheme because words may be in different order in different languages.

Making corrections to existing translations.
I added some strings to the files using Google translate to get something working. 
Feel free to correct any translated string fladded with the comment "googletranslate". 
 
Evolutility currently supports:
- Catalan by Oscar Benadi.
- Chinese (simplified) by Sam Zhou.
- Danish by Henrik Holm.
- English by Olivier Giulieri.
- French by Eddy Boels.
- German by Joachim Seidel.
- Hindi by P.K.Agarwal.
- Italian by Pier Giuseppe Meo.
- Japanese by Kazue Watanabe.
- Persian (Farsi) by Sohail Abbasi
- Portuguese by Gilberto Botaro.
- Romanian by Cosmin Munteanu.
- Spanish by Gilberto Botaro.
- Turkish by Davut Engin.

Localized demos:
http://evolutility.org/demo/dev_Localization.aspx
 
List of ISO 639-1 language codes:
http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes

Latest Evolutility Translation Kit:
http://evolutility.org/more/Get_Involved_Translation.aspx
 
Most icons are from Mark James:
http://www.famfamfam.com/lab/icons/silk/
 
Thank you very much for helping the Evolutility project. 
Olivier Giulieri

www.evolutility.org

	Copyright (c) 2003-2013 Olivier Giulieri - olivier@evolutility.org 

	This file is part of Evolutility CRUD Framework.
	Source link <http://www.evolutility.org/download/download.aspx>

	Evolutility is open source software: you can redistribute it and/or modify
	it under the terms of the GNU Affero General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Evolutility is distributed WITHOUT ANY WARRANTY;
	without even the implied warranty of MERCHANTABILITY
	or FITNESS FOR A PARTICULAR PURPOSE.  
	See the	GNU Affero General Public License for more details.

	You should have received a copy of the GNU Affero General Public License
	along with Evolutility. If not, see <http://www.fsf.org/licensing/licenses/agpl-3.0.html>.


